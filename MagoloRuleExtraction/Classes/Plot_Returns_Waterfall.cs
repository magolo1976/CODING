using OxyPlot;
using OxyPlot.Annotations;
using OxyPlot.Axes;
using OxyPlot.Series;
using HorizontalAlignment = OxyPlot.HorizontalAlignment;

namespace MagoloRuleExtraction.Classes
{
    public class Plot_Returns_Waterfall
    {
        public static (PlotModel, double) PlotReturns(List<double> returns)
        {
            // Calcular sumas
            double positiveReturns = returns.Where(r => r > 0).Sum();
            double negativeReturns = returns.Where(r => r <= 0).Sum();
            double netReturn = (positiveReturns + negativeReturns);

            // Calcular porcentajes sobre total
            double totalReturns = Math.Abs(positiveReturns) + Math.Abs(negativeReturns);
            double posPct = (Math.Abs(positiveReturns) / totalReturns * 100);
            double negPct = (Math.Abs(negativeReturns) / totalReturns * 100);

            positiveReturns = positiveReturns / 10;
            negativeReturns = negativeReturns / 10;
            netReturn = netReturn / 10;
            // Crear modelo de gráfico
            var plotModel = new PlotModel { Title = "Suma de Retornos (Train + Test)" };

            // Configurar eje X (valores)
            var xAxis = new LinearAxis
            {
                Position = AxisPosition.Bottom,
                Title = "Retorno (%)",
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Dot
            };
            plotModel.Axes.Add(xAxis);

            // Configurar eje Y (categorías)
            var yAxis = new CategoryAxis
            {
                Position = AxisPosition.Left
            };
            yAxis.Labels.Add("Retorno Neto");
            yAxis.Labels.Add("Retornos Negativos");
            yAxis.Labels.Add("Retornos Positivos");
            plotModel.Axes.Add(yAxis);

            // Crear serie de barras horizontales
            var barSeries = new BarSeries
            {
                IsStacked = false,
                StrokeColor = OxyColors.Black,
                StrokeThickness = 1,
                BarWidth = 0.7
            };

            // Añadir barras para cada categoría (nota: el índice 0 corresponde a la etiqueta inferior)
            var posBar = new BarItem
            {
                Value = positiveReturns,
                CategoryIndex = 2,  // "Retornos Positivos" está en la posición 2
                Color = OxyColor.Parse("#2ecc71")
            };
            barSeries.Items.Add(posBar);

            var negBar = new BarItem
            {
                Value = negativeReturns,
                CategoryIndex = 1,  // "Retornos Negativos" está en la posición 1
                Color = OxyColor.Parse("#e74c3c")
            };
            barSeries.Items.Add(negBar);

            var netBar = new BarItem
            {
                Value = netReturn,
                CategoryIndex = 0,  // "Retorno Neto" está en la posición 0
                Color = OxyColor.Parse("#3498db")
            };
            barSeries.Items.Add(netBar);

            // Añadir serie al modelo
            plotModel.Series.Add(barSeries);

            // Agregar anotaciones como texto
            var posAnnotation = new TextAnnotation
            {
                Text = $"({posPct:F1} % del volumen)\n+{positiveReturns:F2} %",
                TextPosition = new DataPoint(positiveReturns + 2, 2),
                TextHorizontalAlignment = HorizontalAlignment.Left,
                TextVerticalAlignment = VerticalAlignment.Middle,
                StrokeThickness = 0
            };
            plotModel.Annotations.Add(posAnnotation);

            var negAnnotation = new TextAnnotation
            {
                Text = $"({negPct:F1} % del volumen)\n{negativeReturns:F2} %",
                TextPosition = new DataPoint(negativeReturns - 2, 1),
                TextHorizontalAlignment = HorizontalAlignment.Right,
                TextVerticalAlignment = VerticalAlignment.Middle,
                StrokeThickness = 0
            };
            plotModel.Annotations.Add(negAnnotation);

            var netAnnotation = new TextAnnotation
            {
                Text = $"{netReturn:F2} %",
                TextPosition = new DataPoint(netReturn + (netReturn >= 0 ? 2 : -2), 0),
                TextHorizontalAlignment = netReturn >= 0 ? HorizontalAlignment.Left : HorizontalAlignment.Right,
                TextVerticalAlignment = VerticalAlignment.Middle,
                StrokeThickness = 0
            };
            plotModel.Annotations.Add(netAnnotation);

            // Configurar la leyenda
            plotModel.IsLegendVisible = false;

            // Configurar márgenes
            plotModel.Padding = new OxyThickness(10, 10, 100, 10); // Extra padding a la derecha para las anotaciones

            return (plotModel, totalReturns);
        }
    }
}
