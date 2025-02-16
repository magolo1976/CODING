namespace MTT_RuleExtraction
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;

    public class CalculateRuleMetric
    {
        /***** PYTHON 
def calculate_rule_metric(df, rule, target_column, side):
    """
    Calcula la métrica (mean/std) para una regla específica
    """
    try:
        rule_mask = df.eval(rule)
        if len(df[rule_mask]) < 30:
            return 0
            
        side_multiplier = 1 if side == 'long' else -1
        returns = df[rule_mask][target_column] * side_multiplier
        mean = returns.mean()
        std = returns.std()
        return mean/std if std != 0 else 0
    except:
        return 0        
         */

        public double Calculate_Rule_Metric(DataTable df, string rule, string targetColumn, string side)
        {
            try
            {
                // Filter data based on rule
                DataTable filteredDf = FilterByRule(df, rule);

                if (filteredDf.Rows.Count < 30)
                {
                    return 0;
                }

                double sideMultiplier = side == "long" ? 1 : -1;

                // Extract and adjust returns
                List<double> returns = GetColumnValues(filteredDf, targetColumn)
                    .Select(v => v * sideMultiplier)
                    .ToList();

                // Calculate mean and standard deviation
                double mean = returns.Average();
                double std = CalculateStandardDeviation(returns);

                return std != 0 ? mean / std : 0;
            }
            catch
            {
                return 0;
            }
        }

        private DataTable FilterByRule(DataTable data, string rule)
        {
            // This is a placeholder. In a real implementation,
            // you would use System.Linq.Dynamic.Core or another expression evaluator
            DataTable result = data.Clone();

            foreach (DataRow row in data.Rows)
            {
                if (EvaluateRule(row, rule))
                {
                    result.ImportRow(row);
                }
            }

            return result;
        }

        private bool EvaluateRule(DataRow row, string rule)
        {
            // Placeholder for rule evaluation
            // This needs to be implemented with a proper expression evaluator
            throw new NotImplementedException("Rule evaluation requires a proper expression evaluator");
        }

        private List<double> GetColumnValues(DataTable data, string columnName)
        {
            var result = new List<double>();
            foreach (DataRow row in data.Rows)
            {
                if (!(row[columnName] is DBNull))
                {
                    result.Add(Convert.ToDouble(row[columnName]));
                }
            }
            return result;
        }

        private double CalculateStandardDeviation(List<double> values)
        {
            if (values.Count <= 1)
            {
                return 0;
            }

            double avg = values.Average();
            double sum = values.Sum(d => Math.Pow(d - avg, 2));
            return Math.Sqrt(sum / (values.Count - 1));
        }
    }
}
