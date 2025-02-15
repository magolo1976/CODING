namespace MTT_RuleExtraction
{
    public class CreateReturnsStatsTable
    {
        /***** PYTHON 
def create_returns_stats_table(train_returns, test_returns, forward_returns):
    \"""
    Crea una tabla con las estadísticas de los retornos
    \"""
    def calculate_stats(returns):
        if returns.empty:
            return {
                'mean': 0,
                'std': 0,
                'min': 0,
                'max': 0,
                'pos_pct': 0,
                'count': 0
            }
        
        stats = {
            'mean': returns.mean(),
            'std': returns.std(),
            'min': returns.min(),
            'max': returns.max(),
            'pos_pct': (returns > 0).mean() * 100,
            'count': len(returns)
        }
        return stats

    train_stats = calculate_stats(train_returns)
    test_stats = calculate_stats(test_returns)
    forward_stats = calculate_stats(forward_returns)
    
    stats_df = pd.DataFrame({
        'Estadística': [
            'Retorno Promedio (%)',
            'Desviación Estándar (%)',
            'Retorno Mínimo (%)',
            'Retorno Máximo (%)',
            '% Retornos Positivos',
            'Número de Observaciones'
        ],
        'Test': [
            f"{test_stats['mean']:.3f}",
            f"{test_stats['std']:.3f}",
            f"{test_stats['min']:.3f}",
            f"{test_stats['max']:.3f}",
            f"{test_stats['pos_pct']:.1f}",
            f"{test_stats['count']}"
        ],
        'Train': [
            f"{train_stats['mean']:.3f}",
            f"{train_stats['std']:.3f}",
            f"{train_stats['min']:.3f}",
            f"{train_stats['max']:.3f}",
            f"{train_stats['pos_pct']:.1f}",
            f"{train_stats['count']}"
        ],
        'Forward': [
            f"{forward_stats['mean']:.3f}",
            f"{forward_stats['std']:.3f}",
            f"{forward_stats['min']:.3f}",
            f"{forward_stats['max']:.3f}",
            f"{forward_stats['pos_pct']:.1f}",
            f"{forward_stats['count']}"
        ]
    }).set_index('Estadística')
    
    return stats_df
        */

        // Simulación simple de un DataFrame (en una aplicación real usarías una biblioteca como Deedle o Microsoft ML.NET)
        public class DataFrame
        {
            public Dictionary<string, List<string>> Data { get; private set; }
            public List<string> Index { get; private set; }

            public DataFrame(Dictionary<string, List<string>> data, List<string> index)
            {
                Data = data;
                Index = index;
            }

            public void SetIndex(string columnName)
            {
                if (Data.TryGetValue(columnName, out var indexColumn))
                {
                    Index = indexColumn;
                    Data.Remove(columnName);
                }
            }
        }

        public static DataFrame Create_Returns_Stats_Table(List<double> trainReturns, List<double> testReturns, List<double> forwardReturns)
        {
            // Función para calcular estadísticas
            Dictionary<string, double> CalculateStats(List<double> returns)
            {
                if (returns == null || !returns.Any())
                {
                    return new Dictionary<string, double>
                    {
                        { "mean", 0 },
                        { "std", 0 },
                        { "min", 0 },
                        { "max", 0 },
                        { "pos_pct", 0 },
                        { "count", 0 }
                    };
                }

                double mean = returns.Average();
                double variance = returns.Select(x => Math.Pow(x - mean, 2)).Average();
                double std = Math.Sqrt(variance);

                return new Dictionary<string, double>
                {
                    { "mean", mean },
                    { "std", std },
                    { "min", returns.Min() },
                    { "max", returns.Max() },
                    { "pos_pct", returns.Count(r => r > 0) * 100.0 / returns.Count },
                    { "count", returns.Count }
                };
            }

            var trainStats = CalculateStats(trainReturns);
            var testStats = CalculateStats(testReturns);
            var forwardStats = CalculateStats(forwardReturns);

            // Crear DataFrame
            var estadisticas = new List<string>
            {
                "Retorno Promedio (%)",
                "Desviación Estándar (%)",
                "Retorno Mínimo (%)",
                "Retorno Máximo (%)",
                "% Retornos Positivos",
                "Número de Observaciones"
            };

            var testColumn = new List<string>
            {
                string.Format("{0:F3}", testStats["mean"]),
                string.Format("{0:F3}", testStats["std"]),
                string.Format("{0:F3}", testStats["min"]),
                string.Format("{0:F3}", testStats["max"]),
                string.Format("{0:F1}", testStats["pos_pct"]),
                testStats["count"].ToString()
            };

            var trainColumn = new List<string>
            {
                string.Format("{0:F3}", trainStats["mean"]),
                string.Format("{0:F3}", trainStats["std"]),
                string.Format("{0:F3}", trainStats["min"]),
                string.Format("{0:F3}", trainStats["max"]),
                string.Format("{0:F1}", trainStats["pos_pct"]),
                trainStats["count"].ToString()
            };

            var forwardColumn = new List<string>
            {
                string.Format("{0:F3}", forwardStats["mean"]),
                string.Format("{0:F3}", forwardStats["std"]),
                string.Format("{0:F3}", forwardStats["min"]),
                string.Format("{0:F3}", forwardStats["max"]),
                string.Format("{0:F1}", forwardStats["pos_pct"]),
                forwardStats["count"].ToString()
            };

            var data = new Dictionary<string, List<string>>
            {
                { "Estadística", estadisticas },
                { "Test", testColumn },
                { "Train", trainColumn },
                { "Forward", forwardColumn }
            };

            var dataFrame = new DataFrame(data, null);
            dataFrame.SetIndex("Estadística");

            return dataFrame;
        }
    }
}
