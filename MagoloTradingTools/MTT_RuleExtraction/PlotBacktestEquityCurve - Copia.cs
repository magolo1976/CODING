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
    using System.Data;

    public class PlotBacktestEquityCurve_Copia
    {
        public class EquityCurveData
        {
            public List<DateTime> Dates { get; set; }
            public List<double> CumulativeReturns { get; set; }
            public List<double> RollingMax { get; set; }
            public string Title { get; set; }
            public string XAxisTitle { get; set; }
            public string YAxisTitle { get; set; }
        }

        public EquityCurveData PlotBacktestEquityCurve(DataTable trades, string dateColumn)
        {
            if (trades == null || trades.Rows.Count == 0 || !trades.Columns.Contains(dateColumn) ||
                !trades.Columns.Contains("cumulative_returns") || !trades.Columns.Contains("rolling_max"))
            {
                return null;
            }

            // Extract data from DataTable
            var dates = new List<DateTime>();
            var cumulativeReturns = new List<double>();
            var rollingMax = new List<double>();

            foreach (DataRow row in trades.Rows)
            {
                dates.Add(Convert.ToDateTime(row[dateColumn]));
                cumulativeReturns.Add(Convert.ToDouble(row["cumulative_returns"]));
                rollingMax.Add(Convert.ToDouble(row["rolling_max"]));
            }

            // Return the data required for plotting
            return new EquityCurveData
            {
                Dates = dates,
                CumulativeReturns = cumulativeReturns,
                RollingMax = rollingMax,
                Title = "Equity Curve y High Water Mark",
                XAxisTitle = "Fecha",
                YAxisTitle = "Retorno Acumulado (%)"
            };
        }
    }
}
