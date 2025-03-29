using MagoloRuleExtraction.Classes;
using System.ComponentModel;
using System.Data;

namespace MagoloRuleExtraction.Sections.FeatureSelection
{
    public partial class FeatureSelectionUC: UserControl
    {
        // Estado de la sesión (equivalente a st.session_state)
        public DataTable FeaturesDataTable;
        public string SideSelected { get { return cmbSide.SelectedItem.ToString(); } }
        public double CorrelationThreshold = 75;
        public Dictionary<string, Dictionary<string, object>> PrimaryRules;

        // Componentes del análisis
        private readonly Analyze_All_Features _analysisManager;
        private DataTable TrainData = null;
        private string DateColumn = string.Empty;

        public FeatureSelectionUC()
        {
            InitializeComponent();

            cmbSide.SelectedIndex = 0;

            _analysisManager = new Analyze_All_Features();
        }

        /// <summary>
        /// Establece los datos para el análisis
        /// </summary>
        public void Initialize(DataTable dataFrame, string dateColumn)
        {
            TrainData = dataFrame;
            DateColumn = dateColumn;

            // Restablecer mensaje
            if (TrainData != null && TrainData.Rows.Count > 0)
            {
                this.lblMessage.Text = "Datos cargados correctamente. Listo para analizar.";
                this.lblMessage.ForeColor = Color.Green;
            }
            else
            {
                this.lblMessage.Text = "Datos faltantes. Cargue datos previamente para analizar.";
                this.lblMessage.ForeColor = Color.Red;
            }
        }

        /// <summary>
        /// Actualiza el valor mostrado del trackbar de correlación
        /// </summary>
        private void TrkCorrelation_ValueChanged(object sender, EventArgs e)
        {
            this.lblCorrelationValue.Text = (this.trkCorrelation.Value / 100.0).ToString("0.00");
        }

        /// <summary>
        /// Maneja el evento de clic en el botón Analizar
        /// </summary>
        private void BtnAnalyze_Click(object sender, EventArgs e)
        {
            // Verificar si hay datos cargados
            if (TrainData == null)
            {
                this.lblMessage.Text = "Por favor, carga un archivo CSV primero.";
                this.lblMessage.ForeColor = Color.Red;
                return;
            }

            try
            {
                // Mostrar indicador de carga
                this.btnAnalyze.Enabled = false;
                this.progressBar.Visible = true;
                this.lblMessage.Text = "Analizando features...";
                this.lblMessage.ForeColor = Color.Black;

                // Actualizar UI
                Application.DoEvents();

                // Obtener parámetros
                string side = SideSelected;
                CorrelationThreshold = Math.Round(this.trkCorrelation.Value / 100.0, 3);

                // Iniciar análisis en un hilo separado para no bloquear la UI
                BackgroundWorker worker = new BackgroundWorker();
                worker.DoWork += (s, args) =>
                {
                    // Analizar todas las features
                    var analysisResult = _analysisManager.DoWork(
                        TrainData,
                        "Target",
                        DateColumn,
                        side,
                        CorrelationThreshold
                    );

                    args.Result = analysisResult;
                };

                worker.RunWorkerCompleted += (s, args) =>
                {
                    // Ocultar indicador de carga
                    this.progressBar.Visible = false;
                    this.btnAnalyze.Enabled = true;

                    if (args.Error != null)
                    {
                        // Mostrar error
                        this.lblMessage.Text = "Error al analizar features: " + args.Error.Message;
                        this.lblMessage.ForeColor = Color.Red;
                        return;
                    }

                    // Obtener resultados
                    var result = (Tuple<DataTable, Dictionary<string, Dictionary<string, object>>>)args.Result;
                    FeaturesDataTable = result.Item1;

                    // Guardar reglas en el estado de la sesión
                    PrimaryRules = result.Item2;

                    // Mostrar resultados
                    if (FeaturesDataTable.Rows.Count > 0)
                    {
                        this.lblMessage.Text = $"Features seleccionadas ({FeaturesDataTable.Rows.Count}) después de aplicar el filtro de correlación (umbral: {CorrelationThreshold})";
                        this.lblMessage.ForeColor = Color.Black;
                        this.dgvResults.DataSource = FeaturesDataTable;
                    }
                    else
                    {
                        this.lblMessage.Text = "No se encontraron features significativas";
                        this.lblMessage.ForeColor = Color.Orange;
                        this.dgvResults.DataSource = null;
                    }
                };

                worker.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                // Ocultar indicador de carga y mostrar error
                this.progressBar.Visible = false;
                this.btnAnalyze.Enabled = true;
                this.lblMessage.Text = "Error: " + ex.Message;
                this.lblMessage.ForeColor = Color.Red;
            }
        }

    }
}
