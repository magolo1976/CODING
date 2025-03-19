using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot;

namespace MagoloRuleExtraction.Classes
{
    public class Plot_KS_Test
    {
        /// <summary>
        /// Realiza el test KS y crea una visualización comparativa utilizando OxyPlot
        /// </summary>
        /// <param name="trainReturns">Lista de retornos del conjunto de entrenamiento</param>
        /// <param name="testReturns">Lista de retornos del conjunto de prueba</param>
        /// <returns>Una tupla con el PlotModel, el estadístico KS y el p-valor</returns>
        public static (PlotModel plotModel, double ksStatistic, double pValue) DoWork(List<double> trainReturns, List<double> testReturns)
        {
            // Realizar test KS
            var (ksStatistic, pValue) = CalculateKsTest(trainReturns, testReturns);

            // Crear CDFs empíricas
            var (xTrain, yTrain) = CalculateEmpiricalCdf(trainReturns);
            var (xTest, yTest) = CalculateEmpiricalCdf(testReturns);

            // Encontrar el punto de máxima diferencia
            var (xMax, y1Max, y2Max) = FindMaxDiffPoint(xTrain, yTrain, xTest, yTest);

            // Crear el modelo de gráfico
            var plotModel = new PlotModel
            {
                Title = $"Test Kolmogorov-Smirnov\nEstadístico: {ksStatistic:F3}, p-valor: {pValue:E3}",
                Subtitle = pValue < 0.05
                    ? "Las distribuciones son significativamente diferentes"
                    : "No hay evidencia de diferencias significativas"
            };

            // Configurar ejes
            plotModel.Axes.Add(new LinearAxis
            {
                Position = AxisPosition.Bottom,
                Title = "Retornos",
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Dot
            });

            plotModel.Axes.Add(new LinearAxis
            {
                Position = AxisPosition.Left,
                Title = "Probabilidad Acumulada",
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Dot,
                Minimum = 0,
                Maximum = 1
            });

            // Añadir CDF del conjunto de entrenamiento
            var trainSeries = new LineSeries
            {
                Title = "Train CDF",
                Color = OxyColors.Blue,
                StrokeThickness = 2
            };

            for (int i = 0; i < xTrain.Count; i++)
            {
                trainSeries.Points.Add(new DataPoint(xTrain[i], yTrain[i]));
            }
            plotModel.Series.Add(trainSeries);

            // Añadir CDF del conjunto de prueba
            var testSeries = new LineSeries
            {
                Title = "Test CDF",
                Color = OxyColors.Gold,
                StrokeThickness = 2
            };

            for (int i = 0; i < xTest.Count; i++)
            {
                testSeries.Points.Add(new DataPoint(xTest[i], yTest[i]));
            }
            plotModel.Series.Add(testSeries);

            // Añadir línea de máxima diferencia
            var ksDistanceSeries = new LineSeries
            {
                Title = "KS Distance",
                Color = OxyColors.Red,
                StrokeThickness = 2,
                LineStyle = LineStyle.Dash
            };

            ksDistanceSeries.Points.Add(new DataPoint(xMax, y1Max));
            ksDistanceSeries.Points.Add(new DataPoint(xMax, y2Max));
            plotModel.Series.Add(ksDistanceSeries);

            return (plotModel, ksStatistic, pValue);
        }

        /// <summary>
        /// Calcula la función de distribución acumulativa empírica (CDF)
        /// </summary>
        private static (List<double> x, List<double> y) CalculateEmpiricalCdf(List<double> data)
        {
            // Ordenar los datos
            var sortedData = data.OrderBy(d => d).ToList();

            // Calcular las probabilidades acumuladas
            var probabilities = new List<double>();
            for (int i = 0; i < sortedData.Count; i++)
            {
                probabilities.Add((i + 1.0) / sortedData.Count);
            }

            return (sortedData, probabilities);
        }

