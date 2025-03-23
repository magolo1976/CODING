
using System.Data;
using System.Text.RegularExpressions;

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
                            double sideMultiplier = side == "long" ? 1 : -1;

                            // Obtener los retornos y ajustarlos según la dirección
                            List<double> returns = GetColumnAsDoubleList(ruleMask, targetColumn)
                                .Select(r => r * sideMultiplier)
                                .ToList();

                            // Calcular la métrica
                            double mean = returns.Average();
                            double std = CalculateStandardDeviation(returns);
                            double metric = std != 0 ? mean / std : 0;

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
                resultsTable.Columns.Add("train_metric", typeof(double));

                // Ordenar las reglas por train_metric en orden descendente
                foreach (ReglaCompuesta rule in compoundRules.OrderByDescending(r => r.TrainMetric))
                {
                    DataRow row = resultsTable.NewRow();
                    row["feature"] = string.Join(", ", rule.Features);
                    row["rule"] = rule.Rule;
                    row["metric"] = rule.Metric;
                    row["train_metric"] = rule.TrainMetric;
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
            // Asegurarse de que los nombres de columna estén formateados correctamente
            string cleanExpression = expression.Replace("`", "");

            // Corregir el formato de los números (reemplazar comas por puntos)
            cleanExpression = Regex.Replace(cleanExpression,
                @"([-]?\d+),(\d+)",
                "$1.$2");

            // Eliminar separadores de miles si existen
            cleanExpression = Regex.Replace(cleanExpression,
                @"(\d),(\d{3}[^\d])",
                "$1$2");

            try
            {
                //// Verificar si los nombres de columna necesitan estar entre corchetes
                //foreach (DataColumn column in source.Columns)
                //{
                //    if (column.ColumnName.Contains(" ") || !char.IsLetter(column.ColumnName[0]))
                //    {
                //        // Si el nombre contiene espacios o no comienza con una letra
                //        string bracketedName = $"[{column.ColumnName}]";
                //        // Reemplazar solo ocurrencias completas (no dentro de otras palabras)
                //        cleanExpression = Regex.Replace(cleanExpression,
                //            $"\\b{Regex.Escape(column.ColumnName)}\\b",
                //            bracketedName);
                //    }
                //}

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

        ///// <summary>
        ///// Resultado de una regla compuesta encontrada
        ///// </summary>
        //public class CompoundRule
        //{
        //    public List<string> Features { get; set; }
        //    public string Rule { get; set; }
        //    public double Metric { get; set; }
        //    public double TrainMetric { get; set; }
        //}

        ///// <summary>
        ///// Encuentra reglas secundarias analizando solo las variables que pasaron el filtro inicial
        ///// </summary>
        ///// <param name="trainData">DataTable con los datos de entrenamiento</param>
        ///// <param name="firstFeature">Nombre de la primera característica</param>
        ///// <param name="firstRule">Regla basada en la primera característica</param>
        ///// <param name="targetColumn">Nombre de la columna objetivo</param>
        ///// <param name="dateColumn">Nombre de la columna de fecha</param>
        ///// <param name="side">Dirección del trading ('long' o 'short')</param>
        ///// <param name="threshold">Umbral mínimo para aceptar una regla</param>
        ///// <param name="filteredFeatures">Lista de características filtradas a analizar</param>
        ///// <returns>DataTable con las reglas compuestas encontradas o null si no hay resultados</returns>
        //public DataTable DoWork(
        //    DataTable trainData,
        //    string firstFeature,
        //    string firstRule,
        //    string targetColumn,
        //    string dateColumn,
        //    string side,
        //    double threshold,
        //    List<string> filteredFeatures)
        //{
        //    // Filtrar los datos según la primera regla
        //    DataTable submask = FilterDataByRule(trainData, firstRule);

        //    if (submask.Rows.Count < 30)
        //    {
        //        return null;
        //    }

        //    // Eliminar la primera característica de la lista a analizar
        //    List<string> featuresToAnalyze = filteredFeatures
        //        .Where(f => f != firstFeature)
        //        .ToList();

        //    List<CompoundRule> compoundRules = new List<CompoundRule>();
        //    double sideMultiplier = side == "long" ? 1.0 : -1.0;

        //    foreach (string feature in featuresToAnalyze)
        //    {
        //        try
        //        {
        //            // Extraer la columna para los cálculos de terciles
        //            double[] values = submask.AsEnumerable()
        //                .Select(row => Convert.ToDouble(row[feature]))
        //                .ToArray();

        //            // Calcular terciles (equivalente a pd.qcut)
        //            double[] tercileEdges = CalculateTerciles(values);

        //            for (int i = 0; i < 3; i++)
        //            {
        //                string secondRule;

        //                if (i == 0)
        //                {
        //                    secondRule = $"`{feature}` < {tercileEdges[1]:0.000}";
        //                }
        //                else if (i == 1)
        //                {
        //                    secondRule = $"`{feature}` >= {tercileEdges[1]:0.000} and `{feature}` < {tercileEdges[2]:0.000}";
        //                }
        //                else
        //                {
        //                    secondRule = $"`{feature}` >= {tercileEdges[2]:0.000}";
        //                }

        //                string compoundRule = $"({firstRule}) and ({secondRule})";
        //                DataTable ruleMask = FilterDataByRule(trainData, compoundRule);

        //                if (ruleMask.Rows.Count >= 30)
        //                {
        //                    // Calcular retornos ajustados según el lado
        //                    double[] returns = ruleMask.AsEnumerable()
        //                        .Select(row => Convert.ToDouble(row[targetColumn]) * sideMultiplier)
        //                        .ToArray();

        //                    double mean = returns.Average();
        //                    double std = CalculateStandardDeviation(returns);
        //                    double metric = std != 0 ? mean / std : 0;

        //                    if (metric > threshold)
        //                    {
        //                        compoundRules.Add(new CompoundRule
        //                        {
        //                            Features = new List<string> { firstFeature, feature },
        //                            Rule = compoundRule,
        //                            Metric = metric,
        //                            TrainMetric = metric
        //                        });
        //                    }
        //                }
        //            }
        //        }
        //        catch (Exception)
        //        {
        //            // Continuar con la siguiente característica si hay error
        //            continue;
        //        }
        //    }

        //    if (compoundRules.Count > 0)
        //    {
        //        // Crear DataTable con los resultados
        //        DataTable results = new DataTable();
        //        results.Columns.Add("feature", typeof(string));
        //        results.Columns.Add("rule", typeof(string));
        //        results.Columns.Add("metric", typeof(double));
        //        results.Columns.Add("train_metric", typeof(double));

        //        foreach (var rule in compoundRules.OrderByDescending(r => r.TrainMetric))
        //        {
        //            DataRow row = results.NewRow();
        //            row["feature"] = string.Join(", ", rule.Features);
        //            row["rule"] = rule.Rule;
        //            row["metric"] = rule.Metric;
        //            row["train_metric"] = rule.TrainMetric;
        //            results.Rows.Add(row);
        //        }

        //        return results;
        //    }

        //    return null;
        //}

        ///// <summary>
        ///// Filtra datos según una regla expresada como condición
        ///// </summary>
        ///// <param name="data">DataTable con los datos originales</param>
        ///// <param name="rule">Regla expresada como condición</param>
        ///// <returns>DataTable filtrado según la regla</returns>
        //private DataTable FilterDataByRule(DataTable data, string rule)
        //{
        //    // Convertir la regla a formato de expresión compatible con DataTable
        //    string expression = ConvertRuleToExpression(rule);

        //    // Filtrar usando DataView
        //    DataView view = new DataView(data);
        //    view.RowFilter = expression;

        //    return view.ToTable();
        //}

        ///// <summary>
        ///// Convierte una regla en formato Python a una expresión compatible con DataView.RowFilter
        ///// </summary>
        //private string ConvertRuleToExpression(string rule)
        //{
        //    // Reemplazar comillas invertidas y operadores lógicos
        //    string expression = rule.Replace("`", "")
        //                           .Replace(" and ", " AND ")
        //                           .Replace(" or ", " OR ");

        //    return expression;
        //}

        ///// <summary>
        ///// Calcula los puntos de corte para terciles (similar a pd.qcut)
        ///// </summary>
        //private double[] CalculateTerciles(double[] values)
        //{
        //    if (values.Length == 0)
        //        return new double[0];

        //    // Ordenar valores
        //    Array.Sort(values);

        //    double[] terciles = new double[4];
        //    terciles[0] = values.Min();
        //    terciles[3] = values.Max();

        //    // Calcular puntos de corte para 33% y 66%
        //    int n = values.Length;
        //    terciles[1] = values[(int)(n * 0.333)];
        //    terciles[2] = values[(int)(n * 0.667)];

        //    return terciles;
        //}

        ///// <summary>
        ///// Calcula la desviación estándar de un array de valores
        ///// </summary>
        //private double CalculateStandardDeviation(double[] values)
        //{
        //    if (values.Length <= 1)
        //        return 0;

        //    double avg = values.Average();
        //    double sum = values.Sum(x => Math.Pow(x - avg, 2));
        //    return Math.Sqrt(sum / (values.Length - 1));
        //}
    }
}
