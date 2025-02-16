namespace MTT_RuleExtraction
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;

    public class FindSecondRule
    {
        /***** PYTHON 
def find_second_rule(train_data, first_feature, first_rule, target_column, date_column, side, threshold, filtered_features):
    """
    Encuentra segundas reglas analizando solo las variables que pasaron el filtro inicial
    """
    submask = train_data.query(first_rule)
    
    if len(submask) < 30:
        return None
    
    features_to_analyze = [f for f in filtered_features if f != first_feature]
    compound_rules = []
    
    for feature in features_to_analyze:
        try:
            tercile_edges = pd.qcut(submask[feature], q=3, retbins=True, duplicates='drop')[1]
            
            for i in range(3):
                if i == 0:
                    second_rule = f"`{feature}` < {tercile_edges[1]:.3f}"
                elif i == 1:
                    second_rule = f"`{feature}` >= {tercile_edges[1]:.3f} and `{feature}` < {tercile_edges[2]:.3f}"
                else:
                    second_rule = f"`{feature}` >= {tercile_edges[2]:.3f}"
                
                compound_rule = f"({first_rule}) and ({second_rule})"
                rule_mask = train_data.eval(compound_rule)
                
                if len(train_data[rule_mask]) >= 30:
                    side_multiplier = 1 if side == 'long' else -1
                    returns = train_data[rule_mask][target_column] * side_multiplier
                    mean = returns.mean()
                    std = returns.std()
                    metric = mean/std if std != 0 else 0
                    
                    if metric > threshold:
                        compound_rules.append({
                            'feature': [first_feature, feature],
                            'rule': compound_rule,
                            'metric': metric,
                            'train_metric': metric
                        })
        except:
            continue
    
    if compound_rules:
        results_df = pd.DataFrame(compound_rules)
        return results_df.sort_values('train_metric', ascending=False).reset_index(drop=True)
    else:
        return None         
         */

        public class CompoundRule
        {
            public List<string> Feature { get; set; }
            public string Rule { get; set; }
            public double Metric { get; set; }
            public double TrainMetric { get; set; }
        }

        public List<CompoundRule> Find_Second_Rule(
            DataTable trainData,
            string firstFeature,
            string firstRule,
            string targetColumn,
            string dateColumn,
            string side,
            double threshold,
            List<string> filteredFeatures)
        {
            // Filter data based on first rule
            var subData = FilterByRule(trainData, firstRule);

            if (subData.Rows.Count < 30)
            {
                return null;
            }

            var featuresToAnalyze = filteredFeatures.Where(f => f != firstFeature).ToList();
            var compoundRules = new List<CompoundRule>();

            foreach (var feature in featuresToAnalyze)
            {
                try
                {
                    // Calculate terciles
                    var featureValues = GetColumnValues(subData, feature);
                    var tercileEdges = CalculateTerciles(featureValues);

                    for (int i = 0; i < 3; i++)
                    {
                        string secondRule;
                        if (i == 0)
                        {
                            secondRule = $"`{feature}` < {tercileEdges[1]:F3}";
                        }
                        else if (i == 1)
                        {
                            secondRule = $"`{feature}` >= {tercileEdges[1]:F3} and `{feature}` < {tercileEdges[2]:F3}";
                        }
                        else
                        {
                            secondRule = $"`{feature}` >= {tercileEdges[2]:F3}";
                        }

                        string compoundRule = $"({firstRule}) and ({secondRule})";
                        var filteredData = FilterByRule(trainData, compoundRule);

                        if (filteredData.Rows.Count >= 30)
                        {
                            double sideMultiplier = side == "long" ? 1 : -1;

                            // Calculate returns and metrics
                            var returns = GetColumnValues(filteredData, targetColumn)
                                .Select(v => v * sideMultiplier)
                                .ToList();

                            double mean = returns.Average();
                            double std = CalculateStandardDeviation(returns);
                            double metric = std != 0 ? mean / std : 0;

                            if (metric > threshold)
                            {
                                compoundRules.Add(new CompoundRule
                                {
                                    Feature = new List<string> { firstFeature, feature },
                                    Rule = compoundRule,
                                    Metric = metric,
                                    TrainMetric = metric
                                });
                            }
                        }
                    }
                }
                catch
                {
                    continue; // Skip this feature if any error occurs
                }
            }

            if (compoundRules.Count > 0)
            {
                // Sort by train_metric descending
                return compoundRules.OrderByDescending(r => r.TrainMetric).ToList();
            }
            else
            {
                return null;
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

        private double[] CalculateTerciles(List<double> values)
        {
            if (values.Count == 0)
            {
                return new double[] { double.NegativeInfinity, double.NegativeInfinity, double.PositiveInfinity };
            }

            values.Sort();

            double[] terciles = new double[4];
            terciles[0] = double.NegativeInfinity;

            // Calculate tercile points (0%, 33%, 67%, 100%)
            for (int i = 1; i <= 3; i++)
            {
                int index = (int)Math.Ceiling(values.Count * i / 3.0) - 1;
                index = Math.Min(index, values.Count - 1);
                terciles[i] = values[index];
            }

            // Handle duplicates by ensuring terciles are strictly increasing
            for (int i = 2; i <= 3; i++)
            {
                if (terciles[i] <= terciles[i - 1])
                {
                    terciles[i] = terciles[i - 1] + double.Epsilon;
                }
            }

            return terciles;
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
