
using OxyPlot.Annotations;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot;
using OxyPlot.WindowsForms;

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
        public PlotModel DoWork(double[] randomMetrics, double threshold, string period = "train")
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
                LineStyle = LineStyle.Dash
                //IsVisibleInLegend = false
            };

            thresholdLine.Points.Add(new DataPoint(threshold, 0));
            thresholdLine.Points.Add(new DataPoint(threshold, binProbabilities.Max() * 1.1)); // Extender un poco por encima del máximo

            plotModel.Series.Add(thresholdLine);

            // Agregar anotación para el umbral
            var annotation = new OxyPlot.Annotations.TextAnnotation
            {
                Text = $"Threshold (99%): {threshold:0.000}",
                TextPosition = new DataPoint(threshold, binProbabilities.Max() * 1.05),
                TextHorizontalAlignment = OxyPlot.HorizontalAlignment.Left,
                TextVerticalAlignment = OxyPlot.VerticalAlignment.Top,
                Background = OxyColors.White,
                TextColor = OxyColors.Red
            };

            plotModel.Annotations.Add(annotation);

            return plotModel;
        }

        /// <summary>
        /// Método auxiliar para crear un control PlotView a partir del modelo
        /// Útil para integrar directamente en Windows Forms
        /// </summary>
        public PlotView CreatePlotView(PlotModel plotModel)
        {
            var plotView = new PlotView
            {
                Model = plotModel,
                Dock = System.Windows.Forms.DockStyle.Fill
            };

            return plotView;
        }

        //    /// <summary>
        //    /// Crea un histograma de los monos con línea vertical en el umbral
        //    /// </summary>
        //    /// <param name="randomMetrics">Array con las métricas de los monos</param>
        //    /// <param name="threshold">Umbral para la línea vertical</param>
        //    /// <param name="period">Período ('train' o 'test') para indicar el título</param>
        //    /// <returns>Modelo de gráfico PlotModel</returns>
        //    public PlotModel DoWork(double[] randomMetrics, double threshold, string period = "train")
        //    {
        //        // Crear el modelo de gráfico
        //        PlotModel plotModel = new PlotModel
        //        {
        //            Title = $"Monkey Test ({period})"
        //        };

        //        // Añadir ejes
        //        plotModel.Axes.Add(new LinearAxis
        //        {
        //            Position = AxisPosition.Bottom,
        //            Title = "Métrica"
        //        });

        //        plotModel.Axes.Add(new LinearAxis
        //        {
        //            Position = AxisPosition.Left,
        //            Title = "Frecuencia"
        //        });

        //        // Crear histograma
        //        var histogramSeries = CreateHistogram(randomMetrics);
        //        plotModel.Series.Add(histogramSeries);

        //        // Añadir línea vertical en el umbral
        //        var thresholdLine = new LineAnnotation
        //        {
        //            Type = LineAnnotationType.Vertical,
        //            X = threshold,
        //            LineStyle = LineStyle.Dash,
        //            Color = OxyColors.Red,
        //            Text = $"Threshold (99%): {threshold:0.000}",
        //            TextPosition = new DataPoint(threshold, 0.02),
        //            TextOrientation = 0
        //        };

        //        plotModel.Annotations.Add(thresholdLine);

        //        return plotModel;
        //    }

        //    /// <summary>
        //    /// Crea una serie de histograma para los datos proporcionados
        //    /// </summary>
        //    private HistogramSeries CreateHistogram(double[] data)
        //    {
        //        // Calcular el rango de los datos
        //        double min = data.Min();
        //        double max = data.Max();

        //        // Calcular el ancho de los bins (usando la regla de Sturges)
        //        int numBins = (int)Math.Ceiling(Math.Log(data.Length, 2) + 1);
        //        double binWidth = (max - min) / numBins;

        //        // Crear serie de histograma
        //        var histSeries = new HistogramSeries
        //        {
        //            StrokeColor = OxyColors.Blue,
        //            FillColor = OxyColor.FromArgb(128, 0, 0, 255),
        //            StrokeThickness = 1,
        //            BinWidth = binWidth,
        //            // Normalizar para probabilidad
        //            NormalizedHistogram = true
        //        };

        //        // Contar frecuencias
        //        double[] binCounts = new double[numBins];
        //        double[] binCenters = new double[numBins];

        //        for (int i = 0; i < numBins; i++)
        //        {
        //            double binStart = min + i * binWidth;
        //            double binEnd = binStart + binWidth;
        //            binCenters[i] = binStart + binWidth / 2;

        //            // Contar valores en este bin
        //            binCounts[i] = data.Count(x => x >= binStart && x < binEnd);
        //        }

        //        // El último bin incluye el valor máximo
        //        binCounts[numBins - 1] += data.Count(x => x == max);

        //        // Normalizar las frecuencias
        //        double totalCount = binCounts.Sum();
        //        double[] normalizedCounts = binCounts.Select(c => c / totalCount).ToArray();

        //        // Añadir datos al histograma
        //        for (int i = 0; i < numBins; i++)
        //        {
        //            histSeries.Items.Add(new HistogramItem(binCenters[i], normalizedCounts[i]));
        //        }

        //        return histSeries;
        //    }
    }
}
