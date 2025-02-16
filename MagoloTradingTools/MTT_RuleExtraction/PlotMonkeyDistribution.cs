namespace MTT_RuleExtraction
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class PlotMonkeyDistribution
    {
        /***** PYTHON 
def plot_monkey_distribution(random_metrics, threshold, period='train'):
    """
    Crea un histograma de los monos con línea vertical en el umbral
    
    Args:
        random_metrics: array con las métricas de los monos
        threshold: umbral para la línea vertical
        period: 'train' o 'test' para indicar el período
    """
    fig = go.Figure()
    
    fig.add_trace(go.Histogram(
        x=random_metrics,
        name='Random Metrics',
        nbinsx=50,
        histnorm='probability'
    ))

    fig.add_vline(
        x=threshold,
        line_dash="dash",
        line_color="red",
        annotation_text=f"Threshold (99%): {threshold:.3f}"
    )

    fig.update_layout(
        title=f'Monkey Test ({period})',
        xaxis_title='Métrica',
        yaxis_title='Frecuencia',
        showlegend=False
    )
    
    return fig
         */

        public class HistogramData
        {
            public List<double> Bins { get; private set; }
            public List<double> Frequencies { get; private set; }
            public double Threshold { get; private set; }
            public string Title { get; private set; }

            public HistogramData(List<double> bins, List<double> frequencies, double threshold, string title)
            {
                Bins = bins;
                Frequencies = frequencies;
                Threshold = threshold;
                Title = title;
            }
        }

        /// Esta implementación:
        /// Crea una clase HistogramData que contiene toda la información necesaria para generar un histograma: bins, frecuencias, umbral y título.
        /// Implementa un método PrepareMonkeyDistribution que:
        /// Calcula el rango de los datos
        /// Crea 50 bins de igual ancho
        /// Calcula las frecuencias para cada bin
        /// Normaliza las frecuencias a probabilidades
        /// Prepara el título según el período especificado
        /// Incluye un método auxiliar Calculate99PercentileThreshold para calcular el umbral del percentil 99, que podría ser útil si necesitas determinar automáticamente el umbral.
        ///
        /// --- Asumiendo que ya tienes los randomMetrics y el threshold
        /// double[] randomMetrics = /* tus métricas aleatorias */;
        /// double threshold = /* tu umbral */;
        /// var histogramData = MonkeyDistributionPlotter.PrepareMonkeyDistribution(randomMetrics, threshold, "train");
        /// --- Ahora podrías usar histogramData con cualquier biblioteca de gráficos
        /// --- que utilices en tu proyecto .NET (Windows Forms, WPF, ASP.NET, etc.)
        /// <summary>
        /// Prepara los datos para un histograma de métricas aleatorias con umbral
        /// </summary>
        public static HistogramData PrepareMonkeyDistribution(double[] randomMetrics, double threshold, string period = "train")
        {
            // Determinar el rango de los datos
            double min = randomMetrics.Min();
            double max = randomMetrics.Max();

            // Crear 50 bins
            int numBins = 50;
            double binWidth = (max - min) / numBins;

            var bins = new List<double>();
            var frequencies = new List<double>();

            // Calcular los bordes de los bins
            for (int i = 0; i <= numBins; i++)
            {
                bins.Add(min + i * binWidth);
            }

            // Calcular frecuencias (histograma)
            var histogram = new double[numBins];
            foreach (var metric in randomMetrics)
            {
                int binIndex = (int)Math.Floor((metric - min) / binWidth);
                if (binIndex >= 0 && binIndex < numBins)
                {
                    histogram[binIndex]++;
                }
            }

            // Normalizar a probabilidades
            for (int i = 0; i < numBins; i++)
            {
                frequencies.Add(histogram[i] / randomMetrics.Length);
            }

            // Crear título
            string title = $"Monkey Test ({period})";

            return new HistogramData(bins, frequencies, threshold, title);
        }

        /// <summary>
        /// Método auxiliar para encontrar el umbral del percentil 99
        /// </summary>
        public static double Calculate99PercentileThreshold(double[] randomMetrics)
        {
            var sortedMetrics = randomMetrics.OrderBy(x => x).ToArray();
            int index = (int)Math.Ceiling(sortedMetrics.Length * 0.99) - 1;
            index = Math.Max(0, Math.Min(index, sortedMetrics.Length - 1));
            return sortedMetrics[index];
        }
    }
}
