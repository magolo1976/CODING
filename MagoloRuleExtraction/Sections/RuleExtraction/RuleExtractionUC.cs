using OxyPlot;
using System.Data;
using MagoloRuleExtraction.Classes;
using OxyPlot.Series;

namespace MagoloRuleExtraction.Sections.RuleExtraction
{
    public partial class RuleExtractionUC: UserControl
    {
        #region Properties

        // Datos y estado
        private List<double> TrainReturns;
        private Dictionary<string, RuleInfo> PrimaryRules;
        private List<bool> MaskTrain;
        private List<bool> MaskTest;
        private DataTable DataTrainFrame;
        private DataTable DataTestFrame;
        private DataTable CompoundRulesDataframe;
        private double[] RandomMetrics;
        private double CompoundThreshold;
        private double CorrelationThreshold;
        private string DateColumn;
        private string Side;

        #endregion

        public RuleExtractionUC()
        {
            InitializeComponent();
        }

        // Método para inicializar el control con datos
        public void Initialize(DataTable dataTrainFrame, DataTable dataTestFrame, List<double> trainReturns, Dictionary<string, Dictionary<string, object>> primaryRules,
                              string dateColumn, string side, List<bool> maskTrain, List<bool> maskTest, double correlationThreshold)
        {
            DataTrainFrame = dataTrainFrame;
            DataTestFrame = dataTestFrame;
            TrainReturns = trainReturns;
            DateColumn = dateColumn;
            Side = side;
            MaskTrain = maskTrain;
            MaskTest = maskTest;
            CorrelationThreshold = correlationThreshold;

            PrimaryRules = GetRulesInfo(primaryRules);

            // Llenar el combo de features
            FillFeaturesCombo();

            // Calcular distribución de monos
            CalculateMonkeyDistribution();

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

            if (PrimaryRules != null && PrimaryRules.Count > 0)
            {
                // Ordenar features por score descendente
                var sortedFeatures = PrimaryRules.OrderByDescending(x => x.Value.Score).ToList();

                foreach (var feature in sortedFeatures)
                {
                    cmbFeatureBase.Items.Add($"{feature.Key} (Score: {feature.Value.Score:F2})");
                }

                if (cmbFeatureBase.Items.Count > 0)
                    cmbFeatureBase.SelectedIndex = 0;

                // Mostrar info sobre features disponibles
                lblInfo.Text = $"Se buscarán reglas compuestas usando las {PrimaryRules.Count - 1} features restantes que pasaron el filtro inicial.";
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
                RandomMetrics = new Calculate_Random_Metrics_Compound().DoWork(TrainReturns, Side);
                CompoundThreshold = Math.Round(CalculateQuantile(RandomMetrics, CorrelationThreshold), 3);

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
            var plotModel = new Plot_Monkey_Distribution().DoWork(RandomMetrics, CompoundThreshold, CorrelationThreshold, "train");

            // Actualizar plot
            plotMonkeyDistribution.Model = plotModel;
            plotMonkeyDistribution.InvalidatePlot(true);
        }

        private void BtnBuscarReglas_Click(object sender, EventArgs e)
        {
            if (cmbFeatureBase.SelectedIndex < 0)
                return;

            using (var loadingForm = new LoadingForm("Buscando reglas compuestas..."))
            {
                loadingForm.Show();
                Application.DoEvents();

                // Obtener feature base seleccionada
                string selectedFeatureWithScore = cmbFeatureBase.SelectedItem.ToString();
                string selectedFeature = selectedFeatureWithScore.Substring(0, selectedFeatureWithScore.IndexOf(" ("));

                // Obtener regla base
                string baseRule = PrimaryRules[selectedFeature].Rule;

                // Preparar lista de features filtradas
                List<string> filteredFeatures = PrimaryRules.Keys.ToList();

                // Buscar reglas compuestas
                CompoundRulesDataframe = new Find_Second_Rule().DoWork(
                                                            DataTrainFrame,
                                                            selectedFeature,
                                                            baseRule,
                                                            "Target",
                                                            DateColumn,
                                                            Side,
                                                            CompoundThreshold,
                                                            filteredFeatures
                                                        );


                // Guardar y mostrar reglas
                UpdateCompoundRulesGrid();
                //UpdateRuleSelectionCombo();

                loadingForm.Close();
            }
        }

        private void UpdateCompoundRulesGrid()
        {
            if (CompoundRulesDataframe == null || CompoundRulesDataframe.Rows.Count == 0)
            {
                MessageBox.Show("No se encontraron reglas compuestas que superen el umbral",
                               "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            dgvCompoundRules.SelectionChanged -= DgvCompoundRules_SelectionChanged;

            dgvCompoundRules.DataSource = CompoundRulesDataframe;

            // Configura el modo de ajuste automático a Fill
            dgvCompoundRules.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Asigna los pesos relativos a cada columna (20%, 70%, 10%)
            // Los valores de FillWeight son proporcionales, no porcentajes exactos
            dgvCompoundRules.Columns[0].FillWeight = 20;  // Columna 1: 30%
            dgvCompoundRules.Columns[1].FillWeight = 70;  // Columna 2: 50%
            dgvCompoundRules.Columns[2].FillWeight = 10;  // Columna 3: 20%

            dgvCompoundRules.Refresh();

            dgvCompoundRules.SelectionChanged += DgvCompoundRules_SelectionChanged;

        }

        private void DgvCompoundRules_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvCompoundRules.DataSource != null && dgvCompoundRules.SelectedRows.Count > 0)
            {
                string rule = dgvCompoundRules.SelectedRows[0].Cells[1].Value.ToString();

                PlotRuleReturns(rule);
            }
        }

        //private void UpdateRuleSelectionCombo()
        //{
        //    cmbSelectedRule.Items.Clear();

        //    if (CompoundRulesDataframe != null && CompoundRulesDataframe.Rows.Count > 0)
        //    {
        //        for (int i = 0; i < CompoundRulesDataframe.Rows.Count; i++)
        //        {
        //            double metric = Convert.ToDouble(CompoundRulesDataframe.Rows[i]["metric"]);
        //            string rule = CompoundRulesDataframe.Rows[i]["rule"].ToString();
        //            cmbSelectedRule.Items.Add($"Métrica: {metric:F3} | {rule}");
        //        }

        //        if (cmbSelectedRule.Items.Count > 0)
        //            cmbSelectedRule.SelectedIndex = 0;
        //    }
        //}

        //private void CmbSelectedRule_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (cmbSelectedRule.SelectedIndex >= 0 && CompoundRulesDataframe != null)
        //    {
        //        string rule = CompoundRulesDataframe.Rows[cmbSelectedRule.SelectedIndex]["rule"].ToString();

        //        PlotRuleReturns(rule);
        //    }
        //}

        private void PlotRuleReturns(string rule)
        {
            // Llamar a la función para obtener los datos de la evolución de la regla
            PlotModel plotModelTrain = new Plot_Rule_Returns().DoWork(DataTrainFrame, rule, DateColumn, Side, Plot_Rule_Returns.TRAIN);
            PlotModel plotModelTest = new Plot_Rule_Returns().DoWork(DataTestFrame, rule, DateColumn, Side, Plot_Rule_Returns.TEST);

            if (plotModelTrain == null || plotModelTest == null)
            {
                MessageBox.Show("No hay suficientes datos para mostrar la evolución",
                               "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            foreach (var s in plotModelTest.Series.Cast<LineSeries>())
            {
                LineSeries linear = new LineSeries { Title = s.Title, Color = s.Color };
                linear.Points.AddRange(s.Points);

                plotModelTrain.Series.Add(linear);
            }

            // Actualizar gráfico
            plotRuleReturns.Model = plotModelTrain;
            plotRuleReturns.InvalidatePlot(true);
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
