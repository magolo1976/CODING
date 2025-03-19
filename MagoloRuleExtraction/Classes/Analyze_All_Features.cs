using System.Data;

namespace MagoloRuleExtraction.Classes
{
    public class Analyze_All_Features
    {
        private readonly Select_Uncorrelated_Features _featureSelector;
        private readonly Analyze_Feature _featureAnalyzer;
        private readonly Calculate_Random_Profits _randomProfitsCalculator;

        public Analyze_All_Features()
        {
            _featureSelector = new Select_Uncorrelated_Features();
            _featureAnalyzer = new Analyze_Feature();
            _randomProfitsCalculator = new Calculate_Random_Profits();
        }

        /// <summary>
        /// Analiza todas las características, primero reduciendo la multicolinealidad
        /// </summary>
        /// <param name="dataFrame">DataTable con los datos</param>
        /// <param name="targetColumn">Nombre de la columna objetivo</param>
        /// <param name="dateColumn">Nombre de la columna de fechas</param>
        /// <param name="side">Dirección de la operación: 'long' o 'short'</param>
        /// <param name="correlationThreshold">Umbral de correlación para seleccionar características</param>
        /// <returns>Tupla con DataFrame de resultados y diccionario de reglas</returns>
        public Tuple<DataTable, Dictionary<string, Dictionary<string, object>>> DoWork(
            DataTable dataFrame,
            string targetColumn,
            string dateColumn,
            string side,
            double correlationThreshold = 0.7)
        {
            // Definir columnas a excluir
            List<string> excludeColumns = new List<string> { dateColumn, "Open", "Close", targetColumn, "Target" };

            // Obtener características iniciales (todas las columnas excepto las excluidas)
            List<string> initialFeatures = new List<string>();
            foreach (DataColumn column in dataFrame.Columns)
            {
                if (!excludeColumns.Contains(column.ColumnName))
                {
                    initialFeatures.Add(column.ColumnName);
                }
            }

            // Seleccionar características no correlacionadas
            List<string> selectedFeatures = _featureSelector.DoWork(
                dataFrame, initialFeatures, correlationThreshold);

            // Extraer retornos y calcular ganancias aleatorias
            List<double> returns = ExtractColumnValues(dataFrame, targetColumn);
            double[] randomProfits = _randomProfitsCalculator.DoWork(returns, side);

            // Analizar cada característica seleccionada
            List<Dictionary<string, object>> results = new List<Dictionary<string, object>>();
            Dictionary<string, Dictionary<string, object>> rulesDict = new Dictionary<string, Dictionary<string, object>>();

            foreach (string feature in selectedFeatures)
            {
                try
                {
                    Tuple<double, string> analysisResult = _featureAnalyzer.DoWork(
                        dataFrame, feature, targetColumn, side);

                    double featureMetric = analysisResult.Item1;
                    string featureRule = analysisResult.Item2;

                    // Calcular puntuación (porcentaje de veces que supera a la métrica aleatoria)
                    double score = randomProfits.Count(rp => featureMetric > rp) * 100.0 / randomProfits.Length;

                    if (score > 0)
                    {
                        results.Add(new Dictionary<string, object>
                        {
                            { "feature", feature },
                            { "score", score }
                        });

                        rulesDict[feature] = new Dictionary<string, object>
                        {
                            { "rule", featureRule },
                            { "score", score }
                        };
                    }
                }
                catch
                {
                    continue;
                }
            }

            // Crear DataTable de resultados
            DataTable resultsTable = new DataTable();
            resultsTable.Columns.Add("Feature", typeof(string));
            resultsTable.Columns.Add("Score", typeof(double));

            if (results.Count > 0)
            {
                // Ordenar resultados por puntuación (descendente)
                results = results.OrderByDescending(r => (double)r["score"]).ToList();

                // Llenar la tabla de resultados
                int n = 0;
                foreach (var result in results)
                {
                    DataRow row = resultsTable.NewRow();
                    row["Feature"] = result["feature"];
                    row["Score"] = Math.Round((double)result["score"], 2);
                    resultsTable.Rows.Add(row);
                }
            }

            return new Tuple<DataTable, Dictionary<string, Dictionary<string, object>>>(
                resultsTable, rulesDict);
        }

        /// <summary>
        /// Extrae los valores de una columna como lista de doubles
        /// </summary>
        private List<double> ExtractColumnValues(DataTable data, string columnName)
        {
            List<double> values = new List<double>();
            foreach (DataRow row in data.Rows)
            {
                if (row[columnName] != DBNull.Value)
                {
                    values.Add(Convert.ToDouble(row[columnName]));
                }
            }
            return values;
        }
    }
}
