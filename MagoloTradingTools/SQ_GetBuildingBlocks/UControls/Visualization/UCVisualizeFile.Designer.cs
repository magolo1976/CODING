namespace MTT01_winforms.UControls.Visualization
{
    partial class UCVisualizeFile
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
            btnSave = new Button();
            SuspendLayout();
            // 
            // richTextBoxFile
            // 
            richTextBoxFile.BorderStyle = BorderStyle.None;
            richTextBoxFile.Font = new Font("Consolas", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            richTextBoxFile.Location = new Point(14, 61);
            richTextBoxFile.Name = "richTextBoxFile";
            richTextBoxFile.Size = new Size(670, 764);
            richTextBoxFile.TabIndex = 0;
            richTextBoxFile.Text = "";
            // 
            // btnSave
            // 
            btnSave.Font = new Font("Consolas", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            btnSave.Location = new Point(13, 32);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(75, 23);
            btnSave.TabIndex = 1;
            btnSave.Text = "Salvar";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // VisualizeFileControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(btnSave);
            Controls.Add(richTextBoxFile);
            Name = "VisualizeFileControl";
            Size = new Size(700, 860);
            Load += VisualizeFileControl_Load;
            ResumeLayout(false);
        }

        #endregion

        private RichTextBox richTextBoxFile;
        private Button btnSave;
    }
}
