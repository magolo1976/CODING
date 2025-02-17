namespace MTT_RuleExtraction
{
    /***** PYTHON 
def plot_trades_distribution(trades):
    """
    Crea un histograma de los retornos de los trades
    """
    fig = go.Figure()
    
    fig.add_trace(go.Histogram(
        x=trades['adjusted_returns'],
        nbinsx=50,
        name='Distribución de Retornos'
    ))
    
    fig.update_layout(
        title='Distribución de Retornos por Trade',
        xaxis_title='Retorno (%)',
        yaxis_title='Frecuencia',
        showlegend=False
    )
    
    return fig 
     */

    using System.Linq;

    public class PlotTradeDistribution
    {
        public double[] AdjustedReturns { get; set; }

        public PlotTradeDistribution(double[] adjustedReturns)
        {
            this.AdjustedReturns = adjustedReturns;
        }

        public HistogramData PlotTradesDistribution(int numberOfBins = 50)
        {
            // Create bins for the histogram
            var minReturn = AdjustedReturns.Min();
            var maxReturn = AdjustedReturns.Max();
            var binWidth = (maxReturn - minReturn) / numberOfBins;

            // Initialize histogram data
            double[] bins = new double[numberOfBins];
            int[] frequencies = new int[numberOfBins];

            // Populate the histogram
            foreach (var returnValue in AdjustedReturns)
            {
                int binIndex = (int)((returnValue - minReturn) / binWidth);
                if (binIndex >= 0 && binIndex < numberOfBins)
                {
                    frequencies[binIndex]++;
                }
            }

            // Prepare and return the histogram data
            return new HistogramData
            {
                Bins = Enumerable.Range(0, numberOfBins).Select(i => minReturn + i * binWidth).ToArray(),
                Frequencies = frequencies,
                Title = "Distribución de Retornos por Trade",
                XAxisTitle = "Retorno (%)",
                YAxisTitle = "Frecuencia"
            };
        }
    }

    public class HistogramData
    {
        public double[] Bins { get; set; }
        public int[] Frequencies { get; set; }
        public string Title { get; set; }
        public string XAxisTitle { get; set; }
        public string YAxisTitle { get; set; }
    }
}
