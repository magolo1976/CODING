namespace MTT_RuleExtraction
{
    public class SelectUncorrelatedFeatures
    {
        /***** PYTHON 
def select_uncorrelated_features(df, features, threshold=0.95):
    \"""
    Selecciona features con baja correlación de manera iterativa.
    Maneja casos de valores constantes y NaN.
    \"""
    if len(features) == 0:
        return []
    
    def safe_correlation(series1, series2):
        \"""
        Calcula correlación de manera segura, devolviendo 0 si hay error
        \"""
        try:
            # Eliminar filas donde cualquiera de las series tenga NaN
            mask = ~(series1.isna() | series2.isna())
            s1, s2 = series1[mask], series2[mask]
            
            # Verificar que hay suficiente varianza
            if s1.std() == 0 or s2.std() == 0:
                return 0
                
            return abs(s1.corr(s2))
        except:
            return 0
    
    selected_features = []
    
    # Verificar que el primer feature es válido
    first_feature = features[0]
    if df[first_feature].std() > 0 and df[first_feature].notna().any():
        selected_features = [first_feature]
        last_selected = first_feature
        
        # Iterar sobre el resto de features
        for feature in features[1:]:
            try:
                correlation = safe_correlation(df[feature], df[last_selected])
                if correlation < threshold:
                    selected_features.append(feature)
                    last_selected = feature
            except:
                continue
    
    return selected_features         
        */

        /// <summary>
        /// Selecciona características con baja correlación de manera iterativa.
        /// Maneja casos de valores constantes y valores nulos.
        /// </summary>
        public static List<string> Select_Uncorrelated_Features(Dictionary<string, List<double?>> df, List<string> features, double threshold = 0.95)
        {
            if (features.Count == 0)
                return new List<string>();

            // Función para calcular correlación de manera segura
            double SafeCorrelation(List<double?> series1, List<double?> series2)
            {
                try
                {
                    // Eliminar filas donde cualquiera de las series tenga null
                    var validPairs = new List<(double, double)>();
                    for (int i = 0; i < series1.Count; i++)
                    {
                        if (series1[i].HasValue && series2[i].HasValue)
                        {
                            validPairs.Add((series1[i].Value, series2[i].Value));
                        }
                    }

                    if (validPairs.Count == 0)
                        return 0;

                    // Calcular desviación estándar
                    double std1 = StdDev(validPairs.Select(p => p.Item1).ToList());
                    double std2 = StdDev(validPairs.Select(p => p.Item2).ToList());

                    // Verificar que hay suficiente varianza
                    if (std1 == 0 || std2 == 0)
                        return 0;

                    // Calcular correlación de Pearson
                    return Math.Abs(PearsonCorrelation(validPairs));
                }
                catch
                {
                    return 0;
                }
            }

            var selectedFeatures = new List<string>();

            // Verificar que el primer feature es válido
            string firstFeature = features[0];
            double stdDev = StdDev(df[firstFeature].Where(x => x.HasValue).Select(x => x.Value).ToList());

            if (stdDev > 0 && df[firstFeature].Any(x => x.HasValue))
            {
                selectedFeatures.Add(firstFeature);
                string lastSelected = firstFeature;

                // Iterar sobre el resto de features
                foreach (var feature in features.Skip(1))
                {
                    try
                    {
                        double correlation = SafeCorrelation(df[feature], df[lastSelected]);
                        if (correlation < threshold)
                        {
                            selectedFeatures.Add(feature);
                            lastSelected = feature;
                        }
                    }
                    catch
                    {
                        continue;
                    }
                }
            }

            return selectedFeatures;
        }

        // Calcula la desviación estándar de una lista de valores
        private static double StdDev(List<double> values)
        {
            if (values.Count <= 1)
                return 0;

            double avg = values.Average();
            double sum = values.Sum(d => Math.Pow(d - avg, 2));
            return Math.Sqrt(sum / (values.Count - 1));
        }

        // Calcula el coeficiente de correlación de Pearson
        private static double PearsonCorrelation(List<(double, double)> pairs)
        {
            if (pairs.Count <= 1)
                return 0;

            double avgX = pairs.Average(p => p.Item1);
            double avgY = pairs.Average(p => p.Item2);

            double sumXY = pairs.Sum(p => (p.Item1 - avgX) * (p.Item2 - avgY));
            double sumX2 = pairs.Sum(p => Math.Pow(p.Item1 - avgX, 2));
            double sumY2 = pairs.Sum(p => Math.Pow(p.Item2 - avgY, 2));

            if (sumX2 == 0 || sumY2 == 0)
                return 0;

            return sumXY / (Math.Sqrt(sumX2) * Math.Sqrt(sumY2));
        }
    }
}
