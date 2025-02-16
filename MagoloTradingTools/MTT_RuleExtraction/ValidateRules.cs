namespace MTT_RuleExtraction
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;

    public class ValidateRules
    {
        /***** PYTHON 
def validate_rules(df_test, rules_df, target_column, side, random_metrics):
    """
    Valida las reglas contra la distribución de monos en test
    """
    validation_results = []
    
    for _, row in rules_df.iterrows():
        rule = row['rule']
        train_metric = row['metric'] if 'metric' in row else 0
        
        # Calcular métrica en test
        test_metric = calculate_rule_metric(df_test, rule, target_column, side)
        # Calcular porcentaje de monos superados
        monkey_percentile = (test_metric > random_metrics).mean() * 100
        
        validation_results.append({
            'rule': rule,
            'train_metric': train_metric,
            'test_metric': test_metric,
            'validation': monkey_percentile
        })
    
    return pd.DataFrame(validation_results)        
         */

        public class ValidationResult
        {
            public string Rule { get; set; }
            public double TrainMetric { get; set; }
            public double TestMetric { get; set; }
            public double Validation { get; set; }
        }

        public List<ValidationResult> Validate_Rules(
            DataTable dfTest,
            List<Dictionary<string, object>> rulesDf,
            string targetColumn,
            string side,
            List<double> randomMetrics)
        {
            var validationResults = new List<ValidationResult>();

            foreach (var row in rulesDf)
            {
                string rule = row["rule"].ToString();
                double trainMetric = row.ContainsKey("metric") ? Convert.ToDouble(row["metric"]) : 0;

                // Calculate metric on test data
                double testMetric = CalculateRuleMetric(dfTest, rule, targetColumn, side);

                // Calculate percentage of random metrics exceeded
                double monkeyPercentile = randomMetrics.Count(m => testMetric > m) * 100.0 / randomMetrics.Count;

                validationResults.Add(new ValidationResult
                {
                    Rule = rule,
                    TrainMetric = trainMetric,
                    TestMetric = testMetric,
                    Validation = monkeyPercentile
                });
            }

            return validationResults;
        }

        public double CalculateRuleMetric(DataTable df, string rule, string targetColumn, string side)
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
