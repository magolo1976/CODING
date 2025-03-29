
using System.Data;

namespace MagoloRuleExtraction.Classes
{
    /// <summary>
    /// Clase para encontrar reglas secundarias y compuestas basadas en una regla primaria
    /// </summary>
    public class Find_Second_Rule
    {
        /// <summary>
        /// Representa una regla compuesta con sus métricas
        /// </summary>
        public class ReglaCompuesta
        {
            public List<string> Features { get; set; }
            public string Rule { get; set; }
            public double Metric { get; set; }
            public double TrainMetric { get; set; }
        }

        /// <summary>
        /// Encuentra segundas reglas analizando solo las variables que pasaron el filtro inicial
        /// </summary>
        /// <param name="trainData">DataTable con los datos de entrenamiento</param>
        /// <param name="firstFeature">Primera característica usada en la regla</param>
        /// <param name="firstRule">Primera regla aplicada</param>
        /// <param name="targetColumn">Columna objetivo para calcular las métricas</param>
        /// <param name="dateColumn">Columna de fecha</param>
        /// <param name="side">Dirección de la operación: 'long' o 'short'</param>
        /// <param name="threshold">Umbral mínimo para considerar una regla válida</param>
        /// <param name="filteredFeatures">Lista de características filtradas a considerar</param>
        /// <returns>DataTable con las reglas compuestas ordenadas por métrica de entrenamiento</returns>
        public DataTable DoWork(
            DataTable trainData,
            string firstFeature,
            string firstRule,
            string targetColumn,
            string dateColumn,
            string side,
            double threshold,
            List<string> filteredFeatures)
        {
            // Filtramos los datos según la primera regla
            DataTable submask = FilterDataTableByExpression(trainData, firstRule);

            if (submask.Rows.Count < 30)
            {
                return null;
            }

            // Eliminar la primera característica de la lista de características a analizar
            List<string> featuresToAnalyze = filteredFeatures
                .Where(f => f != firstFeature)
                .ToList();

            List<ReglaCompuesta> compoundRules = new List<ReglaCompuesta>();

            foreach (string feature in featuresToAnalyze)
            {
                try
                {
                    // Obtener los valores de la característica
                    List<double> featureValues = GetColumnAsDoubleList(submask, feature);

                    if (featureValues.Count == 0)
                        continue;

                    // Calcular los bordes de los terciles
                    double[] tercileEdges = CalculateTerciles(featureValues);

                    for (int i = 0; i < 3; i++)
                    {
                        string secondRule;

                        if (i == 0)
                        {
                            secondRule = $"`{feature}` < {tercileEdges[1]:0.000}";
                        }
                        else if (i == 1)
                        {
                            secondRule = $"`{feature}` >= {tercileEdges[1]:0.000} and `{feature}` < {tercileEdges[2]:0.000}";
                        }
                        else
                        {
                            secondRule = $"`{feature}` >= {tercileEdges[2]:0.000}";
                        }

                        string compoundRule = $"({firstRule}) and ({secondRule})";
                        DataTable ruleMask = FilterDataTableByExpression(trainData, compoundRule);

                        if (ruleMask.Rows.Count >= 30)
                        {
                            double sideMultiplier = side == "LONG" ? 1 : -1;

                            // Obtener los retornos y ajustarlos según la dirección
                            List<double> returns = GetColumnAsDoubleList(ruleMask, targetColumn)
                                .Select(r => r * sideMultiplier)
                                .ToList();

                            // Calcular la métrica
                            double mean = returns.Average();
                            double std = CalculateStandardDeviation(returns);
                            double metric = std != 0 ? (mean / std) : 0;

                            if (metric > threshold)
                            {
                                compoundRules.Add(new ReglaCompuesta
                                {
                                    Features = new List<string> { firstFeature, feature },
                                    Rule = compoundRule,
                                    Metric = metric,
                                    TrainMetric = metric
                                });
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    // Ignorar errores y continuar con la siguiente característica
                    continue;
                }
            }

            if (compoundRules.Count > 0)
            {
                // Convertir la lista de reglas a un DataTable
                DataTable resultsTable = new DataTable();
                resultsTable.Columns.Add("feature", typeof(string));
                resultsTable.Columns.Add("rule", typeof(string));
                resultsTable.Columns.Add("metric", typeof(double));
                //resultsTable.Columns.Add("train_metric", typeof(double));

                // Ordenar las reglas por train_metric en orden descendente
                foreach (ReglaCompuesta rule in compoundRules.OrderByDescending(r => r.TrainMetric))
                {
                    DataRow row = resultsTable.NewRow();
                    row["feature"] = string.Join(", ", rule.Features);
                    row["rule"] = rule.Rule;
                    row["metric"] = Math.Round(rule.Metric, 3);
                    //row["train_metric"] = rule.TrainMetric;
                    resultsTable.Rows.Add(row);
                }

                return resultsTable;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Filtra un DataTable según una expresión de consulta
        /// </summary>
        private DataTable FilterDataTableByExpression(DataTable source, string expression)
        {
            string cleanExpression = Utils.CleanExpression(expression);

            try
            {
                // Crear una vista de datos filtrada
                DataView view = new DataView(source);
                view.RowFilter = cleanExpression;

                // Para depuración
                Console.WriteLine("Expresión de filtro aplicada: " + cleanExpression);

                // Convertir la vista filtrada a un nuevo DataTable
                return view.ToTable();
            }
            catch (Exception ex)
            {
                // Capturar y registrar el error específico
                Console.WriteLine($"Error al aplicar filtro: {ex.Message}");
                Console.WriteLine($"Expresión de filtro: {cleanExpression}");
            }

            // En caso de error, devolver todos los datos sin filtrar
            return new DataView(source).ToTable();
        }

        /// <summary>
        /// Obtiene una columna del DataTable como una lista de doubles
        /// </summary>
        private List<double> GetColumnAsDoubleList(DataTable table, string columnName)
        {
            return table.AsEnumerable()
                .Select(row => Convert.ToDouble(row[columnName]))
                .ToList();
        }

        /// <summary>
        /// Calcula los terciles de una lista de valores
        /// </summary>
        private double[] CalculateTerciles(List<double> values)
        {
            List<double> sortedValues = new List<double>(values);
            sortedValues.Sort();

            int count = sortedValues.Count;
            double[] terciles = new double[4];

            terciles[0] = sortedValues.First(); // Mínimo
            terciles[1] = sortedValues[count / 3]; // Primer tercil
            terciles[2] = sortedValues[2 * count / 3]; // Segundo tercil
            terciles[3] = sortedValues.Last(); // Máximo

            // Manejar posibles duplicados asegurando que los bordes son únicos
            for (int i = 1; i < terciles.Length; i++)
            {
                if (terciles[i] <= terciles[i - 1])
                {
                    // Si hay duplicados, incrementar ligeramente el valor
                    terciles[i] = terciles[i - 1] + 0.0001;
                }
            }

            return terciles;
        }

        /// <summary>
        /// Calcula la desviación estándar de una lista de valores
        /// </summary>
        private double CalculateStandardDeviation(List<double> values)
        {
            if (values.Count <= 1)
            {
                return 0;
            }

            double avg = values.Average();
            double sumOfSquaresOfDifferences = values.Sum(val => Math.Pow(val - avg, 2));
            double variance = sumOfSquaresOfDifferences / (values.Count - 1);

            return Math.Sqrt(variance);
        }
    }
}
