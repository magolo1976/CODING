
using OxyPlot.Axes;
using OxyPlot.Legends;
using OxyPlot.Series;
using OxyPlot;
using System.Data;
using MagoloRuleExtraction.Classes;
using System.Windows.Forms;

namespace MagoloRuleExtraction.Sections.RuleExtraction
{
    public partial class RuleExtractionUC: UserControl
    {
        // Datos y estado
        private DataTable dataFrame;
        List<double> trainReturns;
        private Dictionary<string, RuleInfo> primaryRules;
        private DataTable compoundRulesDataframe;
        private double compoundThreshold;
        private double[] randomMetrics;
        private string dateColumn;
        private string side;
        private List<bool> maskTrain;

        public RuleExtractionUC()
        {
            InitializeComponent();
        }

        // Método para inicializar el control con datos
        public void Initialize(DataTable dataFrame, List<double> trainReturns, Dictionary<string, Dictionary<string, object>> primaryRules,
                              string dateColumn, string side, List<bool> maskTrain)
        {
            this.dataFrame = dataFrame;
            this.trainReturns = trainReturns;
            this.dateColumn = dateColumn;
            this.side = side;
            this.maskTrain = maskTrain;

            this.primaryRules = GetRulesInfo(primaryRules);

            // Llenar el combo de features
            FillFeaturesCombo();

            // Calcular distribución de monos si no existe
            if (randomMetrics == null)
            {
                CalculateMonkeyDistribution();
            }
        }

        private Dictionary<string, RuleInfo> GetRulesInfo(Dictionary<string, Dictionary<string, object>> primaryRules)
        {
            Dictionary<string, RuleInfo> retValues = new Dictionary<string, RuleInfo>();

            foreach (KeyValuePair<string, Dictionary<string, object>> key1 in primaryRules)
            {
                RuleInfo ruleInfo = new RuleInfo { 
                    Rule = key1.Value.ToArray()[0].Value.ToString(), 
                    Score = double.Parse(key1.Value.ToArray()[1].Value.ToString()) 
                };

                retValues.Add(key1.Key, ruleInfo);
            }

            return retValues;
        }

        private void FillFeaturesCombo()
        {
            cmbFeatureBase.Items.Clear();

            if (primaryRules != null && primaryRules.Count > 0)
            {
                // Ordenar features por score descendente
                var sortedFeatures = primaryRules.OrderByDescending(x => x.Value.Score).ToList();

                foreach (var feature in sortedFeatures)
                {
                    cmbFeatureBase.Items.Add($"{feature.Key} (Score: {feature.Value.Score:F2})");
                }

                if (cmbFeatureBase.Items.Count > 0)
                    cmbFeatureBase.SelectedIndex = 0;

                // Mostrar info sobre features disponibles
                lblInfo.Text = $"Se buscarán reglas compuestas usando las {primaryRules.Count - 1} features restantes que pasaron el filtro inicial.";
            }
            else
            {
                lblInfo.Text = "Por favor, completa el análisis de features primero";
                btnBuscarReglas.Enabled = false;
            }
        }

        private void CalculateMonkeyDistribution()
        {
            using (var loadingForm = new LoadingForm("Calculando distribución de monos..."))
            {
                loadingForm.Show();
                Application.DoEvents();

                // Llamada a la función que calcula la distribución de monos
                randomMetrics = new Calculate_Random_Metrics_Compound().DoWork(trainReturns, side);
                compoundThreshold = CalculateQuantile(randomMetrics, 0.99);

                // Actualizar gráfico
                UpdateMonkeyDistributionPlot();

                loadingForm.Close();
            }
        }

        private double CalculateQuantile(double[] values, double quantile)
        {
            // Función para calcular el cuantil
            Array.Sort(values);
            int pos = (int)(values.Length * quantile);
            return values[pos];
        }

