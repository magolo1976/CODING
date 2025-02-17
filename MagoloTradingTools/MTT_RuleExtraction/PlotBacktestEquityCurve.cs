namespace MTT_RuleExtraction
{
    /***** PYTHON 
def plot_backtest_equity_curve(trades, date_column):
    """
    Crea un gráfico de la equity curve con drawdown
    """
    fig = go.Figure()
    
    # Equity curve
    fig.add_trace(go.Scatter(
        x=trades[date_column],
        y=trades['cumulative_returns'],
        name='Equity Curve',
        line=dict(color='blue')
    ))
    
    # Rolling maximum
    fig.add_trace(go.Scatter(
        x=trades[date_column],
        y=trades['rolling_max'],
        name='High Water Mark',
        line=dict(color='green', dash='dot')
    ))
    
    fig.update_layout(
        title='Equity Curve y High Water Mark',
        xaxis_title='Fecha',
        yaxis_title='Retorno Acumulado (%)',
        showlegend=True
    )
    
    return fig
     */

    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class TradeData
    {
        public DateTime Date { get; set; }
        public double CumulativeReturns { get; set; }
    }

    public class PlotBacktestEquityCurve
    {
        private List<TradeData> trades;

        public PlotBacktestEquityCurve(List<TradeData> trades)
        {
            this.trades = trades;
        }

        public (List<DateTime> Dates, List<double> CumulativeReturns, List<double> RollingMax) CalculateEquityCurve()
        {
            // Ensure trades are sorted by date
            trades = trades.OrderBy(t => t.Date).ToList();

            // Calculate rolling maximum of cumulative returns
            List<double> rollingMax = new List<double>();
            double currentMax = double.MinValue;

            foreach (var trade in trades)
            {
                currentMax = Math.Max(currentMax, trade.CumulativeReturns);
                rollingMax.Add(currentMax);
            }

            // Prepare the data to return
            List<DateTime> dates = trades.Select(t => t.Date).ToList();
            List<double> cumulativeReturns = trades.Select(t => t.CumulativeReturns).ToList();

            return (dates, cumulativeReturns, rollingMax);
        }
    }
}
