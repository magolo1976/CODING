
namespace MagoloRuleExtraction.Classes
{
    /// <summary>
    /// Clase que calcula métricas aleatorias para evaluar reglas compuestas utilizando simulaciones tipo Monte Carlo
    /// </summary>
    public class Calculate_Random_Metrics_Compound
    {
        private readonly Random _random;

        public Calculate_Random_Metrics_Compound()
        {
            _random = new Random();
        }

        /// <summary>
        /// Calcula métricas aleatorias para reglas compuestas
        /// </summary>
        /// <param name="returns">Lista de retornos</param>
        /// <param name="side">Dirección de la operación: 'long' o 'short'</param>
        /// <param name="nSimulations">Número de simulaciones a realizar</param>
        /// <returns>Array con las métricas aleatorias calculadas</returns>
        public double[] DoWork(List<double> returns, string side = "LONG", int nSimulations = 1000)
        {
            double sideMultiplier = side == "LONG" ? 1 : -1;
            double[] randomMetrics = new double[nSimulations];
            int sampleSize = returns.Count / 9;

            for (int i = 0; i < nSimulations; i++)
            {
                // Tomar muestra aleatoria con reemplazo
                List<double> sample = TakeRandomSampleWithReplacement(returns, sampleSize);

                // Ajustar la muestra según la dirección
                List<double> adjustedSample = sample.Select(x => x * sideMultiplier).ToList();

                // Calcular media y desviación estándar
                double mean = adjustedSample.Average();
                double std = CalculateStandardDeviation(adjustedSample);

                // Calcular métrica
                double metric = std != 0 ? mean / std : 0;
                randomMetrics[i] = metric;
            }

            return randomMetrics;
        }

        /// <summary>
        /// Toma una muestra aleatoria con reemplazo de una lista
        /// </summary>
        private List<double> TakeRandomSampleWithReplacement(List<double> population, int sampleSize)
        {
            List<double> sample = new List<double>(sampleSize);

            for (int i = 0; i < sampleSize; i++)
            {
                int randomIndex = _random.Next(0, population.Count);
                sample.Add(population[randomIndex]);
            }

            return sample;
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