        private void UpdateMonkeyDistributionPlot()
        {
            var plotModel = new Plot_Monkey_Distribution().DoWork(
                randomMetrics, compoundThreshold, "train"
                );
            // Actualizar plot
            plotMonkeyDistribution.Model = plotModel;
            plotMonkeyDistribution.InvalidatePlot(true);

            //var plotModel = new PlotModel { Title = "Distribución de Métricas Aleatorias" };

            //// Crear histograma
            //var histogramSeries = new HistogramSeries
            //{
            //    StrokeThickness = 1,
            //    StrokeColor = OxyColors.Black,
            //    FillColor = OxyColor.FromRgb(135, 206, 235),
            //    ColumnWidth = 0.02
            //};

            //// Añadir datos
            //histogramSeries.Items.AddRange(randomMetrics.Select(x => new HistogramItem(x, 1)));

            //// Añadir línea de threshold
            //var thresholdSeries = new LineSeries
            //{
            //    Color = OxyColors.Red,
            //    StrokeThickness = 2,
            //    Title = $"Umbral (99%): {compoundThreshold:F3}"
            //};
            //thresholdSeries.Points.Add(new DataPoint(compoundThreshold, 0));
            //thresholdSeries.Points.Add(new DataPoint(compoundThreshold, randomMetrics.Length * 0.1)); // Altura aproximada

            //// Configurar ejes
            //plotModel.Axes.Add(new LinearAxis
            //{
            //    Position = AxisPosition.Bottom,
            //    Title = "Métrica"
            //});
            //plotModel.Axes.Add(new LinearAxis
            //{
            //    Position = AxisPosition.Left,
            //    Title = "Frecuencia"
            //});

            //// Añadir series
            //plotModel.Series.Add(histogramSeries);
            //plotModel.Series.Add(thresholdSeries);

            //// Actualizar plot
            //plotMonkeyDistribution.Model = plotModel;
            //plotMonkeyDistribution.InvalidatePlot(true);
        }

        private void BtnBuscarReglas_Click(object sender, EventArgs e)
        {
            if (cmbFeatureBase.SelectedIndex < 0)
                return;

            Task.Run(() =>
            {
                using (var loadingForm = new LoadingForm("Buscando reglas compuestas..."))
                {
                    loadingForm.Show();
                    Application.DoEvents();

                    // Obtener feature base seleccionada
                    string selectedFeatureWithScore = cmbFeatureBase.SelectedItem.ToString();
                    string selectedFeature = selectedFeatureWithScore.Substring(0, selectedFeatureWithScore.IndexOf(" ("));

                    // Obtener regla base
                    string baseRule = primaryRules[selectedFeature].Rule;

                    // Preparar lista de features filtradas
                    List<string> filteredFeatures = primaryRules.Keys.ToList();

                    // Buscar reglas compuestas
                    DataTable compoundRulesDf = new Find_Second_Rule().DoWork(
                                                                dataFrame,
                                                                selectedFeature,
                                                                baseRule,
                                                                "Target",
                                                                dateColumn,
                                                                side,
                                                                compoundThreshold,
                                                                filteredFeatures
                                                            );


                    // Guardar y mostrar reglas
                    compoundRulesDataframe = compoundRulesDf;
                    UpdateCompoundRulesGrid();
                    UpdateRuleSelectionCombo();

                    loadingForm.Close();
                }
            });
        }

