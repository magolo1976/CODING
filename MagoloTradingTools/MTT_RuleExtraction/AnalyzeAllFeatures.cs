namespace MTT_RuleExtraction
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class AnalyzeAllFeatures
    {
        /***** PYTHON 
def analyze_all_features(df, target_column, date_column, side, correlation_threshold=0.7):
    \"""
    Analiza todas las features, primero reduciendo la multicolinealidad
    \"""
    exclude_columns = [date_column, 'Open', 'Close', target_column, 'Target']
    initial_features = [col for col in df.columns if col not in exclude_columns]
    
    selected_features = select_uncorrelated_features(df, initial_features, correlation_threshold)
    
    returns = df[target_column]
    random_profits = calculate_random_profits(returns, side)
    
    results = []
    rules_dict = {}
    
    for feature in selected_features:
        try:
            feature_metric, feature_rule = analyze_feature(df, feature, target_column, side)
            score = (feature_metric > random_profits).mean() * 100
            
            if score > 0:
                results.append({
                    'feature': feature,
                    'score': score
                })
                rules_dict[feature] = {
                    'rule': feature_rule,
                    'score': score
                }
        except:
            continue
    
    if results:
        results_df = pd.DataFrame(results)
        results_df = results_df.sort_values('score', ascending=False).reset_index(drop=True)
        results_df['score'] = results_df['score'].round(2)
        return results_df, rules_dict
    else:
        return pd.DataFrame(columns=['feature', 'score']), {}
         */

        /// <summary>
        /// Analiza todas las características, primero reduciendo la multicolinealidad
        /// </summary>
        public static (List<Dictionary<string, object>> resultsDf, Dictionary<string, Dictionary<string, object>> rulesDict)
            Analyze_All_Features(
                Dictionary<string, List<double>> df,
                string targetColumn,
                string dateColumn,
                string side,
                double correlationThreshold = 0.7)
        {
            var excludeColumns = new List<string> { dateColumn, "Open", "Close", targetColumn, "Target" };
            var initialFeatures = df.Keys.Where(col => !excludeColumns.Contains(col)).ToList();

            var selectedFeatures = SelectUncorrelatedFeatures.Select_Uncorrelated_Features(
                df.ToDictionary(kv => kv.Key, kv => kv.Value.Select(v => (double?)v).ToList()),
                initialFeatures,
                correlationThreshold);

            var returns = df[targetColumn];
            var randomProfits = CalculateRandomProfits.Calculate_Random_Profits(returns, side);

            var results = new List<Dictionary<string, object>>();
            var rulesDict = new Dictionary<string, Dictionary<string, object>>();

            foreach (var feature in selectedFeatures)
            {
                try
                {
                    var (featureMetric, featureRule) = AnalyzeFeature.Analyze_Feature(df, feature, targetColumn, side);
                    double score = randomProfits.Count(p => featureMetric > p) * 100.0 / randomProfits.Length;

                    if (score > 0)
                    {
                        results.Add(new Dictionary<string, object>
                    {
                        { "feature", feature },
                        { "score", score }
                    });

                        rulesDict[feature] = new Dictionary<string, object>
                    {
                        { "rule", featureRule },
                        { "score", score }
                    };
                    }
                }
                catch
                {
                    continue;
                }
            }

            if (results.Any())
            {
                // Ordenar resultados por puntuación en orden descendente
                results = results.OrderByDescending(r => Convert.ToDouble(r["score"]))
                                 .Select((r, i) =>
                                 {
                                     var newDict = new Dictionary<string, object>(r);
                                     newDict["score"] = Math.Round(Convert.ToDouble(r["score"]), 2);
                                     return newDict;
                                 })
                                 .ToList();

                return (results, rulesDict);
            }
            else
            {
                return (new List<Dictionary<string, object>>(), new Dictionary<string, Dictionary<string, object>>());
            }
        }
    }
}
