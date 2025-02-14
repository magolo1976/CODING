using System.Data;
using OxyPlot.Series;
using OxyPlot;
using MTTCommon_objects;
using OxyPlot.Axes;
using MTT_LoadData;
using MTT_Calculo;
using static MTTCommon_objects.Enumerations;

namespace MTT01_winforms.UControls.Data
{
    public partial class UCLoadCSV : UserControl
    {
        private static List<CandleMT4Data> _candleList;
        private static List<CandleMT4Data> CandleList
        {
            get
            {
                if (_candleList == null) { _candleList = new List<CandleMT4Data>(); }
                return _candleList;
            }
            set { _candleList = value; }
        }

        public UCLoadCSV()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }

        private void btnLoadCSV_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                DataTable dataTable = DataCSV.CsvToDataTable(openFileDialog1.FileName);

                CandleList = DataCandle.ListFromDataTable(dataTable);

                dataGridView.DataSource = dataTable;

                InitializeLineChart();

                InitializeLineGauss();

                //var plotModel = new PlotModel { Title = "Ejemplo de Gráfico de Líneas" };
                //var lineSeries = new LineSeries();

                //// Agregar datos a la serie
                //foreach (DataRow row in dataTable.Rows)
                //{
                //    lineSeries.Points.Add(new DataPoint(Convert.ToDouble(row["Date"]), Convert.ToDouble(row["Open"])));
                //}

                //plotModel.Series.Add(lineSeries);

                //plotView.Model = plotModel;


