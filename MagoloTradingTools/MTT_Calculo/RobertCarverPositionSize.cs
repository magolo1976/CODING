using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTT_Calculo
{
    public class RobertCarverPositionSize
    {
        // Función principal para calcular el tamaño de posición ajustado por volatilidad
        public static double Calculate(List<double> returns, double targetVolatility = 0.10, int lookbackPeriod = 60, int round = 2)
        {
            if (returns == null || returns.Count < lookbackPeriod)
            {
                throw new ArgumentException("La lista de retornos debe tener al menos tantos elementos como el período de observación.");
            }

            // Paso 1: Calcular la volatilidad histórica (desviación estándar anualizada)
            double historicalVolatility = CalculateAnnualizedVolatility(returns, lookbackPeriod);

            // Paso 2: Ajustar el tamaño de la posición
            double positionSize = targetVolatility / historicalVolatility;

            // Evitar posiciones extremadamente grandes o pequeñas
            positionSize = Math.Max(0.01, Math.Min(positionSize, 2.0)); // Limitar entre 1% y 200%

            return Math.Round(positionSize, round);
        }

        // Función para calcular la volatilidad histórica anualizada
        private static double CalculateAnnualizedVolatility(List<double> returns, int lookbackPeriod)
        {
            // Tomar solo los últimos 'lookbackPeriod' retornos
            List<double> recentReturns = returns.GetRange(returns.Count - lookbackPeriod, lookbackPeriod);

            // Calcular la media de los retornos
            double meanReturn = Average(recentReturns);

            // Calcular la desviación estándar de los retornos
            double variance = 0;
            foreach (double r in recentReturns)
            {
                variance += Math.Pow(r - meanReturn, 2);
            }
            variance /= lookbackPeriod;

            double standardDeviation = Math.Sqrt(variance);

            // Anualizar la volatilidad (asumiendo 252 días hábiles en un año)
            double annualizedVolatility = standardDeviation * Math.Sqrt(252);

            return annualizedVolatility;
        }

        // Función auxiliar para calcular el promedio de una lista
        private static double Average(IEnumerable<double> values)
        {
            double sum = 0;
            int count = 0;
            foreach (var value in values)
            {
                sum += value;
                count++;
            }
            return count > 0 ? sum / count : 0;
        }

    }
}
