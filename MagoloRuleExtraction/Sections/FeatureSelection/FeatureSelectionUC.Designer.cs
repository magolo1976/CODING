namespace MagoloRuleExtraction.Sections.FeatureSelection
{
    partial class FeatureSelectionUC
    {
        /// <summary> 
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            panel = new Panel();
            lblTitle = new Label();
            lblCorrelationValue = new Label();
            btnAnalyze = new Button();
            progressBar = new ProgressBar();
            lblMessage = new Label();
            dgvResults = new DataGridView();
            optionsPanel = new TableLayoutPanel();
            lblSide = new Label();
            cmbSide = new ComboBox();
            lblCorrelation = new Label();
            trkCorrelation = new TrackBar();
            toolTip = new ToolTip(components);
            panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvResults).BeginInit();
            optionsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trkCorrelation).BeginInit();
            SuspendLayout();
            // 
            // panel
            // 
            panel.Controls.Add(progressBar);
            panel.Controls.Add(lblTitle);
            panel.Controls.Add(lblCorrelationValue);
            panel.Controls.Add(btnAnalyze);
            panel.Controls.Add(lblMessage);
            panel.Controls.Add(dgvResults);
            panel.Controls.Add(optionsPanel);
            panel.Location = new Point(0, -17);
            panel.Name = "panel";
            panel.Size = new Size(819, 553);
            panel.TabIndex = 0;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitle.Location = new Point(19, 51);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(202, 25);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Selección de Features";
            // 
            // lblCorrelationValue
            // 
            lblCorrelationValue.AutoSize = true;
            lblCorrelationValue.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblCorrelationValue.Location = new Point(755, 106);
            lblCorrelationValue.Name = "lblCorrelationValue";
            lblCorrelationValue.Size = new Size(41, 21);
            lblCorrelationValue.TabIndex = 2;
            lblCorrelationValue.Text = "0.95";
            // 
            // btnAnalyze
            // 
            btnAnalyze.BackColor = Color.LightSkyBlue;
            btnAnalyze.Location = new Point(19, 141);
            btnAnalyze.Name = "btnAnalyze";
            btnAnalyze.Size = new Size(150, 30);
            btnAnalyze.TabIndex = 3;
            btnAnalyze.Text = "Analizar Features";
            btnAnalyze.UseVisualStyleBackColor = false;
            btnAnalyze.Click += BtnAnalyze_Click;
            // 
            // progressBar
            // 
            progressBar.Location = new Point(201, 111);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(200, 10);
            progressBar.Style = ProgressBarStyle.Marquee;
            progressBar.TabIndex = 4;
            progressBar.Visible = false;
            // 
            // lblMessage
            // 
            lblMessage.AutoSize = true;
            lblMessage.Location = new Point(350, 146);
            lblMessage.Name = "lblMessage";
            lblMessage.Size = new Size(0, 15);
            lblMessage.TabIndex = 5;
            // 
            // dgvResults
            // 
            dgvResults.AllowUserToAddRows = false;
            dgvResults.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvResults.Location = new Point(19, 181);
            dgvResults.Name = "dgvResults";
            dgvResults.ReadOnly = true;
            dgvResults.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvResults.Size = new Size(780, 350);
            dgvResults.TabIndex = 6;
            // 
            // optionsPanel
            // 
            optionsPanel.ColumnCount = 2;
            optionsPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            optionsPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            optionsPanel.Controls.Add(lblSide, 0, 0);
            optionsPanel.Controls.Add(cmbSide, 0, 1);
            optionsPanel.Controls.Add(lblCorrelation, 1, 0);
            optionsPanel.Controls.Add(trkCorrelation, 1, 1);
            optionsPanel.Location = new Point(19, 81);
            optionsPanel.Name = "optionsPanel";
            optionsPanel.RowCount = 2;
            optionsPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            optionsPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            optionsPanel.Size = new Size(780, 50);
            optionsPanel.TabIndex = 1;
            // 
            // lblSide
            // 
            lblSide.AutoSize = true;
            lblSide.Location = new Point(3, 0);
            lblSide.Name = "lblSide";
            lblSide.Size = new Size(91, 15);
            lblSide.TabIndex = 0;
            lblSide.Text = "Selecciona Side:";
            // 
            // cmbSide
            // 
            cmbSide.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbSide.Items.AddRange(new object[] { "Long", "Short" });
            cmbSide.Location = new Point(3, 23);
            cmbSide.Name = "cmbSide";
            cmbSide.Size = new Size(150, 23);
            cmbSide.TabIndex = 1;
            toolTip.SetToolTip(cmbSide, "Dirección de la estrategia");
            // 
            // lblCorrelation
            // 
            lblCorrelation.AutoSize = true;
            lblCorrelation.Location = new Point(393, 0);
            lblCorrelation.Name = "lblCorrelation";
            lblCorrelation.Size = new Size(129, 15);
            lblCorrelation.TabIndex = 2;
            lblCorrelation.Text = "Umbral de Correlación:";
            // 
            // trkCorrelation
            // 
            trkCorrelation.Location = new Point(393, 23);
            trkCorrelation.Maximum = 100;
            trkCorrelation.Minimum = 75;
            trkCorrelation.Name = "trkCorrelation";
            trkCorrelation.Size = new Size(337, 24);
            trkCorrelation.TabIndex = 3;
            toolTip.SetToolTip(trkCorrelation, "Umbral máximo de correlación permitido entre features. Un valor más bajo resulta en menos features pero más independientes entre sí.");
            trkCorrelation.Value = 95;
            trkCorrelation.ValueChanged += TrkCorrelation_ValueChanged;
            // 
            // FeatureSelectionUC
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(panel);
            MinimumSize = new Size(800, 500);
            Name = "FeatureSelectionUC";
            Size = new Size(822, 556);
            panel.ResumeLayout(false);
            panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvResults).EndInit();
            optionsPanel.ResumeLayout(false);
            optionsPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trkCorrelation).EndInit();
            ResumeLayout(false);
        }

        // Campos para los controles
        private Panel panel;
        private Label lblTitle;
        private TableLayoutPanel optionsPanel;
        private Label lblSide;
        private ComboBox cmbSide;
        private Label lblCorrelation;
        private TrackBar trkCorrelation;
        private Label lblCorrelationValue;
        private Button btnAnalyze;
        private ProgressBar progressBar;
        private Label lblMessage;
        private DataGridView dgvResults;
        private ToolTip toolTip;

        #endregion
    }
}
