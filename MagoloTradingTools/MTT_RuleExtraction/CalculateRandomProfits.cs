namespace MTT_RuleExtraction
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class CalculateRandomProfits
    {
        /***** PYTHON 
def calculate_random_profits(returns, side='long', n_simulations=1000):
    """
    Calcula los profits aleatorios para el Monkey Test usando len(train)/3
    """
    side_multiplier = 1 if side == 'long' else -1
    random_metrics = []
    sample_size = int(len(returns)/3)

    for _ in range(n_simulations):
        sample = returns.sample(n=sample_size, replace=True)
        adjusted_sample = sample * side_multiplier
        mean = adjusted_sample.mean()
        std = adjusted_sample.std()
        metric = mean/std if std != 0 else 0
        random_metrics.append(metric)

    return np.array(random_metrics)         
        */

        /// <summary>
        /// Calcula los profits aleatorios para el Monkey Test usando longitud_de_retornos/3
        /// </summary>
        public static double[] Calculate_Random_Profits(List<double> returns, string side = "long", int nSimulations = 1000)
        {
            Random random = new Random();
            double sideMultiplier = side == "long" ? 1 : -1;
            List<double> randomMetrics = new List<double>();
            int sampleSize = returns.Count / 3;

            for (int i = 0; i < nSimulations; i++)
            {
                // Tomar muestra aleatoria con reemplazo
                List<double> sample = TakeRandomSample(returns, sampleSize, random);

                // Ajustar la muestra según el lado (long/short)
                List<double> adjustedSample = sample.Select(x => x * sideMultiplier).ToList();

                // Calcular media y desviación estándar
                double mean = adjustedSample.Count > 0 ? adjustedSample.Average() : 0;
                double std = CalculateStdDev(adjustedSample);

                // Calcular métrica (Sharpe ratio simplificado)
                double metric = std != 0 ? mean / std : 0;
                randomMetrics.Add(metric);
            }

            return randomMetrics.ToArray();
        }

        /// <summary>
        /// Toma una muestra aleatoria con reemplazo de una lista
        /// </summary>
        private static List<double> TakeRandomSample(List<double> population, int sampleSize, Random random)
        {
            List<double> sample = new List<double>();

            for (int i = 0; i < sampleSize; i++)
            {
                int randomIndex = random.Next(population.Count);
                sample.Add(population[randomIndex]);
            }

            return sample;
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
