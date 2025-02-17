namespace MTT_RuleExtraction
{
    /***** PYTHON 
def plot_returns_waterfall(returns):
    """
    Crea un gráfico de barras que muestra la suma de retornos positivos y negativos
    """
    # Calcular sumas
    positive_returns = returns[returns > 0].sum()
    negative_returns = returns[returns <= 0].sum()
    net_return = positive_returns + negative_returns
    
    # Crear gráfico
    fig = go.Figure()
    
    # Barra de retornos positivos
    fig.add_trace(go.Bar(
        name='Retornos Positivos',
        x=['Retornos Positivos'],
        y=[positive_returns],
        marker_color='#2ecc71',
        text=f'+{positive_returns:.2f}%',
        textposition='outside'
    ))
    
    # Barra de retornos negativos
    fig.add_trace(go.Bar(
        name='Retornos Negativos',
        x=['Retornos Negativos'],
        y=[negative_returns],
        marker_color='#e74c3c',
        text=f'{negative_returns:.2f}%',
        textposition='outside'
    ))
    
    # Barra de retorno neto
    fig.add_trace(go.Bar(
        name='Retorno Neto',
        x=['Retorno Neto'],
        y=[net_return],
        marker_color='#3498db',
        text=f'{net_return:.2f}%',
        textposition='outside'
    ))
    
    # Actualizar layout
    fig.update_layout(
        title='Suma de Retornos (Train + Test)',
        yaxis_title='Retorno (%)',
        showlegend=True,
        barmode='group'
    )
    
    # Añadir anotaciones con porcentajes sobre total
    total_abs = abs(positive_returns) + abs(negative_returns)
    pos_pct = (abs(positive_returns) / total_abs * 100)
    neg_pct = (abs(negative_returns) / total_abs * 100)
    
    fig.add_annotation(
        x='Retornos Positivos',
        y=positive_returns,
        text=f'({pos_pct:.1f}% del volumen)',
        showarrow=False,
        yshift=30
    )
    
    fig.add_annotation(
        x='Retornos Negativos',
        y=negative_returns,
        text=f'({neg_pct:.1f}% del volumen)',
        showarrow=False,
        yshift=-30
    )
    
    return fig      
     */

    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class PlotReturnsWaterfall_Copia
    {
        // ... Previous code ...

        public class WaterfallChartData
        {
            public class BarData
            {
                public string Name { get; set; }
                public double Value { get; set; }
                public string Color { get; set; }
                public string Text { get; set; }
                public string Annotation { get; set; }
                public int AnnotationYShift { get; set; }
            }

            public string Title { get; set; }
            public string YAxisTitle { get; set; }
            public List<BarData> Bars { get; set; }
        }

        public WaterfallChartData PlotReturnsWaterfall(double[] returns)
        {
            if (returns == null || returns.Length == 0)
            {
                return null;
            }

            // Calculate sums
            double positiveReturns = returns.Where(r => r > 0).Sum();
            double negativeReturns = returns.Where(r => r <= 0).Sum();
            double netReturn = positiveReturns + negativeReturns;

            // Calculate percentages
            double totalAbs = Math.Abs(positiveReturns) + Math.Abs(negativeReturns);
            double posPct = (Math.Abs(positiveReturns) / totalAbs * 100);
            double negPct = (Math.Abs(negativeReturns) / totalAbs * 100);

            // Create bar data
            var bars = new List<WaterfallChartData.BarData>
        {
            new WaterfallChartData.BarData
            {
                Name = "Retornos Positivos",
                Value = positiveReturns,
                Color = "#2ecc71",
                Text = $"+{positiveReturns:F2}%",
                Annotation = $"({posPct:F1}% del volumen)",
                AnnotationYShift = 30
            },
            new WaterfallChartData.BarData
            {
                Name = "Retornos Negativos",
                Value = negativeReturns,
                Color = "#e74c3c",
                Text = $"{negativeReturns:F2}%",
                Annotation = $"({negPct:F1}% del volumen)",
                AnnotationYShift = -30
            },
            new WaterfallChartData.BarData
            {
                Name = "Retorno Neto",
                Value = netReturn,
                Color = "#3498db",
                Text = $"{netReturn:F2}%",
                Annotation = null,
                AnnotationYShift = 0
            }
        };

            return new WaterfallChartData
            {
                Title = "Suma de Retornos (Train + Test)",
                YAxisTitle = "Retorno (%)",
                Bars = bars
            };
        }
    }
}
