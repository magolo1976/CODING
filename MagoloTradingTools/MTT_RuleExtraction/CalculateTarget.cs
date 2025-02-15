namespace MTT_RuleExtraction
{
    internal class CalculateTarget
    {
        /*PYTHON
         * 
        def calculate_target(df):
            """
            Calcula el Target como: (Open(T+2) - Open(T+1)) * 100 / Open(T+1)
            """
            df['Next_Open'] = df['Open'].shift(-1)
            df['Next_Next_Open'] = df['Open'].shift(-2)
            df['Target'] = (df['Next_Next_Open'] - df['Next_Open']) * 100 / df['Next_Open']
            df = df.drop(['Next_Open', 'Next_Next_Open'], axis=1)
            return df.dropna()
        */

        /// <summary>
        /// **** IMPLEMENTACION ***
        /// /// var data = new List<Dictionary<string, double>>
        /// {
        ///     new Dictionary<string, double> { { "Open", 100 } },
        ///     new Dictionary<string, double> { { "Open", 105 } },
        ///     new Dictionary<string, double> { { "Open", 110 } },
        ///     new Dictionary<string, double> { { "Open", 115 } }
        /// };
        /// var result = Calculate_Target.GetData(data);
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static List<Dictionary<string, double>> GetData(List<Dictionary<string, double>> data)
        {
            // Initialize a list to store the resulting data with the Target
            List<Dictionary<string, double>> result = new List<Dictionary<string, double>>();

            // Process each entry, where 'data' is assumed to have at least 2 records
            for (int i = 0; i < data.Count - 2; i++) // Iterate until the third last element
            {
                var current = data[i];
                var nextOpen = data[i + 1]["Open"];
                var nextNextOpen = data[i + 2]["Open"];

                double target = (nextNextOpen - nextOpen) * 100 / nextOpen;

                // Create a new dictionary for the result
                Dictionary<string, double> resultEntry = new Dictionary<string, double>(current)
                {
                    { "Target", target } // Add calculated target
                };

                result.Add(resultEntry);
            }

            return result; // Return the result list
        }
    }
}
