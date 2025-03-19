using System.Data;

namespace MagoloRuleExtraction.Classes
{
    public class Create_Returns_Stats_Table
    {
        /// <summary>
        /// Crea una tabla con las estadísticas de los retornos
        /// </summary>
        public static DataTable DoWork(IEnumerable<double> trainReturns, IEnumerable<double> testReturns, IEnumerable<double> forwardReturns)
        {
            var trainStats = CalculateStats(trainReturns);
            var testStats = CalculateStats(testReturns);
            var forwardStats = CalculateStats(forwardReturns);

            // Crear la tabla
            DataTable table = new DataTable();

            // Añadir columnas
            table.Columns.Add("Estadística", typeof(string));
            table.Columns.Add("Test", typeof(string));
            table.Columns.Add("Train", typeof(string));
            table.Columns.Add("Forward", typeof(string));

            // Añadir filas
            table.Rows.Add("Retorno Promedio (%)",
                           $"{testStats["mean"]:F3}",
                           $"{trainStats["mean"]:F3}",
                           $"{forwardStats["mean"]:F3}");

            table.Rows.Add("Desviación Estándar (%)",
                           $"{testStats["std"]:F3}",
                           $"{trainStats["std"]:F3}",
                           $"{forwardStats["std"]:F3}");

            table.Rows.Add("Retorno Mínimo (%)",
                           $"{testStats["min"]:F3}",
                           $"{trainStats["min"]:F3}",
                           $"{forwardStats["min"]:F3}");

            table.Rows.Add("Retorno Máximo (%)",
                           $"{testStats["max"]:F3}",
                           $"{trainStats["max"]:F3}",
                           $"{forwardStats["max"]:F3}");

            table.Rows.Add("% Retornos Positivos",
                           $"{testStats["pos_pct"]:F1}",
                           $"{trainStats["pos_pct"]:F1}",
                           $"{forwardStats["pos_pct"]:F1}");

            table.Rows.Add("Número de Observaciones",
                           $"{testStats["count"]}",
                           $"{trainStats["count"]}",
                           $"{forwardStats["count"]}");

            return table;
        }

        /// <summary>
        /// Calcula estadísticas para una colección de retornos
        /// </summary>
        private static Dictionary<string, double> CalculateStats(IEnumerable<double> returns)
        {
            if (returns == null || !returns.Any())
            {
                return new Dictionary<string, double>
                {
                    { "mean", 0 },
                    { "std", 0 },
                    { "min", 0 },
                    { "max", 0 },
                    { "pos_pct", 0 },
                    { "count", 0 }
                };
            }

            double[] returnsArray = returns.ToArray();

            return new Dictionary<string, double>
            {
                { "mean", returnsArray.Average() },
                { "std", CalculateStandardDeviation(returnsArray) },
                { "min", returnsArray.Min() },
                { "max", returnsArray.Max() },
                { "pos_pct", returnsArray.Count(r => r > 0) * 100.0 / returnsArray.Length },
                { "count", returnsArray.Length }
            };
        }

        /// <summary>
        /// Calcula la desviación estándar
        /// </summary>
        private static double CalculateStandardDeviation(double[] values)
        {
            double avg = values.Average();
            double sum = values.Sum(d => Math.Pow(d - avg, 2));
            return Math.Sqrt(sum / (values.Length - 1 > 0 ? values.Length - 1 : 1)); // n-1 para muestra
        }
    }
}
