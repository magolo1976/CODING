using System.Data;
using OxyPlot.Series;
using OxyPlot;
using MTTCommon_objects;
using OxyPlot.Axes;
using MTT_LoadData;

namespace MTT01_winforms.UControls.Calculo
{
    public partial class UCAnalisisDeDatos : UserControl
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

        public UCAnalisisDeDatos()
        {
            InitializeComponent();

            InitialLabels();
        }

        private void btnLoadCSV_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                DataTable dataTable = DataCSV.CsvToDataTable(openFileDialog1.FileName);

                CandleList = DataCandle.ListFromDataTable(dataTable);

                dataGridView.DataSource = dataTable;

                List<double> listEstacionalidad = InitializeLine();

                double fijo = listEstacionalidad[listEstacionalidad.Count - 1];
                List<double> datosFiltrados = new List<double>();

                double total_posi = 0;
                double total_nega = 0;

                for (int cd = 0; cd < listEstacionalidad.Count - 1; cd++)
                {
                    if (listEstacionalidad[cd] <= fijo)
                    {
                        double data_speci = listEstacionalidad[cd + 1];
                        datosFiltrados.Add(data_speci);

                        if (data_speci > 0) { total_posi++; }
                        else { total_nega++; }
                    }
                }

                InitialLabels();

                if (total_nega > total_posi)
                {
                    lblPercentBajista.Text = Math.Round((total_nega / datosFiltrados.Count) * 100, 1).ToString() + "%";
                    lblPercentBajista.Visible = true;
                    lblMsgBajista.Visible = true;
                }
                else
                {
                    lblPercentAlcista.Text = Math.Round((total_posi / datosFiltrados.Count) * 100, 1).ToString() + "%";
                    lblPercentAlcista.Visible = true;
                    lblMsgAlcista.Visible = true;
                }

            }
        }

        private void InitialLabels()
        {
            lblPercentBajista.Visible = false;
            lblPercentAlcista.Visible = false;
            lblMsgAlcista.Visible = false;
            lblMsgBajista.Visible = false;
        }

        #region Line

        private List<double> InitializeLine()
        {
            //  Preparación de los datos
            List<CandleMT4Data> DiffDaysList = new List<CandleMT4Data>();
            List<double> listEstacionalidad = new List<double>();

            for (int c = CandleList.Count - 2; c >= 0; c--)
            {
                CandleMT4Data candle = new CandleMT4Data()
                {
                    Date = CandleList[c].Date,
                    Open = CandleList[c].Open - CandleList[c + 1].Open
                };

                DiffDaysList.Add(candle);
                listEstacionalidad.Add(candle.Open);
            }

            int maxUp = (int)DiffDaysList.Max(x => x.Open);
            int minDw = (int)DiffDaysList.Min(x => x.Open);

            //  Diseño
            var model = new PlotModel { Title = "Estacionalidad" };

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

            // Línea central
            var lineZero = new LineSeries
            {
                Title = "Zero",
                MarkerType = MarkerType.None,
                MarkerSize = 3
            };

            var lineA2 = new LineSeries
            {
                Title = "A2",
                MarkerType = MarkerType.None,
                MarkerSize = 2
            };

            var lineA1 = new LineSeries
            {
                Title = "A1",
                MarkerType = MarkerType.None,
                MarkerSize = 2
            };

            var lineB1 = new LineSeries
            {
                Title = "B1",
                MarkerType = MarkerType.None,
                MarkerSize = 2
            };

            var lineB2 = new LineSeries
            {
                Title = "B2",
                MarkerType = MarkerType.None,
                MarkerSize = 2
            };

            // Obtener los datos y añadirlos a la serie
            foreach (CandleMT4Data valor in DiffDaysList)
            {
                lineOpens.Points.Add(new DataPoint(DateTimeAxis.ToDouble(valor.Date), valor.Open));

                lineZero.Points.Add(new DataPoint(DateTimeAxis.ToDouble(valor.Date), 0.0));
                lineA2.Points.Add(new DataPoint(DateTimeAxis.ToDouble(valor.Date), maxUp));
                lineA1.Points.Add(new DataPoint(DateTimeAxis.ToDouble(valor.Date), maxUp / 2));
                lineB1.Points.Add(new DataPoint(DateTimeAxis.ToDouble(valor.Date), minDw / 2));
                lineB2.Points.Add(new DataPoint(DateTimeAxis.ToDouble(valor.Date), minDw));
            }

            model.Series.Add(lineOpens);
            model.Series.Add(lineZero);
            model.Series.Add(lineA2);
            model.Series.Add(lineA1);
            model.Series.Add(lineB1);
            model.Series.Add(lineB2);

            // Crear y añadir el PlotView al formulario
            plotViewIntegracion.Model = model;
            plotViewIntegracion.Dock = DockStyle.Fill;

            return listEstacionalidad;
        }

        #endregion


    }
}
