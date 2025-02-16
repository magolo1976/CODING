namespace MTT_RuleExtraction
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class PlotMetricComparison
    {
        /***** PYTHON 
def plot_metric_comparison(df):
    """
    Crea un gráfico de dispersión comparando métricas de train vs test
    """
    if 'train_metric' not in df.columns or 'test_metric' not in df.columns:
        return None
        
    fig = go.Figure()
    
    fig.add_trace(go.Scatter(
        x=df['train_metric'],
        y=df['test_metric'],
        mode='markers',
        marker=dict(
            size=10,
            color='blue',
            opacity=0.6
        ),
        name='Reglas'
    ))
    
    # Añadir línea diagonal de referencia
    max_value = max(max(df['train_metric']), max(df['test_metric']))
    min_value = min(min(df['train_metric']), min(df['test_metric']))
    fig.add_trace(go.Scatter(
        x=[min_value, max_value],
        y=[min_value, max_value],
        mode='lines',
        line=dict(dash='dash', color='red'),
        name='Línea de referencia'
    ))
    
    fig.update_layout(
        title='Comparación de Métricas Train vs Test',
        xaxis_title='Métrica en Train',
        yaxis_title='Métrica en Test',
        showlegend=True
    )
    
    return fig        
         */

        public class MetricComparisonData
        {
            public List<double> TrainMetrics { get; set; }
            public List<double> TestMetrics { get; set; }
            public double MinValue { get; set; }
            public double MaxValue { get; set; }
            public string Title { get; set; }
            public string XAxisTitle { get; set; }
            public string YAxisTitle { get; set; }
        }

        public MetricComparisonData PrepareMetricComparisonData(List<Dictionary<string, object>> dfRows)
        {
            // Validate input data
            if (!dfRows.Any() ||
                !dfRows[0].ContainsKey("train_metric") ||
                !dfRows[0].ContainsKey("test_metric"))
            {
                return null;
            }

            // Extract metrics
            var trainMetrics = dfRows.Select(row => Convert.ToDouble(row["train_metric"])).ToList();
            var testMetrics = dfRows.Select(row => Convert.ToDouble(row["test_metric"])).ToList();

            // Calculate min and max values for reference line
            double maxValue = Math.Max(trainMetrics.Max(), testMetrics.Max());
            double minValue = Math.Min(trainMetrics.Min(), testMetrics.Min());

            // Prepare result data
            return new MetricComparisonData
            {
                TrainMetrics = trainMetrics,
                TestMetrics = testMetrics,
                MinValue = minValue,
                MaxValue = maxValue,
                Title = "Comparación de Métricas Train vs Test",
                XAxisTitle = "Métrica en Train",
                YAxisTitle = "Métrica en Test"
            };
        }
    }
}
