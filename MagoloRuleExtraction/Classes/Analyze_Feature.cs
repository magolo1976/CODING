using System.Data;

namespace MagoloRuleExtraction.Classes
{
    public class Analyze_Feature
    {
        /// <summary>
        /// Analiza una característica dividiendo en terciles y retorna la métrica del mejor tercil
        /// junto con su regla correspondiente
        /// </summary>
        /// <param name="dataFrame">DataTable con los datos</param>
        /// <param name="feature">Nombre de la característica a analizar</param>
        /// <param name="target">Nombre de la columna objetivo</param>
        /// <param name="side">Dirección de la operación: 'long' o 'short'</param>
        /// <returns>Tupla con la mejor métrica y su regla correspondiente</returns>
        public Tuple<double, string> DoWork(DataTable dataFrame, string feature, string target, string side)
        {
            double sideMultiplier = side == "long" ? 1 : -1;

            try
            {
                // Extraer valores de la característica
                List<double> featureValues = ExtractColumnValues(dataFrame, feature);

                // Calcular terciles
                double[] tercileEdges = CalculateTercileEdges(featureValues);
                List<int> terciles = AssignTerciles(featureValues, tercileEdges);

                // Extraer valores objetivo ajustados
                List<double> targetValues = ExtractColumnValues(dataFrame, target)
                    .Select(v => v * sideMultiplier)
                    .ToList();

                // Calcular métricas para cada tercil
                double[] metrics = new double[3];
                for (int i = 0; i < 3; i++)
                {
                    List<double> tercilReturns = new List<double>();
                    for (int j = 0; j < terciles.Count; j++)
                    {
                        if (terciles[j] == i)
                        {
                            tercilReturns.Add(targetValues[j]);
                        }
                    }

                    if (tercilReturns.Count > 0)
                    {
                        double mean = tercilReturns.Average();
                        double std = CalculateStandardDeviation(tercilReturns);
                        metrics[i] = std != 0 ? mean / std : 0;
                    }
                    else
                    {
                        metrics[i] = 0;
                    }
                }

                // Encontrar el mejor tercil
                int bestTercil = Array.IndexOf(metrics, metrics.Max());
                double bestMetric = metrics[bestTercil];

                // Generar regla para el mejor tercil
                string rule;
                if (bestTercil == 0)
                {
                    rule = $"`{feature}` < {tercileEdges[1]:0.###}";
                }
                else if (bestTercil == 1)
                {
                    rule = $"`{feature}` >= {tercileEdges[1]:0.###} and `{feature}` < {tercileEdges[2]:0.###}";
                }
                else
                {
                    rule = $"`{feature}` >= {tercileEdges[2]:0.###}";
                }

                return new Tuple<double, string>(bestMetric, rule);
            }
            catch
            {
                return new Tuple<double, string>(0, "");
            }
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

        /// <summary>
        /// Calcula los bordes de los terciles
        /// </summary>
        private double[] CalculateTercileEdges(List<double> values)
        {
            List<double> sortedValues = new List<double>(values);
            sortedValues.Sort();

            int count = sortedValues.Count;
            double[] edges = new double[4];

            edges[0] = sortedValues.First();
            edges[1] = sortedValues[count / 3];
            edges[2] = sortedValues[2 * count / 3];
            edges[3] = sortedValues.Last();

            return edges;
        }

        /// <summary>
        /// Asigna terciles a los valores basándose en los bordes calculados
        /// </summary>
        private List<int> AssignTerciles(List<double> values, double[] edges)
        {
            List<int> terciles = new List<int>(values.Count);

            foreach (double value in values)
            {
                if (value < edges[1])
                {
                    terciles.Add(0);
                }
                else if (value < edges[2])
                {
                    terciles.Add(1);
                }
                else
                {
                    terciles.Add(2);
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
                return 0;

            double avg = values.Average();
            double sumOfSquaresOfDifferences = values.Select(val => (val - avg) * (val - avg)).Sum();
            double variance = sumOfSquaresOfDifferences / (values.Count - 1);
            return Math.Sqrt(variance);
        }
    }
}
