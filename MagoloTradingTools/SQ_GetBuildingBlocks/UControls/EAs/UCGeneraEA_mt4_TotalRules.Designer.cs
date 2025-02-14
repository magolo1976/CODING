namespace MTT01_winforms.UControls.EAs
{
    partial class UCGeneraEA_mt4_TotalRules
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
            cmbEAType = new ComboBox();
            txtEAName = new TextBox();
            lblName = new Label();
            btnCargarReglas = new Button();
            folderBrowserDialog1 = new FolderBrowserDialog();
            lblFilesLoaded = new Label();
            label1 = new Label();
            SuspendLayout();
            // 
            // richTextBoxFile
            // 
            richTextBoxFile.BorderStyle = BorderStyle.None;
            richTextBoxFile.Font = new Font("Consolas", 8.25F);
            richTextBoxFile.Location = new Point(14, 81);
            richTextBoxFile.Name = "richTextBoxFile";
            richTextBoxFile.Size = new Size(1005, 760);
            richTextBoxFile.TabIndex = 0;
            richTextBoxFile.Text = "";
            richTextBoxFile.WordWrap = false;
            // 
            // btnGenerate
            // 
            btnGenerate.BackColor = Color.Yellow;
            btnGenerate.Font = new Font("Consolas", 9F);
            btnGenerate.Location = new Point(944, 52);
            btnGenerate.Name = "btnGenerate";
            btnGenerate.Size = new Size(75, 23);
            btnGenerate.TabIndex = 1;
            btnGenerate.Text = "Generar";
            btnGenerate.UseVisualStyleBackColor = false;
            btnGenerate.Click += btnGenerate_Click;
            // 
            // cmbEAType
            // 
            cmbEAType.Font = new Font("Consolas", 9F);
            cmbEAType.FormattingEnabled = true;
            cmbEAType.Items.AddRange(new object[] { "SOLO COMPRA", "SOLO VENTA", "COMPRA & VENTA" });
            cmbEAType.Location = new Point(830, 52);
            cmbEAType.Name = "cmbEAType";
            cmbEAType.Size = new Size(108, 22);
            cmbEAType.TabIndex = 2;
            // 
            // txtEAName
            // 
            txtEAName.Font = new Font("Consolas", 9F);
            txtEAName.Location = new Point(649, 53);
            txtEAName.Name = "txtEAName";
            txtEAName.Size = new Size(175, 22);
            txtEAName.TabIndex = 3;
            txtEAName.Text = "TotalRules_Block";
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Font = new Font("Consolas", 9F);
            lblName.Location = new Point(544, 56);
            lblName.Name = "lblName";
            lblName.Size = new Size(105, 14);
            lblName.TabIndex = 4;
            lblName.Text = "Nombre del EA:";
            // 
            // btnCargarReglas
            // 
            btnCargarReglas.BackColor = Color.Yellow;
            btnCargarReglas.Location = new Point(11, 52);
            btnCargarReglas.Name = "btnCargarReglas";
            btnCargarReglas.Size = new Size(108, 23);
            btnCargarReglas.TabIndex = 5;
            btnCargarReglas.Text = "Cargar Reglas";
            btnCargarReglas.UseVisualStyleBackColor = false;
            btnCargarReglas.Click += btnCargarReglas_Click;
            // 
            // lblFilesLoaded
            // 
            lblFilesLoaded.AutoSize = true;
            lblFilesLoaded.Location = new Point(125, 56);
            lblFilesLoaded.Name = "lblFilesLoaded";
            lblFilesLoaded.Size = new Size(19, 15);
            lblFilesLoaded.TabIndex = 6;
            lblFilesLoaded.Text = "xx";
            lblFilesLoaded.Visible = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Consolas", 9F);
            label1.Location = new Point(14, 32);
            label1.Name = "label1";
            label1.Size = new Size(189, 14);
            label1.TabIndex = 7;
            label1.Text = "Genera EA (mt4) TotalRules";
            // 
            // UCGeneraEA_mt4_TotalRules
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(label1);
            Controls.Add(lblFilesLoaded);
            Controls.Add(btnCargarReglas);
            Controls.Add(lblName);
            Controls.Add(txtEAName);
            Controls.Add(cmbEAType);
            Controls.Add(btnGenerate);
            Controls.Add(richTextBoxFile);
            Name = "UCGeneraEA_mt4_TotalRules";
            Size = new Size(1032, 860);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private RichTextBox richTextBoxFile;
        private Button btnGenerate;
        private ComboBox cmbEAType;
        private TextBox txtEAName;
        private Label lblName;
        private Button btnCargarReglas;
        private FolderBrowserDialog folderBrowserDialog1;
        private Label lblFilesLoaded;
        private Label label1;
    }
}
