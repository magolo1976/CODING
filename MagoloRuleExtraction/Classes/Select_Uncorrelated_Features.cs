using System.Data;

namespace MagoloRuleExtraction.Classes
{
    public class Select_Uncorrelated_Features
    {
        /// <summary>
        /// Selecciona características con baja correlación de manera iterativa.
        /// Maneja casos de valores constantes y NaN.
        /// </summary>
        /// <param name="dataFrame">DataTable con los datos</param>
        /// <param name="features">Lista de nombres de características a analizar</param>
        /// <param name="threshold">Umbral de correlación</param>
        /// <returns>Lista de características seleccionadas</returns>
        public List<string> DoWork(DataTable dataFrame, List<string> features, double threshold = 0.95)
        {
            if (features.Count == 0)
                return new List<string>();

            List<string> selectedFeatures = new List<string>();

            // Verificar que el primer feature es válido
            string firstFeature = features[0];
            if (HasVariance(dataFrame, firstFeature) && !AllNull(dataFrame, firstFeature))
            {
                selectedFeatures.Add(firstFeature);
                string lastSelected = firstFeature;

                // Iterar sobre el resto de features
                for (int i = 1; i < features.Count; i++)
                {
                    string feature = features[i];
                    try
                    {
                        double correlation = SafeCorrelation(dataFrame, feature, lastSelected);
                        if (correlation < threshold)
                        {
                            selectedFeatures.Add(feature);
                            lastSelected = feature;
                        }
                    }
                    catch
                    {
                        // Ignorar esta característica y continuar
                        continue;
                    }
                }
            }

            return selectedFeatures;
        }

        /// <summary>
        /// Calcula correlación de manera segura, devolviendo 0 si hay error
        /// </summary>
        private double SafeCorrelation(DataTable data, string series1Name, string series2Name)
        {
            try
            {
                // Extraer datos de las columnas y filtrar filas con valores nulos
                List<Tuple<double, double>> validPairs = new List<Tuple<double, double>>();

                foreach (DataRow row in data.Rows)
                {
                    if (!IsNull(row[series1Name]) && !IsNull(row[series2Name]))
                    {
                        double val1 = Convert.ToDouble(row[series1Name]);
                        double val2 = Convert.ToDouble(row[series2Name]);
                        validPairs.Add(new Tuple<double, double>(val1, val2));
                    }
                }

                if (validPairs.Count < 2)
                    return 0;

                // Extraer las series en listas separadas
                List<double> s1 = validPairs.Select(p => p.Item1).ToList();
                List<double> s2 = validPairs.Select(p => p.Item2).ToList();

                // Verificar que hay suficiente varianza
                if (CalculateStandardDeviation(s1) == 0 || CalculateStandardDeviation(s2) == 0)
                    return 0;

                // Calcular correlación
                return Math.Abs(CalculateCorrelation(s1, s2));
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Calcula la correlación de Pearson entre dos series
        /// </summary>
        private double CalculateCorrelation(List<double> x, List<double> y)
        {
            int n = x.Count;
            double sumX = x.Sum();
            double sumY = y.Sum();
            double sumXY = 0;
            double sumX2 = 0;
            double sumY2 = 0;

            for (int i = 0; i < n; i++)
            {
                sumXY += x[i] * y[i];
                sumX2 += x[i] * x[i];
                sumY2 += y[i] * y[i];
            }

            double numerator = n * sumXY - sumX * sumY;
            double denominator = Math.Sqrt((n * sumX2 - sumX * sumX) * (n * sumY2 - sumY * sumY));

            if (denominator == 0)
                return 0;

            return numerator / denominator;
        }

        /// <summary>
        /// Calcula la desviación estándar de una lista de valores
        /// </summary>
        private double CalculateStandardDeviation(List<double> values)
        {
            if (values.Count <= 1)
                return 0;

            double avg = values.Average();
            double sumOfSquaresOfDifferences = values.Select(val => (val - avg) * (val - avg)).Sum();
            double variance = sumOfSquaresOfDifferences / (values.Count - 1);
            return Math.Sqrt(variance);
        }

        /// <summary>
        /// Verifica si una columna tiene varianza (no es constante)
        /// </summary>
        private bool HasVariance(DataTable data, string columnName)
        {
            List<double> values = new List<double>();
            foreach (DataRow row in data.Rows)
            {
                if (!IsNull(row[columnName]))
                {
                    values.Add(Convert.ToDouble(row[columnName]));
                }
            }

            if (values.Count <= 1)
                return false;

            double firstValue = values[0];
            return values.Any(v => Math.Abs(v - firstValue) > double.Epsilon);
        }

        /// <summary>
        /// Verifica si todos los valores de una columna son nulos
        /// </summary>
        private bool AllNull(DataTable data, string columnName)
        {
            foreach (DataRow row in data.Rows)
            {
                if (!IsNull(row[columnName]))
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Verifica si un valor es nulo o DBNull
        /// </summary>
        private bool IsNull(object value)
        {
            return value == null || value == DBNull.Value || double.IsNaN(Convert.ToDouble(value));
        }
    }
}
