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
    using System.Linq;

    public class PlotReturnsWaterfall
    {
        public class WaterfallData
        {
            public double PositiveReturns { get; set; }
            public double NegativeReturns { get; set; }
            public double NetReturn { get; set; }
            public double PositivePercentage { get; set; }
            public double NegativePercentage { get; set; }
        }

        public WaterfallData Plot_Returns_Waterfall(double[] returns)
        {
            // Calculate sums
            double positiveReturns = returns.Where(r => r > 0).Sum();
            double negativeReturns = returns.Where(r => r <= 0).Sum();
            double netReturn = positiveReturns + negativeReturns;

            // Prepare the statistical data
            double totalAbs = Math.Abs(positiveReturns) + Math.Abs(negativeReturns);
            double posPct = totalAbs > 0 ? (Math.Abs(positiveReturns) / totalAbs * 100) : 0;
            double negPct = totalAbs > 0 ? (Math.Abs(negativeReturns) / totalAbs * 100) : 0;

            // Return a structured object containing the results
            return new WaterfallData
            {
                PositiveReturns = positiveReturns,
                NegativeReturns = negativeReturns,
                NetReturn = netReturn,
                PositivePercentage = posPct,
                NegativePercentage = negPct
            };
        }
    }

    //// Example usage
    //class Program
    //{
    //    static void Main()
    //    {
    //        ReturnsWaterfall waterfall = new ReturnsWaterfall();

    //        double[] returns = { 10.5, -5.0, 3.2, -2.1, 4.0 }; // Sample returns

    //        ReturnsWaterfall.WaterfallData data = waterfall.PlotReturnsWaterfall(returns);

    //        // Print results – for validation or plotting purposes
    //        Console.WriteLine($"Positive Returns: {data.PositiveReturns:F2}%");
    //        Console.WriteLine($"Negative Returns: {data.NegativeReturns:F2}%");
    //        Console.WriteLine($"Net Return: {data.NetReturn:F2}%");
    //        Console.WriteLine($"Positive Percentage: {data.PositivePercentage:F1}%");
    //        Console.WriteLine($"Negative Percentage: {data.NegativePercentage:F1}%");
    //    }
    //}
}
