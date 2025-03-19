using System.Data;

namespace MagoloRuleExtraction.Classes
{
    public class Calculate_Target
    {
        public static DataTable DoWork(DataTable dt)
        {
            // Verificar que el DataTable tenga las columnas necesarias
            if (!dt.Columns.Contains("Open"))
            {
                throw new ArgumentException("El DataTable debe contener una columna 'Open'");
            }

            // Agregar columnas temporales para los cálculos
            dt.Columns.Add("Next_Open", typeof(double));
            dt.Columns.Add("Next_Next_Open", typeof(double));
            dt.Columns.Add("Target", typeof(double));

            // Calcular Next_Open y Next_Next_Open - equivalente a shift(-1) y shift(-2)
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                // Calcular Next_Open (valor T+1)
                if (i < dt.Rows.Count - 1)
                {
                    dt.Rows[i]["Next_Open"] = Convert.ToDouble(dt.Rows[i + 1]["Open"]);
                }
                else
                {
                    dt.Rows[i]["Next_Open"] = DBNull.Value;
                }

                // Calcular Next_Next_Open (valor T+2)
                if (i < dt.Rows.Count - 2)
                {
                    dt.Rows[i]["Next_Next_Open"] = Convert.ToDouble(dt.Rows[i + 2]["Open"]);
                }
                else
                {
                    dt.Rows[i]["Next_Next_Open"] = DBNull.Value;
                }
            }

            // Calcular Target: (Open(T+2) - Open(T+1)) * 100 / Open(T+1)
            foreach (DataRow row in dt.Rows)
            {
                if (row["Next_Open"] != DBNull.Value && row["Next_Next_Open"] != DBNull.Value)
                {
                    double nextOpen = Convert.ToDouble(row["Next_Open"]);
                    double nextNextOpen = Convert.ToDouble(row["Next_Next_Open"]);

                    // Evitar división por cero
                    if (nextOpen != 0)
                    {
                        row["Target"] = (nextNextOpen - nextOpen) * 100 / nextOpen;
                    }
                    else
                    {
                        row["Target"] = DBNull.Value;
                    }
                }
                else
                {
                    row["Target"] = DBNull.Value;
                }
            }

            // Crear una copia del DataTable sin las filas que contienen valores nulos
            DataTable resultTable = dt.Clone();

            // Eliminar columnas temporales que no son necesarias en el resultado
            resultTable.Columns.Remove("Next_Open");
            resultTable.Columns.Remove("Next_Next_Open");

            // Copiar solo las filas que no tienen valores nulos en Target (equivalente a dropna())
            foreach (DataRow row in dt.Rows)
            {
                if (row["Target"] != DBNull.Value)
                {
                    DataRow newRow = resultTable.NewRow();
                    foreach (DataColumn col in resultTable.Columns)
                    {
                        newRow[col.ColumnName] = row[col.ColumnName];
                    }
                    resultTable.Rows.Add(newRow);
                }
            }

            return resultTable;
        }
    }
}
