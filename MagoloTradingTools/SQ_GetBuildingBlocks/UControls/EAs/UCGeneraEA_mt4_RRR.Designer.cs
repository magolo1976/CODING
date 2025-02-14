namespace MTT01_winforms.UControls.EAs
{
    partial class UCGeneraEA_mt4_RRR
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
            richTextBoxFile = new RichTextBox();
            btnGenerate = new Button();
            txtRules = new TextBox();
            btnCargarReglas = new Button();
            lblName = new Label();
            openFileDialog1 = new OpenFileDialog();
            label1 = new Label();
            btnSalvarEA = new Button();
            SuspendLayout();
            // 
            // richTextBoxFile
            // 
            richTextBoxFile.BorderStyle = BorderStyle.None;
            richTextBoxFile.Font = new Font("Consolas", 8.25F);
            richTextBoxFile.Location = new Point(14, 96);
            richTextBoxFile.Name = "richTextBoxFile";
            richTextBoxFile.Size = new Size(1015, 748);
            richTextBoxFile.TabIndex = 0;
            richTextBoxFile.Text = "";
            richTextBoxFile.WordWrap = false;
            // 
            // btnGenerate
            // 
            btnGenerate.BackColor = Color.FromArgb(192, 255, 192);
            btnGenerate.Font = new Font("Consolas", 9F);
            btnGenerate.Location = new Point(954, 63);
            btnGenerate.Name = "btnGenerate";
            btnGenerate.Size = new Size(75, 23);
            btnGenerate.TabIndex = 1;
            btnGenerate.Text = "Generar";
            btnGenerate.UseVisualStyleBackColor = false;
            btnGenerate.Click += btnGenerate_Click;
            // 
            // txtRules
            // 
            txtRules.Font = new Font("Consolas", 9F);
            txtRules.Location = new Point(267, 64);
            txtRules.Name = "txtRules";
            txtRules.Size = new Size(680, 22);
            txtRules.TabIndex = 3;
            txtRules.Text = "1,2";
            // 
            // btnCargarReglas
            // 
            btnCargarReglas.BackColor = Color.FromArgb(192, 255, 192);
            btnCargarReglas.Location = new Point(14, 62);
            btnCargarReglas.Name = "btnCargarReglas";
            btnCargarReglas.Size = new Size(108, 23);
            btnCargarReglas.TabIndex = 5;
            btnCargarReglas.Text = "Cargar Expert";
            btnCargarReglas.UseVisualStyleBackColor = false;
            btnCargarReglas.Click += btnCargarReglas_Click;
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Font = new Font("Consolas", 9F);
            lblName.Location = new Point(135, 66);
            lblName.Name = "lblName";
            lblName.Size = new Size(126, 14);
            lblName.TabIndex = 4;
            lblName.Text = "Números de regla:";
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Consolas", 9F);
            label1.Location = new Point(14, 43);
            label1.Name = "label1";
            label1.Size = new Size(140, 14);
            label1.TabIndex = 6;
            label1.Text = "Genera EA (mt4) RRR";
            // 
            // btnSalvarEA
            // 
            btnSalvarEA.BackColor = Color.FromArgb(192, 255, 192);
            btnSalvarEA.Font = new Font("Consolas", 9F);
            btnSalvarEA.Location = new Point(843, 34);
            btnSalvarEA.Name = "btnSalvarEA";
            btnSalvarEA.Size = new Size(186, 23);
            btnSalvarEA.TabIndex = 7;
            btnSalvarEA.Text = "Salva EA RRR";
            btnSalvarEA.UseVisualStyleBackColor = false;
            btnSalvarEA.Visible = false;
            btnSalvarEA.Click += btnSalvarEA_Click;
            // 
            // UCGeneraEA_mt4_RRR
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(btnSalvarEA);
            Controls.Add(label1);
            Controls.Add(btnCargarReglas);
            Controls.Add(lblName);
            Controls.Add(txtRules);
            Controls.Add(btnGenerate);
            Controls.Add(richTextBoxFile);
            Name = "UCGeneraEA_mt4_RRR";
            Size = new Size(1042, 860);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private RichTextBox richTextBoxFile;
        private Button btnGenerate;
        private ComboBox cmbEAType;
        private TextBox txtRules;
        private Button btnCargarReglas;
        private Label lblName;
        private OpenFileDialog openFileDialog1;
        private Label label1;
        private Button btnSalvarEA;
    }
}
