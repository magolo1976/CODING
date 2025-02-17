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
    using System.Collections.Generic;
    using System.Linq;

    public class CalculateBacktestMetrics
    {
        public class Trade
        {
            public double Target { get; set; }
            public double AdjustedReturns { get; set; }
            public double CumulativeReturns { get; set; }
            public double RollingMax { get; set; }
            public double Drawdown { get; set; }
        }

        public class Metrics
        {
            public int TotalTrades { get; set; }
            public double TotalReturn { get; set; }
            public double AvgReturn { get; set; }
            public double WinRate { get; set; }
            public double BestTrade { get; set; }
            public double WorstTrade { get; set; }
            public double StdReturns { get; set; }
            public double SharpeRatio { get; set; }
            public double MaxDrawdown { get; set; }
        }

        public static (Metrics metrics, List<Trade> trades) Calculate_Backtest_Metrics(List<Dictionary<string, object>> df, string rule, string side = "long")
        {
            // Applying the rule
            var trades = df.Where(row => EvaluateRule(row, rule)).Select(row => new Trade
            {
                Target = Convert.ToDouble(row["Target"])
            }).ToList();

            if (trades.Count == 0)
            {
                return (null, null);
            }

            // Adjust for side
            double sideMultiplier = side == "long" ? 1 : -1;
            foreach (var trade in trades)
            {
                trade.AdjustedReturns = trade.Target * sideMultiplier;
            }

            // Calculate metrics
            Metrics metrics = new Metrics
            {
                TotalTrades = trades.Count,
                TotalReturn = trades.Sum(t => t.AdjustedReturns),
                AvgReturn = trades.Average(t => t.AdjustedReturns),
                WinRate = trades.Count(t => t.AdjustedReturns > 0) * 100.0 / trades.Count,
                BestTrade = trades.Max(t => t.AdjustedReturns),
                WorstTrade = trades.Min(t => t.AdjustedReturns),
                StdReturns = CalculateStandardDeviation(trades.Select(t => t.AdjustedReturns).ToList())
            };

            metrics.SharpeRatio = metrics.StdReturns != 0 ? metrics.AvgReturn / metrics.StdReturns : 0;

            // Calculate drawdown
            double cumulativeReturns = 0;
            double rollingMax = double.MinValue;
            double maxDrawdown = 0;

            foreach (var trade in trades)
            {
                cumulativeReturns += trade.AdjustedReturns;
                trade.CumulativeReturns = cumulativeReturns;

                if (cumulativeReturns > rollingMax)
                    rollingMax = cumulativeReturns;

                trade.RollingMax = rollingMax;
                trade.Drawdown = rollingMax - cumulativeReturns;

                if (trade.Drawdown > maxDrawdown)
                    maxDrawdown = trade.Drawdown;
            }

            metrics.MaxDrawdown = maxDrawdown;

            return (metrics, trades);
        }

        private static double CalculateStandardDeviation(List<double> values)
        {
            if (values.Count == 0) return 0;

            double avg = values.Average();
            double sumSqrdDiffs = values.Select(v => (v - avg) * (v - avg)).Sum();
            return Math.Sqrt(sumSqrdDiffs / values.Count);
        }

        private static bool EvaluateRule(Dictionary<string, object> row, string rule)
        {
            // For a simple implementation, we will evaluate a rule that is an expression involving the "Target" key.
            // Evaluate example: "Target > 2" or more complex expressions might require using a more capable evaluator.
            // Here we will keep it simple for illustrative purposes.
            string targetValueStr = row["Target"].ToString();
            double targetValue = Convert.ToDouble(targetValueStr);

            // Handling a simple rule evaluation; this is a placeholder.
            if (rule.StartsWith("Target"))
            {
                var parts = rule.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length == 3 && double.TryParse(parts[2], out double compareValue))
                {
                    switch (parts[1])
                    {
                        case ">":
                            return targetValue > compareValue;
                        case "<":
                            return targetValue < compareValue;
                        case "=":
                            return targetValue == compareValue;
                            // Add more cases as needed
                    }
                }
            }
            // For complex rules, consider building a proper expression evaluation mechanism.
            return false;
        }
    }
}