                //var pm = new PlotModel
                //{
                //    Title = "Trigonometric functions",
                //    Subtitle = "Example using the FunctionSeries",
                //    PlotType = PlotType.Cartesian,
                //    Background = OxyColors.White
                //};
                //pm.Series.Add(new FunctionSeries(Math.Sin, -10, 10, 0.1, "sin(x)"));
                //pm.Series.Add(new FunctionSeries(Math.Cos, -10, 10, 0.1, "cos(x)"));
                //pm.Series.Add(new FunctionSeries(t => 5 * Math.Cos(t), t => 5 * Math.Sin(t), 0, 2 * Math.PI, 0.1, "cos(t),sin(t)"));
                //plot1.Model = pm;

            }
        }

        #region Line Gauss

        private void InitializeLineGauss()
        {
            var model = new PlotModel { Title = "Campana de Gauss" };

            // Configuración del eje X (DateTime Axis)
            model.Axes.Add(new DateTimeAxis
            {
                Position = AxisPosition.Bottom,
                StringFormat = "yyyy\nMM-dd\nhh:mm",
                Title = "Fecha",
                MinorIntervalType = DateTimeIntervalType.Auto,
                IntervalType = DateTimeIntervalType.Auto,
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Dot
            });

            // Configuración del eje Y
            model.Axes.Add(new LinearAxis
            {
                Position = AxisPosition.Left,
                Title = "Valor"
            });

            // Crear y añadir la serie de líneas
            var lineOpens = new LineSeries
            {
                Title = "Open",
                MarkerType = MarkerType.Circle,
                MarkerSize = 3
            };

            Dictionary<DateTime,double> listaEspecifica = Estadistica.CampanaDeGaussExtendida(CandleList, DatoCalulado.High_Low);

            // Obtener los datos y añadirlos a la serie
            foreach (KeyValuePair<DateTime, double> valor in listaEspecifica)
            {
                lineOpens.Points.Add(new DataPoint(DateTimeAxis.ToDouble(valor.Key), valor.Value));
            }

            model.Series.Add(lineOpens);

            // Crear y añadir el PlotView al formulario
            plotViewGauss.Model = model;
            plotViewGauss.Dock = DockStyle.Fill;
        }

        #endregion

        #region Line HLOC

        private void InitializeLineChart()
        {
            var model = new PlotModel { Title = "Gráfico de Líneas" };

            // Configuración del eje X (DateTime Axis)
            model.Axes.Add(new DateTimeAxis
            {
                Position = AxisPosition.Bottom,
                StringFormat = "yyyy\nMM-dd\nhh:mm",
                Title = "Fecha",
                MinorIntervalType = DateTimeIntervalType.Auto,
                IntervalType = DateTimeIntervalType.Auto,
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Dot
            });

            // Configuración del eje Y
            model.Axes.Add(new LinearAxis
            {
                Position = AxisPosition.Left,
                Title = "Valor"
            });

            // Crear y añadir la serie de líneas
            var lineOpens = new LineSeries
            {
                Title = "Open",
                MarkerType = MarkerType.Circle,
                MarkerSize = 3
            };
            var lineCloses = new LineSeries
            {
                Title = "Close",
                MarkerType = MarkerType.Circle,
                MarkerSize = 3
            };
            var lineHights = new LineSeries
            {
                Title = "Hight",
                MarkerType = MarkerType.Circle,
                MarkerSize = 3
            };
            var lineLows = new LineSeries
            {
                Title = "Lows",
                MarkerType = MarkerType.Circle,
                MarkerSize = 3
            };

            // Obtener los datos y añadirlos a la serie
            foreach (CandleMT4Data candle in CandleList)
            {
                lineOpens.Points.Add(new DataPoint(DateTimeAxis.ToDouble(candle.Date), candle.Open));
                lineCloses.Points.Add(new DataPoint(DateTimeAxis.ToDouble(candle.Date), candle.Close));
                lineHights.Points.Add(new DataPoint(DateTimeAxis.ToDouble(candle.Date), candle.High));
                lineLows.Points.Add(new DataPoint(DateTimeAxis.ToDouble(candle.Date), candle.Low));
            }

            model.Series.Add(lineOpens);
            model.Series.Add(lineCloses);
            model.Series.Add(lineLows);
            model.Series.Add(lineHights);

            // Crear y añadir el PlotView al formulario
            plotViewHLOC.Model = model;
            plotViewHLOC.Dock = DockStyle.Fill;
        }

        private void checkOpen_CheckedChanged(object sender, EventArgs e)
        {//0
            plotViewHLOC.Model.Series[0].IsVisible = checkOpen.Checked;
            plotViewHLOC.Refresh();
        }

        private void checkHight_CheckedChanged(object sender, EventArgs e)
        {//3
            plotViewHLOC.Model.Series[3].IsVisible = checkHight.Checked;
            plotViewHLOC.Refresh();
        }

        private void checkLow_CheckedChanged(object sender, EventArgs e)
        {//2
            plotViewHLOC.Model.Series[2].IsVisible = checkLow.Checked;
            plotViewHLOC.Refresh();
        }

        private void checkClose_CheckedChanged(object sender, EventArgs e)
        {//1
            plotViewHLOC.Model.Series[1].IsVisible = checkClose.Checked;
            plotViewHLOC.Refresh();
        }

        #endregion


        /* 
         **************
         CANDLESTICKS
         **************
        var model = new PlotModel { Title = "Gráfica de Velas" };

        // Añadir ejes
        model.Axes.Add(new DateTimeAxis
        {
            Position = AxisPosition.Bottom,
            StringFormat = "yyyy-MM-dd HH:mm",
            Title = "Fecha y Hora",
            MinorIntervalType = DateTimeIntervalType.Auto,
            IntervalType = DateTimeIntervalType.Auto,
            MajorGridlineStyle = LineStyle.Solid,
            MinorGridlineStyle = LineStyle.Dot
        });
        model.Axes.Add(new LinearAxis
        {
            Position = AxisPosition.Left,
            Title = "Precio"
        });

        var series = new CandleStickSeries
        {
            Title = "Datos",
            StrokeThickness = 1,
            DecreasingColor = OxyColors.Red,
            IncreasingColor = OxyColors.Green
        };
        foreach (CandleMT4Data candle in CandleList)
        {
            series.Items.Add(new HighLowItem(DateTimeAxis.ToDouble(candle.Date), candle.High, candle.Low, candle.Open, candle.Close));
        }

        plotView.Model = model;
        plotView.Dock = DockStyle.Fill;
         */
    }
}
