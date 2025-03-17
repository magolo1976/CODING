using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot;
using System.Data;

namespace MagoloRuleExtraction.Classes
{
    public class Plot_Price_Evolution
    {
        public static PlotModel GetPlot(
                                    DataTable dataTable,
                                    string dateColumnName,
                                    DateTime trainStart,
                                    DateTime trainEnd,
                                    DateTime testStart,
                                    DateTime testEnd,
                                    DateTime forwardStart,
                                    DateTime forwardEnd)
        {
            // Crear el modelo del gráfico
            var plotModel = new PlotModel { Title = "Evolución del precio por período" };

            // Añadir ejes
            var dateAxis = new DateTimeAxis
            {
                Position = AxisPosition.Bottom,
                Title = "Fecha",
                StringFormat = "yyyy", //"dd/MM/yyyy",
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Dot,
                IntervalType = DateTimeIntervalType.Years
            };

            var valueAxis = new LinearAxis
            {
                Position = AxisPosition.Left,
                Title = "Precio de apertura",
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Dot
            };

            plotModel.Axes.Add(dateAxis);
            plotModel.Axes.Add(valueAxis);

            // Filtrar por períodos y crear series

            // Serie de Test (amarillo)
            var testSeries = new LineSeries
            {
                Title = "Test",
                Color = OxyColor.FromRgb(255, 215, 0),  // #FFD700 amarillo
                StrokeThickness = 2,
                MarkerType = MarkerType.None
            };

            // Serie de Train (azul)
            var trainSeries = new LineSeries
            {
                Title = "Train",
                Color = OxyColor.FromRgb(0, 0, 255),    // #0000FF azul
                StrokeThickness = 2,
                MarkerType = MarkerType.None
            };

            // Serie de Forward (rojo)
            var forwardSeries = new LineSeries
            {
                Title = "Forward",
                Color = OxyColor.FromRgb(255, 0, 0),    // #FF0000 rojo
                StrokeThickness = 2,
                MarkerType = MarkerType.None
            };

            // Procesar los datos de la tabla
            foreach (DataRow row in dataTable.Rows)
            {
                if (row[dateColumnName] == DBNull.Value || row["Open"] == DBNull.Value)
                    continue;

                DateTime rowDate = Convert.ToDateTime(row[dateColumnName]);
                double price = Convert.ToDouble(row["Open"]);

                // Convertir DateTime a OxyPlot DateTimeAxis.ToDouble para los puntos
                double x = DateTimeAxis.ToDouble(rowDate);

                // Añadir a la serie correspondiente según el período
                if (rowDate >= testStart && rowDate <= testEnd)
                    testSeries.Points.Add(new DataPoint(x, price));

                if (rowDate >= trainStart && rowDate <= trainEnd)
                    trainSeries.Points.Add(new DataPoint(x, price));

                if (rowDate >= forwardStart && rowDate <= forwardEnd)
                    forwardSeries.Points.Add(new DataPoint(x, price));
            }

            // Añadir las series al modelo en el mismo orden que en la función de Python
            // Primero Test, luego Train, y finalmente Forward
            plotModel.Series.Add(testSeries);
            plotModel.Series.Add(trainSeries);
            plotModel.Series.Add(forwardSeries);

            // Añadir leyenda
            plotModel.Legends.Add(new OxyPlot.Legends.Legend
            {
                LegendPosition = OxyPlot.Legends.LegendPosition.TopRight,
                LegendPlacement = OxyPlot.Legends.LegendPlacement.Inside
            });

            return plotModel;
        }
    }
}
