namespace MTT01_winforms.UControls.Visualization
{
    partial class UCCompareImages
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
            btnsearchFolder = new Button();
            txtUrlFolder = new TextBox();
            txtUrlIcon = new TextBox();
            openFileDialog1 = new OpenFileDialog();
            folderBrowserDialog1 = new FolderBrowserDialog();
            btnSearchIcon = new Button();
            button3 = new Button();
            label1 = new Label();
            label2 = new Label();
            btnDownloadImages = new Button();
            btnDownloadImagesTexto = new Button();
            SuspendLayout();
            // 
            // btnsearchFolder
            // 
            btnsearchFolder.Location = new Point(28, 82);
            btnsearchFolder.Name = "btnsearchFolder";
            btnsearchFolder.Size = new Size(75, 23);
            btnsearchFolder.TabIndex = 0;
            btnsearchFolder.Text = "buscar";
            btnsearchFolder.UseVisualStyleBackColor = true;
            btnsearchFolder.Click += btnsearchFolder_Click;
            // 
            // txtUrlFolder
            // 
            txtUrlFolder.Font = new Font("Segoe UI", 8F);
            txtUrlFolder.Location = new Point(28, 57);
            txtUrlFolder.Name = "txtUrlFolder";
            txtUrlFolder.Size = new Size(657, 22);
            txtUrlFolder.TabIndex = 1;
            // 
            // txtUrlIcon
            // 
            txtUrlIcon.Font = new Font("Segoe UI", 8F);
            txtUrlIcon.Location = new Point(28, 146);
            txtUrlIcon.Name = "txtUrlIcon";
            txtUrlIcon.Size = new Size(657, 22);
            txtUrlIcon.TabIndex = 2;
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // btnSearchIcon
            // 
            btnSearchIcon.Location = new Point(28, 175);
            btnSearchIcon.Name = "btnSearchIcon";
            btnSearchIcon.Size = new Size(75, 23);
            btnSearchIcon.TabIndex = 3;
            btnSearchIcon.Text = "buscar";
            btnSearchIcon.UseVisualStyleBackColor = true;
            btnSearchIcon.Click += btnSearchIcon_Click;
            // 
            // button3
            // 
            button3.Location = new Point(551, 246);
            button3.Name = "button3";
            button3.Size = new Size(134, 23);
            button3.TabIndex = 4;
            button3.Text = "Compara";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(28, 39);
            label1.Name = "label1";
            label1.Size = new Size(102, 15);
            label1.TabIndex = 5;
            label1.Text = "Carpeta de iconos";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(28, 128);
            label2.Name = "label2";
            label2.Size = new Size(100, 15);
            label2.TabIndex = 6;
            label2.Text = "Icono a comparar";
            // 
            // btnDownloadImages
            // 
            btnDownloadImages.BackColor = Color.FromArgb(192, 255, 192);
            btnDownloadImages.ForeColor = Color.Black;
            btnDownloadImages.Location = new Point(28, 338);
            btnDownloadImages.Name = "btnDownloadImages";
            btnDownloadImages.Size = new Size(170, 23);
            btnDownloadImages.TabIndex = 7;
            btnDownloadImages.Text = "Descargar imágenes";
            btnDownloadImages.UseVisualStyleBackColor = false;
            btnDownloadImages.Click += btnDownloadImages_Click;
            // 
            // btnDownloadImagesTexto
            // 
            btnDownloadImagesTexto.BackColor = Color.FromArgb(128, 255, 255);
            btnDownloadImagesTexto.ForeColor = Color.Black;
            btnDownloadImagesTexto.Location = new Point(217, 338);
            btnDownloadImagesTexto.Name = "btnDownloadImagesTexto";
            btnDownloadImagesTexto.Size = new Size(192, 23);
            btnDownloadImagesTexto.TabIndex = 8;
            btnDownloadImagesTexto.Text = "Descargar img y texto";
            btnDownloadImagesTexto.UseVisualStyleBackColor = false;
            btnDownloadImagesTexto.Click += btnDownloadImagesTexto_Click;
            // 
            // UCCompareImages
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(btnDownloadImagesTexto);
            Controls.Add(btnDownloadImages);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(button3);
            Controls.Add(btnSearchIcon);
            Controls.Add(txtUrlIcon);
            Controls.Add(txtUrlFolder);
            Controls.Add(btnsearchFolder);
            Name = "UCCompareImages";
            Size = new Size(705, 381);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnsearchFolder;
        private TextBox txtUrlFolder;
        private TextBox txtUrlIcon;
        private OpenFileDialog openFileDialog1;
        private FolderBrowserDialog folderBrowserDialog1;
        private Button btnSearchIcon;
        private Button button3;
        private Label label1;
        private Label label2;
        private Button btnDownloadImages;
        private Button btnDownloadImagesTexto;
    }
}
