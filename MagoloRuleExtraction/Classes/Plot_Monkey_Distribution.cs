
using OxyPlot.Annotations;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot;

namespace MagoloRuleExtraction.Classes
{
    public class Plot_Monkey_Distribution
    {
        /// <summary>
        /// Crea un histograma de los monos con línea vertical en el umbral
        /// </summary>
        /// <param name="randomMetrics">Array con las métricas de los monos</param>
        /// <param name="threshold">Umbral para la línea vertical</param>
        /// <param name="period">Período ('train' o 'test') para indicar en el título</param>
        /// <returns>Modelo de gráfico OxyPlot</returns>
        public PlotModel DoWork(double[] randomMetrics, double threshold, double CorrelationThreshold, string period = "train")
        {
            // Crear el modelo de gráfico
            var plotModel = new PlotModel
            {
                Title = $"Monkey Test ({period})"
            };

            // Configurar eje X (métrica)
            var xAxis = new LinearAxis
            {
                Position = AxisPosition.Bottom,
                Title = "Métrica"
            };
            plotModel.Axes.Add(xAxis);

            // Configurar eje Y (frecuencia)
            var yAxis = new LinearAxis
            {
                Position = AxisPosition.Left,
                Title = "Frecuencia",
                StringFormat = "0.00%"
            };
            plotModel.Axes.Add(yAxis);

            // Calcular el histograma
            int numBins = 50;
            double min = randomMetrics.Min();
            double max = randomMetrics.Max();
            double binWidth = (max - min) / numBins;

            // Crear array de conteos para cada bin
            int[] binCounts = new int[numBins];

            foreach (double metric in randomMetrics)
            {
                int binIndex = (int)Math.Floor((metric - min) / binWidth);

                // Asegurarse de que el índice esté dentro de los límites
                if (binIndex >= 0 && binIndex < numBins)
                {
                    binCounts[binIndex]++;
                }
                else if (binIndex >= numBins)
                {
                    binCounts[numBins - 1]++;
                }
            }

            // Normalizar los conteos para obtener probabilidades
            double[] binProbabilities = binCounts.Select(count => (double)count / randomMetrics.Length).ToArray();

            // Crear serie de histograma
            var histogramSeries = new HistogramSeries
            {
                FillColor = OxyColors.Blue,
                StrokeColor = OxyColors.Black,
                StrokeThickness = 1
            };

            // Agregar bins al histograma
            for (int i = 0; i < numBins; i++)
            {
                double binStart = min + i * binWidth;
                double binEnd = binStart + binWidth;
                histogramSeries.Items.Add(new HistogramItem(binStart, binEnd, binProbabilities[i], i));
            }

            plotModel.Series.Add(histogramSeries);

            // Agregar línea vertical en el umbral
            var thresholdLine = new LineSeries
            {
                Color = OxyColors.Red,
                StrokeThickness = 2,
                LineStyle = LineStyle.Dash,
                Title = $"Umbral ({CorrelationThreshold}%): {threshold:F3}"
            };

            thresholdLine.Points.Add(new DataPoint(threshold, 0));
            thresholdLine.Points.Add(new DataPoint(threshold, binProbabilities.Max() * 60)); // Extender un poco por encima del máximo

            plotModel.Series.Add(thresholdLine);

            // Agregar anotación para el umbral
            var annotation = new TextAnnotation
            {
                Text = $"Threshold ({CorrelationThreshold}%): {threshold:0.000}",
                TextPosition = new DataPoint(threshold, binProbabilities.Max() * 60),
                TextHorizontalAlignment = OxyPlot.HorizontalAlignment.Left,
                TextVerticalAlignment = VerticalAlignment.Top,
                Background = OxyColors.White,
                TextColor = OxyColors.Red
            };

            plotModel.Annotations.Add(annotation);

            return plotModel;
        }
    }
}