        /// <summary>
        /// Implementa el test de Kolmogorov-Smirnov para dos muestras
        /// </summary>
        private static (double ksStatistic, double pValue) CalculateKsTest(List<double> sample1, List<double> sample2)
        {
            // Obtener las CDFs empíricas
            var (x1, y1) = CalculateEmpiricalCdf(sample1);
            var (x2, y2) = CalculateEmpiricalCdf(sample2);

            // Encontrar la máxima diferencia
            var (_, _, _, maxDiff) = FindMaxDiffPointWithDiff(x1, y1, x2, y2);

            // Calcular el estadístico KS
            double ksStatistic = maxDiff;

            // Calcular el p-valor usando la aproximación asintótica
            int n = sample1.Count;
            int m = sample2.Count;
            double en = Math.Sqrt((double)n * m / (n + m));
            double pValue = CalculateKsPValue(ksStatistic, en);

            return (ksStatistic, pValue);
        }

        /// <summary>
        /// Calcula el p-valor para el estadístico KS
        /// </summary>
        private static double CalculateKsPValue(double ks, double en)
        {
            // Implementación de la aproximación asintótica para el p-valor
            // Basado en la fórmula de Kolmogorov
            double lambda = (en + 0.12 + 0.11 / en) * ks;

            // Serie para aproximar el p-valor
            double sum = 0;
            for (int i = 1; i <= 100; i++)
            {
                double term = Math.Exp(-2 * Math.Pow(i, 2) * Math.Pow(lambda, 2));
                sum += Math.Pow(-1, i - 1) * term;
            }

            double pValue = 2 * sum;

            // Limitar el p-valor entre 0 y 1
            pValue = Math.Max(0, Math.Min(1, pValue));

            return pValue;
        }

        /// <summary>
        /// Encuentra el punto donde la diferencia entre las dos CDFs es máxima
        /// </summary>
        private static (double xMax, double y1Max, double y2Max) FindMaxDiffPoint(
            List<double> x1, List<double> y1, List<double> x2, List<double> y2)
        {
            var (xMax, y1Max, y2Max, _) = FindMaxDiffPointWithDiff(x1, y1, x2, y2);
            return (xMax, y1Max, y2Max);
        }

        /// <summary>
        /// Encuentra el punto donde la diferencia entre las dos CDFs es máxima
        /// y devuelve también la diferencia máxima
        /// </summary>
        private static (double xMax, double y1Max, double y2Max, double maxDiff) FindMaxDiffPointWithDiff(
            List<double> x1, List<double> y1, List<double> x2, List<double> y2)
        {
            // Combinar todos los puntos x y ordenarlos
            var allX = x1.Concat(x2).Distinct().OrderBy(x => x).ToList();

            // Interpolar los valores y para cada conjunto
            var y1Interp = Interpolate(allX, x1, y1);
            var y2Interp = Interpolate(allX, x2, y2);

            // Encontrar la diferencia máxima
            double maxDiff = 0;
            int maxDiffIdx = 0;

            for (int i = 0; i < allX.Count; i++)
            {
                double diff = Math.Abs(y1Interp[i] - y2Interp[i]);
                if (diff > maxDiff)
                {
                    maxDiff = diff;
                    maxDiffIdx = i;
                }
            }

            return (allX[maxDiffIdx], y1Interp[maxDiffIdx], y2Interp[maxDiffIdx], maxDiff);
        }

        /// <summary>
        /// Interpolación lineal para obtener valores y correspondientes a nuevos puntos x
        /// </summary>
        private static List<double> Interpolate(List<double> newX, List<double> x, List<double> y)
        {
            List<double> result = new List<double>(newX.Count);

            foreach (double xi in newX)
            {
                // Si xi está fuera del rango de x, usar el valor más cercano
                if (xi <= x[0])
                {
                    result.Add(y[0]);
                    continue;
                }

                if (xi >= x[x.Count - 1])
                {
                    result.Add(y[y.Count - 1]);
                    continue;
                }

                // Encontrar los índices de los puntos entre los que se encuentra xi
                int idx = 0;
                while (idx < x.Count - 1 && x[idx + 1] < xi)
                {
                    idx++;
                }

                // Interpolación lineal
                double x0 = x[idx];
                double x1 = x[idx + 1];
                double y0 = y[idx];
                double y1 = y[idx + 1];

                double yi = y0 + (y1 - y0) * (xi - x0) / (x1 - x0);
                result.Add(yi);
            }

            return result;
        }
    }
}
