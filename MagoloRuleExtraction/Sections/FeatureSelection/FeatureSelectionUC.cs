using MagoloRuleExtraction.Classes;
using System.ComponentModel;
using System.Data;

namespace MagoloRuleExtraction.Sections.FeatureSelection
{
    public partial class FeatureSelectionUC: UserControl
    {
        // Estado de la sesión (equivalente a st.session_state)
        public DataTable TrainData = null;
        public string DateColumn = string.Empty;
        public DataTable FeaturesDataTable;
        public string SideSelected { get { return cmbSide.SelectedItem.ToString(); } }
        public Dictionary<string, Dictionary<string, object>> PrimaryRules;

        //private bool[] _maskTrain; // Máscara para filtrar datos de entrenamiento

        // Componentes del análisis
        private readonly Analyze_All_Features _analysisManager;

        public FeatureSelectionUC()
        {
            InitializeComponent();

            cmbSide.SelectedIndex = 0;

            _analysisManager = new Analyze_All_Features();
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
                double correlationThreshold = this.trkCorrelation.Value / 100.0;

                //// Filtrar datos para entrenamiento
                //DataTable trainData = FilterTrainData();

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
                        correlationThreshold
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
                        this.lblMessage.Text = $"Features seleccionadas ({FeaturesDataTable.Rows.Count}) después de aplicar el filtro de correlación (umbral: {correlationThreshold})";
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

        ///// <summary>
        ///// Filtra los datos para obtener solo los de entrenamiento según la máscara
        ///// </summary>
        //private DataTable FilterTrainData()
        //{
        //    if (_dataFrame == null || _maskTrain == null)
        //        return null;

        //    DataTable result = _dataFrame.Clone();

        //    for (int i = 0; i < _maskTrain.Length; i++)
        //    {
        //        if (_maskTrain[i])
        //        {
        //            result.ImportRow(_dataFrame.Rows[i]);
        //        }
        //    }

        //    return result;
        //}

        /// <summary>
        /// Establece los datos para el análisis
        /// </summary>
        public void SetData(DataTable dataFrame, string dateColumn)//, bool[] maskTrain)
        {
            TrainData = dataFrame;
            DateColumn = dateColumn;
            //_maskTrain = maskTrain;

            // Restablecer mensaje
            this.lblMessage.Text = "Datos cargados correctamente. Listo para analizar.";
            this.lblMessage.ForeColor = Color.Green;
        }

    }
}
