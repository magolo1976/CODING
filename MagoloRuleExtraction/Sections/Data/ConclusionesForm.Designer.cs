namespace MagoloRuleExtraction.Sections.Data
{
    partial class ConclusionesForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panel1 = new Panel();
            lblTitle = new Label();
            lblEstabilidad = new Label();
            label1 = new Label();
            lblComportamiento = new Label();
            label3 = new Label();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(lblTitle);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(428, 50);
            panel1.TabIndex = 0;
            // 
            // lblTitle
            // 
            lblTitle.Dock = DockStyle.Fill;
            lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitle.Location = new Point(0, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(428, 50);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Conclusiones";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblEstabilidad
            // 
            lblEstabilidad.AutoSize = true;
            lblEstabilidad.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblEstabilidad.ForeColor = Color.DarkOrange;
            lblEstabilidad.Location = new Point(12, 97);
            lblEstabilidad.Name = "lblEstabilidad";
            lblEstabilidad.Size = new Size(384, 45);
            lblEstabilidad.TabIndex = 4;
            lblEstabilidad.Text = "✅ **Activo Adecuado para Minería de Reglas**\r\nExiste una similitud razonable entre las distribuciones de train y test,\r\npermitiendo la búsqueda de reglas con cierta confianza.";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label1.Location = new Point(12, 73);
            label1.Name = "label1";
            label1.Size = new Size(127, 15);
            label1.TabIndex = 3;
            label1.Text = "Estabilidad del Activo:";
            // 
            // lblComportamiento
            // 
            lblComportamiento.AutoSize = true;
            lblComportamiento.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblComportamiento.ForeColor = Color.FromArgb(0, 192, 0);
            lblComportamiento.Location = new Point(12, 178);
            lblComportamiento.Name = "lblComportamiento";
            lblComportamiento.Size = new Size(384, 45);
            lblComportamiento.TabIndex = 6;
            lblComportamiento.Text = "✅ **Activo Adecuado para Minería de Reglas**\r\nExiste una similitud razonable entre las distribuciones de train y test,\r\npermitiendo la búsqueda de reglas con cierta confianza.";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label3.Location = new Point(12, 153);
            label3.Name = "label3";
            label3.Size = new Size(167, 15);
            label3.TabIndex = 5;
            label3.Text = "Comportamiento Tendencial:";
            // 
            // ConclusionesForm
            // 
            ClientSize = new Size(428, 259);
            Controls.Add(lblComportamiento);
            Controls.Add(label3);
            Controls.Add(lblEstabilidad);
            Controls.Add(label1);
            Controls.Add(panel1);
            Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "ConclusionesForm";
            StartPosition = FormStartPosition.CenterParent;
            panel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        private Panel panel1;
        private Label lblTitle;

        #endregion

        private Label lblEstabilidad;
        private Label label1;
        private Label lblComportamiento;
        private Label label3;
    }
}