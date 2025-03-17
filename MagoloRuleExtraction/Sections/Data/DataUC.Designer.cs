namespace MagoloRuleExtraction.Sections.Data
{
    partial class DataUC
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
            btnStatistic = new Button();
            btnConclusiones = new Button();
            SuspendLayout();
            // 
            // btnStatistic
            // 
            btnStatistic.BackColor = SystemColors.ActiveCaption;
            btnStatistic.Location = new Point(1077, 66);
            btnStatistic.Name = "btnStatistic";
            btnStatistic.Size = new Size(75, 23);
            btnStatistic.TabIndex = 0;
            btnStatistic.Text = "Estadística";
            btnStatistic.UseVisualStyleBackColor = false;
            btnStatistic.Visible = false;
            btnStatistic.Click += btnStatistic_Click;
            // 
            // btnConclusiones
            // 
            btnConclusiones.BackColor = Color.FromArgb(255, 192, 255);
            btnConclusiones.Location = new Point(1158, 66);
            btnConclusiones.Name = "btnConclusiones";
            btnConclusiones.Size = new Size(75, 23);
            btnConclusiones.TabIndex = 1;
            btnConclusiones.Text = "Conclusiones";
            btnConclusiones.UseVisualStyleBackColor = false;
            btnConclusiones.Visible = false;
            btnConclusiones.Click += btnConclusiones_Click;
            // 
            // DataUC
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(btnConclusiones);
            Controls.Add(btnStatistic);
            Name = "DataUC";
            Size = new Size(1246, 770);
            ResumeLayout(false);
        }

        #endregion

        private Button btnStatistic;
        private Button btnConclusiones;
    }
}
