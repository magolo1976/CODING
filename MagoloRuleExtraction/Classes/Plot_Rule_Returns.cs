
using OxyPlot.Axes;
using OxyPlot.Legends;
using OxyPlot.Series;
using OxyPlot;
using System.Data;
using OxyPlot.WindowsForms;

namespace MagoloRuleExtraction.Classes
{
    public class Plot_Rule_Returns
    {
        /// <summary>
        /// Grafica la evolución de los retornos acumulados para una regla en train y test
        /// </summary>
        /// <param name="df">DataTable con todos los datos</param>
        /// <param name="rule">Regla a evaluar</param>
        /// <param name="dateColumn">Nombre de la columna de fecha</param>
        /// <param name="side">Dirección de la operación: 'long' o 'short'</param>
        /// <param name="maskTrain">Lista de booleanos que indica qué filas pertenecen al conjunto de entrenamiento</param>
        /// <returns>Modelo de gráfico OxyPlot o null si no hay datos</returns>
        public PlotModel DoWork(DataTable df, string rule, string dateColumn, string side, List<bool> maskTrain)
        {
            // Filtrar datos según la regla
            DataTable dfFiltered = FilterDataTableByExpression(df, rule);

            if (dfFiltered.Rows.Count == 0)
            {
                return null;
            }

            double sideMultiplier = side == "long" ? 1 : -1;

            // Agregar columna de retornos ajustados
            dfFiltered.Columns.Add("adjusted_returns", typeof(double));
            for (int i = 0; i < dfFiltered.Rows.Count; i++)
            {
                dfFiltered.Rows[i]["adjusted_returns"] = Convert.ToDouble(dfFiltered.Rows[i]["Target"]) * sideMultiplier;
            }

            // Agregar columna de período (train/test)
            dfFiltered.Columns.Add("period", typeof(string));
            for (int i = 0; i < dfFiltered.Rows.Count; i++)
            {
                int originalIndex = df.Rows.IndexOf(dfFiltered.Rows[i]);
                dfFiltered.Rows[i]["period"] = (originalIndex < maskTrain.Count && maskTrain[originalIndex]) ? "train" : "test";
            }

            // Calcular retornos acumulados
            dfFiltered.Columns.Add("cumulative_returns", typeof(double));
            double cumulativeSum = 0;
            for (int i = 0; i < dfFiltered.Rows.Count; i++)
            {
                cumulativeSum += Convert.ToDouble(dfFiltered.Rows[i]["adjusted_returns"]);
                dfFiltered.Rows[i]["cumulative_returns"] = cumulativeSum;
            }

            // Crear el modelo de gráfico
            var plotModel = new PlotModel
            {
                Title = "Evolución del Retorno Acumulado"
            };

            // Crear y configurar la leyenda
            plotModel.Legends.Add(new Legend
            {
                LegendPlacement = LegendPlacement.Outside,
                LegendPosition = LegendPosition.BottomCenter,
                LegendOrientation = LegendOrientation.Horizontal
            });

            // Configurar eje X (fecha)
            var dateAxis = new DateTimeAxis
            {
                Position = AxisPosition.Bottom,
                Title = "Fecha",
                StringFormat = "yyyy-MM-dd"
            };
            plotModel.Axes.Add(dateAxis);

            // Configurar eje Y (retorno acumulado)
            var valueAxis = new LinearAxis
            {
                Position = AxisPosition.Left,
                Title = "Retorno Acumulado (%)"
            };
            plotModel.Axes.Add(valueAxis);

            // Filtrar datos de entrenamiento
            DataView trainView = new DataView(dfFiltered);
            trainView.RowFilter = "period = 'train'";
            DataTable dfTrain = trainView.ToTable();

            // Agregar serie para datos de entrenamiento
            if (dfTrain.Rows.Count > 0)
            {
                var trainSeries = new LineSeries
                {
                    Title = "Train",
                    Color = OxyColors.Blue,
                    StrokeThickness = 2
                };

                for (int i = 0; i < dfTrain.Rows.Count; i++)
                {
                    DateTime date = Convert.ToDateTime(dfTrain.Rows[i][dateColumn]);
                    double value = Convert.ToDouble(dfTrain.Rows[i]["cumulative_returns"]);
                    trainSeries.Points.Add(new DataPoint(DateTimeAxis.ToDouble(date), value));
                }

                plotModel.Series.Add(trainSeries);
            }

            // Filtrar datos de prueba
            DataView testView = new DataView(dfFiltered);
            testView.RowFilter = "period = 'test'";
            DataTable dfTest = testView.ToTable();

            // Agregar serie para datos de prueba
            if (dfTest.Rows.Count > 0)
            {
                var testSeries = new LineSeries
                {
                    Title = "Test",
                    Color = OxyColors.Gold,
                    StrokeThickness = 2
                };

                for (int i = 0; i < dfTest.Rows.Count; i++)
                {
                    DateTime date = Convert.ToDateTime(dfTest.Rows[i][dateColumn]);
                    double value = Convert.ToDouble(dfTest.Rows[i]["cumulative_returns"]);
                    testSeries.Points.Add(new DataPoint(DateTimeAxis.ToDouble(date), value));
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
            // Reemplazar los backticks en la expresión por nombres de columna
            string cleanExpression = expression.Replace("`", "");

            // Crear una vista de datos filtrada
            DataView view = new DataView(source);
            view.RowFilter = cleanExpression;

            // Convertir la vista filtrada a un nuevo DataTable
            DataTable result = view.ToTable();
            return result;
        }

        /// <summary>
        /// Método auxiliar para crear un control PlotView a partir del modelo
        /// Útil para integrar directamente en Windows Forms
        /// </summary>
        public PlotView CreatePlotView(PlotModel plotModel)
        {
            var plotView = new PlotView
            {
                Model = plotModel,
                Dock = System.Windows.Forms.DockStyle.Fill
            };

            return plotView;
        }

        ///// <summary>
        ///// Grafica la evolución de los retornos acumulados para una regla en train y test
        ///// </summary>
        ///// <param name="df">DataTable con los datos</param>
        ///// <param name="rule">Regla a evaluar</param>
        ///// <param name="dateColumn">Nombre de la columna de fecha</param>
        ///// <param name="side">Dirección del trading ('long' o 'short')</param>
        ///// <param name="maskTrain">Array booleano que indica filas de entrenamiento</param>
        ///// <returns>Modelo de gráfico PlotModel o null si no hay datos suficientes</returns>
        //public PlotModel DoWork(DataTable df, string rule, string dateColumn, string side, bool[] maskTrain)
        //{
        //    // Filtrar datos por la regla
        //    DataTable filteredDf = FilterDataByRule(df, rule);

        //    if (filteredDf.Rows.Count == 0)
        //    {
        //        return null;
        //    }

        //    double sideMultiplier = side == "long" ? 1.0 : -1.0;

        //    // Crear nuevas columnas en la tabla filtrada
        //    filteredDf.Columns.Add("adjusted_returns", typeof(double));
        //    filteredDf.Columns.Add("period", typeof(string));
        //    filteredDf.Columns.Add("cumulative_returns", typeof(double));

        //    // Calcular retornos ajustados
        //    for (int i = 0; i < filteredDf.Rows.Count; i++)
        //    {
        //        double targetValue = Convert.ToDouble(filteredDf.Rows[i]["Target"]);
        //        filteredDf.Rows[i]["adjusted_returns"] = targetValue * sideMultiplier;

        //        // Asignar período (train/test) a cada fila
        //        int originalIndex = GetOriginalIndex(df, filteredDf.Rows[i]);
        //        filteredDf.Rows[i]["period"] = maskTrain[originalIndex] ? "train" : "test";
        //    }

        //    // Calcular retornos acumulados por período
        //    CalculateCumulativeReturns(filteredDf, "train");
        //    CalculateCumulativeReturns(filteredDf, "test");

        //    // Crear el modelo de gráfico
        //    PlotModel plotModel = new PlotModel
        //    {
        //        Title = "Evolución del Retorno Acumulado",
        //        LegendPosition = LegendPosition.RightTop,
        //        LegendPlacement = LegendPlacement.Inside
        //    };

        //    //// Añadir ejes
        //    //plotModel.Axes.Add(new DateTimeAxis
        //    //{
        //    //    Position = AxisPosition.Bottom,
        //    //    Title = "Fecha",
        //    //    StringFormat = "dd/MM/yyyy"
        //    //});

        //    //plotModel.Axes.Add(new LinearAxis
        //    //{
        //    //    Position = AxisPosition.Left,
        //    //    Title = "Retorno Acumulado (%)"
        //    //});

        //    //// Graficar datos de entrenamiento
        //    //AddLineSeries(plotModel, filteredDf, dateColumn, "train", OxyColors.Blue);

        //    //// Graficar datos de prueba
        //    //AddLineSeries(plotModel, filteredDf, dateColumn, "test", OxyColors.Gold);

        //    return plotModel;

        //}

        ///// <summary>
        ///// Filtra datos según una regla expresada como condición
        ///// </summary>
        //private DataTable FilterDataByRule(DataTable data, string rule)
        //{
        //    // Convertir la regla a formato de expresión compatible con DataTable
        //    string expression = ConvertRuleToExpression(rule);

        //    // Filtrar usando DataView
        //    DataView view = new DataView(data);
        //    view.RowFilter = expression;

        //    return view.ToTable();
        //}

        ///// <summary>
        ///// Convierte una regla en formato Python a una expresión compatible con DataView.RowFilter
        ///// </summary>
        //private string ConvertRuleToExpression(string rule)
        //{
        //    // Reemplazar comillas invertidas y operadores lógicos
        //    string expression = rule.Replace("`", "")
        //                           .Replace(" and ", " AND ")
        //                           .Replace(" or ", " OR ");

        //    return expression;
        //}

        ///// <summary>
        ///// Obtiene el índice original de una fila en la tabla principal
        ///// </summary>
        //private int GetOriginalIndex(DataTable originalTable, DataRow row)
        //{
        //    // Comparar todas las columnas para identificar la fila
        //    for (int i = 0; i < originalTable.Rows.Count; i++)
        //    {
        //        bool match = true;
        //        foreach (DataColumn col in originalTable.Columns)
        //        {
        //            if (!row[col.ColumnName].Equals(originalTable.Rows[i][col.ColumnName]))
        //            {
        //                match = false;
        //                break;
        //            }
        //        }

        //        if (match)
        //            return i;
        //    }

        //    return -1;
        //}

        ///// <summary>
        ///// Calcula los retornos acumulados para un período específico
        ///// </summary>
        //private void CalculateCumulativeReturns(DataTable df, string period)
        //{
        //    double cumulativeReturn = 0;

        //    // Filtrar por período y ordenar por fecha
        //    var rows = df.AsEnumerable()
        //        .Where(row => row.Field<string>("period") == period)
        //        .OrderBy(row => row.Field<DateTime>(df.Columns.Contains("Date") ? "Date" : df.Columns[0].ColumnName));

        //    foreach (var row in rows)
        //    {
        //        cumulativeReturn += row.Field<double>("adjusted_returns");
        //        row["cumulative_returns"] = cumulativeReturn;
        //    }
        //}

        ///// <summary>
        ///// Añade una serie de líneas al gráfico para un período específico
        ///// </summary>
        //private void AddLineSeries(PlotModel plotModel, DataTable df, string dateColumn, string period, OxyColor color)
        //{
        //    var rows = df.AsEnumerable()
        //        .Where(row => row.Field<string>("period") == period)
        //        .OrderBy(row => row.Field<DateTime>(dateColumn));

        //    if (!rows.Any())
        //        return;

        //    LineSeries series = new LineSeries
        //    {
        //        Title = period,
        //        Color = color,
        //        StrokeThickness = 2
        //    };

        //    foreach (var row in rows)
        //    {
        //        DateTime date = row.Field<DateTime>(dateColumn);
        //        double value = row.Field<double>("cumulative_returns");
        //        series.Points.Add(new DataPoint(DateTimeAxis.ToDouble(date), value));
        //    }

        //    plotModel.Series.Add(series);
        //}
    }
}
