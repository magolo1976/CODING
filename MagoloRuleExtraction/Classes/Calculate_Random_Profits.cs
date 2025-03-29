namespace MagoloRuleExtraction.Classes
{
    public class Calculate_Random_Profits
    {
        private readonly Random _random = new Random();

        /// <summary>
        /// Calcula los profits aleatorios para el Monkey Test usando len(train)/3
        /// </summary>
        /// <param name="returns">Lista de retornos</param>
        /// <param name="side">Dirección de la operación: 'long' o 'short'</param>
        /// <param name="nSimulations">Número de simulaciones a realizar</param>
        /// <returns>Array de métricas aleatorias</returns>
        public double[] DoWork(List<double> returns, string side = "LONG", int nSimulations = 1000)
        {
            double sideMultiplier = side == "LONG" ? 1 : -1;
            double[] randomMetrics = new double[nSimulations];
            int sampleSize = returns.Count / 3;

            for (int i = 0; i < nSimulations; i++)
            {
                // Tomar una muestra aleatoria con reemplazo
                List<double> sample = SampleWithReplacement(returns, sampleSize);

                // Ajustar la muestra según el lado
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
        private List<double> SampleWithReplacement(List<double> population, int sampleSize)
        {
            List<double> sample = new List<double>(sampleSize);
            int populationSize = population.Count;

            for (int i = 0; i < sampleSize; i++)
            {
                int randomIndex = _random.Next(populationSize);
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
                return 0;

            double avg = values.Average();
            double sumOfSquaresOfDifferences = values.Select(val => (val - avg) * (val - avg)).Sum();
            double variance = sumOfSquaresOfDifferences / (values.Count - 1);
            return Math.Sqrt(variance);
        }
    }
}
