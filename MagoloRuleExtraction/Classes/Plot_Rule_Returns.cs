using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot;
using System.Data;

namespace MagoloRuleExtraction.Classes
{
    public class Plot_Rule_Returns
    {
        public const string TRAIN = "train";
        public const string TEST = "test";

        public PlotModel DoWork(
                DataTable df,
                string rule,
                string dateColumn,
                string side,
                string period)
        {
            // Filtrar datos según la regla
            DataTable filteredRows = FilterDataTableByExpression(df, rule);

            //var filteredRows = df.Select(rule)
            //    .Where(row => row != null)
            //    .ToList();

            if (filteredRows.Rows.Count == 0)
            {
                return null;
            }

            // Crear un nuevo DataTable con las filas filtradas
            DataTable dfFiltered = df.Clone();
            foreach (DataRow row in filteredRows.Rows)
            {
                dfFiltered.ImportRow(row);
            }

            // Determinar el multiplicador según el lado (long/short)
            int sideMultiplier = side == "LONG" ? 1 : -1;

            // Añadir columnas para retornos ajustados y período
            dfFiltered.Columns.Add("AdjustedReturns", typeof(double));
            dfFiltered.Columns.Add("Period", typeof(string));
            dfFiltered.Columns.Add("CumulativeReturns", typeof(double));

            double cumulativeReturns = 0;
            for (int i = 0; i < dfFiltered.Rows.Count; i++)
            {
                // Calcular retornos ajustados
                double target = Convert.ToDouble(dfFiltered.Rows[i]["Target"]);
                dfFiltered.Rows[i]["AdjustedReturns"] = target * sideMultiplier;

                // Asignar período (train/test)
                dfFiltered.Rows[i]["Period"] = period; // mask[i] ? "train" : "test";

                // Calcular retornos acumulados
                cumulativeReturns += Convert.ToDouble(dfFiltered.Rows[i]["AdjustedReturns"]);
                dfFiltered.Rows[i]["CumulativeReturns"] = cumulativeReturns;
            }

            // Crear el modelo de gráfico
            var plotModel = new PlotModel { Title = "Evolución del Retorno Acumulado" };
            plotModel.Axes.Add(new DateTimeAxis
            {
                Position = AxisPosition.Bottom,
                Title = "Fecha"
            });
            plotModel.Axes.Add(new LinearAxis
            {
                Position = AxisPosition.Left,
                Title = "Retorno Acumulado (%)"
            });

            // Graficar datos de entrenamiento
            var trainRows = dfFiltered.Select($"Period = '{TRAIN}'");
            if (trainRows.Length > 0)
            {
                var trainSeries = new LineSeries
                {
                    Title = "Train",
                    Color = OxyColors.Blue,
                    MarkerType = MarkerType.None
                };

                foreach (DataRow row in trainRows)
                {
                    DateTime date = Convert.ToDateTime(row[dateColumn]);
                    double cumReturns = Convert.ToDouble(row["CumulativeReturns"]);
                    trainSeries.Points.Add(new DataPoint(DateTimeAxis.ToDouble(date), cumReturns));
                }
                plotModel.Series.Add(trainSeries);
            }

            // Graficar datos de prueba
            var testRows = dfFiltered.Select($"Period = '{TEST}'");
            if (testRows.Length > 0)
            {
                var testSeries = new LineSeries
                {
                    Title = "Test",
                    Color = OxyColors.Gold,
                    MarkerType = MarkerType.None
                };

                foreach (DataRow row in testRows)
                {
                    DateTime date = Convert.ToDateTime(row[dateColumn]);
                    double cumReturns = Convert.ToDouble(row["CumulativeReturns"]);
                    testSeries.Points.Add(new DataPoint(DateTimeAxis.ToDouble(date), cumReturns));
                }
                plotModel.Series.Add(testSeries);
            }

            return plotModel;
        }

        /// <summary>
        /// Filtra un DataTable según una expresión de consulta
        /// </summary>
        private DataTable FilterDataTableByExpression(DataTable source, string expression)
        {
            string cleanExpression = Utils.CleanExpression(expression);

            // Crear una vista de datos filtrada
            DataView view = new DataView(source);
            view.RowFilter = cleanExpression;

            // Convertir la vista filtrada a un nuevo DataTable
            DataTable result = view.ToTable();
            return result;
        }
    }
}
