namespace MTT_RuleExtraction
{
    /***** PYTHON 
def calculate_backtest_metrics(df, rule, side='long'):
    """
    Calcula métricas de backtest para una regla
    """
    # Aplicar la regla
    mask = df.eval(rule)
    trades = df[mask].copy()
    
    if len(trades) == 0:
        return None
        
    # Ajustar por side
    side_multiplier = 1 if side == 'long' else -1
    trades['adjusted_returns'] = trades['Target'] * side_multiplier
    
    # Calcular métricas
    metrics = {
        'total_trades': len(trades),
        'total_return': trades['adjusted_returns'].sum(),
        'avg_return': trades['adjusted_returns'].mean(),
        'win_rate': (trades['adjusted_returns'] > 0).mean() * 100,
        'best_trade': trades['adjusted_returns'].max(),
        'worst_trade': trades['adjusted_returns'].min(),
        'std_returns': trades['adjusted_returns'].std(),
        'sharpe': trades['adjusted_returns'].mean() / trades['adjusted_returns'].std() if trades['adjusted_returns'].std() != 0 else 0
    }
    
    # Calcular drawdown
    trades['cumulative_returns'] = trades['adjusted_returns'].cumsum()
    trades['rolling_max'] = trades['cumulative_returns'].cummax()
    trades['drawdown'] = trades['rolling_max'] - trades['cumulative_returns']
    metrics['max_drawdown'] = trades['drawdown'].max()
    
    return metrics, trades
     */

    using System;
    using System.Linq;
    using System.Data;
    using System.Text.RegularExpressions;

    public class CalculateBacktestMetrics_Copia
    {
        public class BacktestMetrics
        {
            public int TotalTrades { get; set; }
            public double TotalReturn { get; set; }
            public double AvgReturn { get; set; }
            public double WinRate { get; set; }
            public double BestTrade { get; set; }
            public double WorstTrade { get; set; }
            public double StdReturns { get; set; }
            public double Sharpe { get; set; }
            public double MaxDrawdown { get; set; }
        }

        public class BacktestResult
        {
            public BacktestMetrics Metrics { get; set; }
            public DataTable Trades { get; set; }
        }

        private class SimpleExpressionEvaluator
        {
            // This is a simplified expression evaluator - in production would use a proper expression parser
            public static bool Evaluate(DataRow row, string expression)
            {
                // This is a very basic implementation that handles simple comparisons
                // For a real implementation, you'd want to use a proper expression parser
                string pattern = @"(\w+)\s*(>|<|>=|<=|==|!=)\s*(-?\d+\.?\d*)";
                var match = Regex.Match(expression, pattern);

                if (match.Success)
                {
                    string column = match.Groups[1].Value;
                    string op = match.Groups[2].Value;
                    double value = double.Parse(match.Groups[3].Value);

                    if (!row.Table.Columns.Contains(column))
                        return false;

                    double rowValue = Convert.ToDouble(row[column]);

                    switch (op)
                    {
                        case ">": return rowValue > value;
                        case "<": return rowValue < value;
                        case ">=": return rowValue >= value;
                        case "<=": return rowValue <= value;
                        case "==": return rowValue == value;
                        case "!=": return rowValue != value;
                        default: return false;
                    }
                }

                return false;
            }
        }

        public BacktestResult CalculateBacktestMetrics(DataTable df, string rule, string side = "long")
        {
            // Filter rows based on rule
            DataTable trades = df.Clone();
            foreach (DataRow row in df.Rows)
            {
                if (SimpleExpressionEvaluator.Evaluate(row, rule))
                {
                    trades.ImportRow(row);
                }
            }

            if (trades.Rows.Count == 0)
            {
                return null;
            }

            // Add adjusted_returns column
            trades.Columns.Add("adjusted_returns", typeof(double));
            int sideMultiplier = side == "long" ? 1 : -1;

            foreach (DataRow row in trades.Rows)
            {
                row["adjusted_returns"] = Convert.ToDouble(row["Target"]) * sideMultiplier;
            }

            // Calculate metrics
            double[] adjustedReturns = trades.AsEnumerable()
                .Select(row => Convert.ToDouble(row["adjusted_returns"]))
                .ToArray();

            int winningTrades = adjustedReturns.Count(r => r > 0);
            double mean = adjustedReturns.Average();
            double stdDev = CalculateStdDev(adjustedReturns);

            var metrics = new BacktestMetrics
            {
                TotalTrades = trades.Rows.Count,
                TotalReturn = adjustedReturns.Sum(),
                AvgReturn = mean,
                WinRate = (double)winningTrades / trades.Rows.Count * 100,
                BestTrade = adjustedReturns.Max(),
                WorstTrade = adjustedReturns.Min(),
                StdReturns = stdDev,
                Sharpe = stdDev != 0 ? mean / stdDev : 0
            };

            // Calculate drawdown
            trades.Columns.Add("cumulative_returns", typeof(double));
            trades.Columns.Add("rolling_max", typeof(double));
            trades.Columns.Add("drawdown", typeof(double));

            double cumSum = 0;
            double rollingMax = 0;

            foreach (DataRow row in trades.Rows)
            {
                double adjReturn = Convert.ToDouble(row["adjusted_returns"]);
                cumSum += adjReturn;
                row["cumulative_returns"] = cumSum;

                rollingMax = Math.Max(rollingMax, cumSum);
                row["rolling_max"] = rollingMax;

                row["drawdown"] = rollingMax - cumSum;
            }

            metrics.MaxDrawdown = trades.AsEnumerable()
                .Max(row => Convert.ToDouble(row["drawdown"]));

            return new BacktestResult
            {
                Metrics = metrics,
                Trades = trades
            };
        }

        private double CalculateStdDev(double[] values)
        {
            if (values.Length <= 1)
                return 0;

            double avg = values.Average();
            double sumOfSquaresOfDifferences = values.Sum(val => Math.Pow(val - avg, 2));
            double variance = sumOfSquaresOfDifferences / (values.Length - 1);
            return Math.Sqrt(variance);
        }
    }
}
