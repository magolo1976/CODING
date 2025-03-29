using OxyPlot;
using OxyPlot.WindowsForms;
using System.Data;
using System.IO;

namespace MagoloRuleExtraction.Sections.RuleExtraction
{
    partial class RuleExtractionUC
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
            this.SuspendLayout();

            // Crear controles
            Label lblTitle = new Label
            {
                Text = "Extracción de Reglas Compuestas",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                Location = new Point(10, 10),
                Size = new Size(300, 25),
                AutoSize = true
            };

            plotMonkeyDistribution = new PlotView
            {
                Location = new Point(10, 40),
                Size = new Size(780, 200),
                Model = new PlotModel { Title = "Distribución de Monos" }
            };

            Label lblFeatureBase = new Label
            {
                Text = "Selecciona una feature base:",
                Location = new Point(0, 230),
                Size = new Size(200, 20),
                AutoSize = true
            };

            cmbFeatureBase = new ComboBox
            {
                Location = new Point(10, 270),
                Size = new Size(400, 25),
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            lblInfo = new Label
            {
                Location = new Point(0, 285),
                Size = new Size(780, 20),
                ForeColor = Color.Navy,
                BackColor = Color.LightBlue,
                Padding = new Padding(5),
                AutoSize = true
            };

            btnBuscarReglas = new Button
            {
                Text = "Buscar Reglas",
                Location = new Point(10, 335),
                Size = new Size(150, 30)
            };
            btnBuscarReglas.Click += BtnBuscarReglas_Click;

            dgvCompoundRules = new DataGridView
            {
                Location = new Point(10, 375),
                Size = new Size(780, 200),
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };
            dgvCompoundRules.SelectionChanged += DgvCompoundRules_SelectionChanged;

            //Label lblSelectedRule = new Label
            //{
            //    Text = "Selecciona una regla para ver su evolución:",
            //    Location = new Point(0, 565),
            //    Size = new Size(250, 20),
            //    AutoSize = true
            //};

            //cmbSelectedRule = new ComboBox
            //{
            //    Location = new Point(10, 605),
            //    Size = new Size(780, 25),
            //    DropDownStyle = ComboBoxStyle.DropDownList
            //};
            //cmbSelectedRule.SelectedIndexChanged += CmbSelectedRule_SelectedIndexChanged;

            plotRuleReturns = new PlotView
            {
                Location = new Point(10, 570),
                Size = new Size(780, 280),
                Model = new PlotModel { Title = "" } //Evolución de la Regla" }
            };

            // Añadir controles
            this.Controls.Add(lblTitle);
            this.Controls.Add(plotMonkeyDistribution);
            this.Controls.Add(lblFeatureBase);
            this.Controls.Add(cmbFeatureBase);
            this.Controls.Add(lblInfo);
            this.Controls.Add(btnBuscarReglas);
            this.Controls.Add(dgvCompoundRules);
            //this.Controls.Add(lblSelectedRule);
            //this.Controls.Add(cmbSelectedRule);
            this.Controls.Add(plotRuleReturns);
            
            Name = "RuleExtractionUC";
            this.Size = new Size(800, 870);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        // Controles UI
        private ComboBox cmbFeatureBase;
        private Button btnBuscarReglas;
        private DataGridView dgvCompoundRules;
        //private ComboBox cmbSelectedRule;
        private PlotView plotMonkeyDistribution;
        private PlotView plotRuleReturns;
        private Label lblInfo;
        #endregion
    }
}