        private void UpdateCompoundRulesGrid()
        {
            if (compoundRulesDataframe == null || compoundRulesDataframe.Rows.Count == 0)
            {
                MessageBox.Show("No se encontraron reglas compuestas que superen el umbral",
                               "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            dgvCompoundRules.DataSource = compoundRulesDataframe;
            dgvCompoundRules.Refresh();
        }

        private void UpdateRuleSelectionCombo()
        {
            cmbSelectedRule.Items.Clear();

            if (compoundRulesDataframe != null && compoundRulesDataframe.Rows.Count > 0)
            {
                for (int i = 0; i < compoundRulesDataframe.Rows.Count; i++)
                {
                    double metric = Convert.ToDouble(compoundRulesDataframe.Rows[i]["metric"]);
                    string rule = compoundRulesDataframe.Rows[i]["rule"].ToString();
                    cmbSelectedRule.Items.Add($"Métrica: {metric:F3} | {rule}");
                }

                if (cmbSelectedRule.Items.Count > 0)
                    cmbSelectedRule.SelectedIndex = 0;
            }
        }

        private void CmbSelectedRule_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSelectedRule.SelectedIndex >= 0 && compoundRulesDataframe != null)
            {
                string rule = compoundRulesDataframe.Rows[cmbSelectedRule.SelectedIndex]["rule"].ToString();
                PlotRuleReturns(rule);
            }
        }

        private void PlotRuleReturns(string rule)
        {
            // Llamar a la función para obtener los datos de la evolución de la regla
            PlotModel plotModel = new Plot_Rule_Returns().DoWork(dataFrame, rule, dateColumn, side, maskTrain);

            if (plotModel == null)
            {
                MessageBox.Show("No hay suficientes datos para mostrar la evolución",
                               "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Actualizar gráfico
            plotRuleReturns.Model = plotModel;
            plotRuleReturns.InvalidatePlot(true);


            //// Crear nuevo modelo para el gráfico
            //var plotModel = new PlotModel { Title = "Evolución de la Regla" };

            //// Configurar ejes
            //plotModel.Axes.Add(new DateTimeAxis
            //{
            //    Position = AxisPosition.Bottom,
            //    Title = "Fecha",
            //    StringFormat = "yyyy-MM-dd"
            //});

            //plotModel.Axes.Add(new LinearAxis
            //{
            //    Position = AxisPosition.Left,
            //    Title = "Retorno Acumulado (%)"
            //});

            //// Crear series
            //var ruleSeries = new LineSeries
            //{
            //    Title = "Regla",
            //    Color = OxyColors.Green,
            //    StrokeThickness = 2
            //};

            //var benchmarkSeries = new LineSeries
            //{
            //    Title = "Benchmark",
            //    Color = OxyColors.Blue,
            //    StrokeThickness = 1
            //};

            //// Llenar las series con datos
            //foreach (var point in plotData)
            //{
            //    DateTime date = Convert.ToDateTime(point.Date);
            //    double dateValue = DateTimeAxis.ToDouble(date);

            //    ruleSeries.Points.Add(new DataPoint(dateValue, point.RuleReturn * 100));
            //    benchmarkSeries.Points.Add(new DataPoint(dateValue, point.BenchmarkReturn * 100));
            //}

            //// Añadir series al modelo
            //plotModel.Series.Add(ruleSeries);
            //plotModel.Series.Add(benchmarkSeries);

            //// Añadir leyenda
            //plotModel.Legends.Add(new Legend
            //{
            //    LegendPosition = LegendPosition.TopRight
            //});

            //// Actualizar gráfico
            //plotRuleReturns.Model = plotModel;
            //plotRuleReturns.InvalidatePlot(true);
        }
    }

    // Clase para el punto de datos de retorno
    public class ReturnPoint
    {
        public DateTime Date { get; set; }
        public double RuleReturn { get; set; }
        public double BenchmarkReturn { get; set; }
    }

    // Form de carga
    public class LoadingForm : Form
    {
        public LoadingForm(string message)
        {
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.ControlBox = false;
            this.Size = new Size(300, 100);

            Label lblMessage = new Label
            {
                Text = message,
                AutoSize = true,
                Location = new Point(50, 20)
            };

            ProgressBar progressBar = new ProgressBar
            {
                Style = ProgressBarStyle.Marquee,
                Location = new Point(50, 50),
                Size = new Size(200, 20)
            };

            this.Controls.Add(lblMessage);
            this.Controls.Add(progressBar);
        }
    }

}
