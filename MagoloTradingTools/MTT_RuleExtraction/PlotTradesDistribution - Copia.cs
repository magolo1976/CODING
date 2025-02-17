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

    using System;
    using System.Collections.Generic;
    using System.Data;

    public class PlotTradeDistribution_Copia
    {
        // ... Previous code ...

        public class HistogramData
        {
            public List<double> Values { get; set; }
            public string Title { get; set; }
            public string XAxisTitle { get; set; }
            public string YAxisTitle { get; set; }
            public int SuggestedBinCount { get; set; }
        }

        public HistogramData PlotTradesDistribution(DataTable trades)
        {
            if (trades == null || trades.Rows.Count == 0 || !trades.Columns.Contains("adjusted_returns"))
            {
                return null;
            }

            // Extract adjusted returns data from DataTable
            var adjustedReturns = new List<double>();
            foreach (DataRow row in trades.Rows)
            {
                adjustedReturns.Add(Convert.ToDouble(row["adjusted_returns"]));
            }

            // Return the data required for plotting a histogram
            return new HistogramData
            {
                Values = adjustedReturns,
                Title = "Distribución de Retornos por Trade",
                XAxisTitle = "Retorno (%)",
                YAxisTitle = "Frecuencia",
                SuggestedBinCount = 50
            };
        }
    }
}
