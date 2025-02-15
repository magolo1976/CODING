namespace MTT_RuleExtraction
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class AnalyzeFeature
    {
        /***** PYTHON 
def analyze_feature(df, feature, target, side):
    \"""
    Analiza una feature dividiendo en terciles y retorna la métrica del mejor tercil
    junto con su regla correspondiente
    \"""
    side_multiplier = 1 if side == 'long' else -1
    adjusted_target = df[target] * side_multiplier
    
    try:
        tercile_edges = pd.qcut(df[feature], q=3, retbins=True)[1]
        terciles = pd.qcut(df[feature], q=3, labels=[0, 1, 2], duplicates='drop')
        
        metrics = []
        for i in range(3):
            tercil_returns = adjusted_target[terciles == i]
            if len(tercil_returns) > 0:
                mean = tercil_returns.mean()
                std = tercil_returns.std()
                metric = mean/std if std != 0 else 0
                metrics.append(metric)
            else:
                metrics.append(0)
        
        best_tercil = np.argmax(metrics)
        best_metric = metrics[best_tercil]
        
        if best_tercil == 0:
            rule = f"`{feature}` < {tercile_edges[1]:.3f}"
        elif best_tercil == 1:
            rule = f"`{feature}` >= {tercile_edges[1]:.3f} and `{feature}` < {tercile_edges[2]:.3f}"
        else:
            rule = f"`{feature}` >= {tercile_edges[2]:.3f}"
        
        return best_metric, rule
    except:
        return 0        
         */

        /// <summary>
        /// Analiza una característica dividiendo en terciles y retorna la métrica del mejor tercil
        /// junto con su regla correspondiente
        /// </summary>
        public static (double bestMetric, string rule) Analyze_Feature(
            Dictionary<string, List<double>> df,
            string feature,
            string target,
            string side)
        {
            double sideMultiplier = side == "long" ? 1 : -1;
            List<double> adjustedTarget = df[target].Select(x => x * sideMultiplier).ToList();

            try
            {
                // Obtener valores de la característica
                List<double> featureValues = df[feature];

                // Calcular terciles
                (int[] tercileIndices, double[] tercileEdges) = CalculateTerciles(featureValues);

                // Calcular métricas para cada tercil
                List<double> metrics = new List<double>();
                for (int i = 0; i < 3; i++)
                {
                    var tercilReturns = new List<double>();
                    for (int j = 0; j < tercileIndices.Length; j++)
                    {
                        if (tercileIndices[j] == i)
                        {
                            tercilReturns.Add(adjustedTarget[j]);
                        }
                    }

                    if (tercilReturns.Count > 0)
                    {
                        double mean = tercilReturns.Average();
                        double std = CalculateStdDev(tercilReturns);
                        double metric = std != 0 ? mean / std : 0;
                        metrics.Add(metric);
                    }
                    else
                    {
                        metrics.Add(0);
                    }
                }

                // Encontrar el mejor tercil
                int bestTercil = metrics.IndexOf(metrics.Max());
                double bestMetric = metrics[bestTercil];

                // Generar regla
                string rule;
                if (bestTercil == 0)
                {
                    rule = $"`{feature}` < {tercileEdges[1]:F3}";
                }
                else if (bestTercil == 1)
                {
                    rule = $"`{feature}` >= {tercileEdges[1]:F3} and `{feature}` < {tercileEdges[2]:F3}";
                }
                else
                {
                    rule = $"`{feature}` >= {tercileEdges[2]:F3}";
                }

                return (bestMetric, rule);
            }
            catch
            {
                return (0, "");
            }
        }

        /// <summary>
        /// Calcula terciles y sus bordes
        /// </summary>
        private static (int[] tercileIndices, double[] tercileEdges) CalculateTerciles(List<double> values)
        {
            // Ordenar valores y mantener índices originales
            var orderedWithIndices = values.Select((v, i) => (Value: v, OriginalIndex: i))
                                           .OrderBy(x => x.Value)
                                           .ToList();

            int count = values.Count;
            int[] tercileIndices = new int[count];

            // Calcular tamaño de cada tercil
            int tercileSize = count / 3;
            int remainder = count % 3;

            // Asignar terciles
            int currentIndex = 0;
            for (int tercil = 0; tercil < 3; tercil++)
            {
                int currentTercileSize = tercileSize + (tercil < remainder ? 1 : 0);

                for (int i = 0; i < currentTercileSize; i++)
                {
                    if (currentIndex < count)
                    {
                        tercileIndices[orderedWithIndices[currentIndex].OriginalIndex] = tercil;
                        currentIndex++;
                    }
                }
            }

            // Calcular bordes de terciles
            double[] tercileEdges = new double[4];
            tercileEdges[0] = orderedWithIndices.First().Value;
            tercileEdges[3] = orderedWithIndices.Last().Value;

            int firstTercileSize = tercileSize + (0 < remainder ? 1 : 0);
            int secondTercileSize = tercileSize + (1 < remainder ? 1 : 0);

            tercileEdges[1] = orderedWithIndices[firstTercileSize - 1].Value;
            tercileEdges[2] = orderedWithIndices[firstTercileSize + secondTercileSize - 1].Value;

            // Manejar valores duplicados en los bordes
            if (tercileEdges[1] == tercileEdges[2])
            {
                // En caso de duplicados, intentamos distribuir más equitativamente
                var distinctValues = orderedWithIndices.Select(x => x.Value).Distinct().OrderBy(x => x).ToList();
                if (distinctValues.Count >= 3)
                {
                    int distinctCount = distinctValues.Count;
                    tercileEdges[1] = distinctValues[distinctCount / 3];
                    tercileEdges[2] = distinctValues[2 * distinctCount / 3];
                }
            }

            return (tercileIndices, tercileEdges);
        }

        /// <summary>
        /// Calcula la desviación estándar de una lista de valores
        /// </summary>
        private static double CalculateStdDev(List<double> values)
        {
            if (values.Count <= 1)
                return 0;

            double avg = values.Average();
            double sumOfSquaresOfDifferences = values.Sum(val => Math.Pow(val - avg, 2));
            return Math.Sqrt(sumOfSquaresOfDifferences / (values.Count - 1));
        }
    }
}
