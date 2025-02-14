namespace MTT01_winforms.UControls.StrategyQuant
{
    partial class UCSqxBblockAnalizer
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            txtFolder = new TextBox();
            btnGetFolder = new Button();
            folderBrowserDialog = new FolderBrowserDialog();
            btnAnalizar = new Button();
            label1 = new Label();
            label2 = new Label();
            listBoxLongEntry = new ListBox();
            listBoxShortEntry = new ListBox();
            btnDeleteLongs = new Button();
            btnSaveLongs = new Button();
            btnSaveShorts = new Button();
            btnDeleteShorts = new Button();
            saveFileDialog = new SaveFileDialog();
            btnLongContiene = new Button();
            txtLongContiene = new TextBox();
            txtShortContiene = new TextBox();
            btnShortContiene = new Button();
            label3 = new Label();
            label4 = new Label();
            btnSaveAll = new Button();
            txtContarBblocks = new TextBox();
            btnSaveUniqueFile = new Button();
            SuspendLayout();
            // 
            // txtFolder
            // 
            txtFolder.BorderStyle = BorderStyle.FixedSingle;
            txtFolder.Font = new Font("Consolas", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            txtFolder.ForeColor = Color.DodgerBlue;
            txtFolder.Location = new Point(111, 37);
            txtFolder.Margin = new Padding(4, 5, 4, 5);
            txtFolder.Name = "txtFolder";
            txtFolder.Size = new Size(622, 20);
            txtFolder.TabIndex = 0;
            // 
            // btnGetFolder
            // 
            btnGetFolder.FlatStyle = FlatStyle.Popup;
            btnGetFolder.Font = new Font("Consolas", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            btnGetFolder.ForeColor = Color.DodgerBlue;
            btnGetFolder.Location = new Point(13, 36);
            btnGetFolder.Margin = new Padding(4, 5, 4, 5);
            btnGetFolder.Name = "btnGetFolder";
            btnGetFolder.Size = new Size(89, 21);
            btnGetFolder.TabIndex = 1;
            btnGetFolder.Text = "Buscar";
            btnGetFolder.UseVisualStyleBackColor = true;
            btnGetFolder.Click += btnGetFolder_Click;
            // 
            // btnAnalizar
            // 
            btnAnalizar.FlatStyle = FlatStyle.Popup;
            btnAnalizar.Font = new Font("Consolas", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            btnAnalizar.ForeColor = Color.DodgerBlue;
            btnAnalizar.Location = new Point(741, 36);
            btnAnalizar.Margin = new Padding(4, 5, 4, 5);
            btnAnalizar.Name = "btnAnalizar";
            btnAnalizar.Size = new Size(144, 21);
            btnAnalizar.TabIndex = 2;
            btnAnalizar.Text = "Analizar";
            btnAnalizar.UseVisualStyleBackColor = true;
            btnAnalizar.Click += btnAnalizar_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Consolas", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            label1.ForeColor = Color.LightSeaGreen;
            label1.Location = new Point(11, 100);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(97, 13);
            label1.TabIndex = 6;
            label1.Text = "LongEntrySignal";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Consolas", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            label2.ForeColor = Color.DeepPink;
            label2.Location = new Point(451, 100);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(103, 13);
            label2.TabIndex = 7;
            label2.Text = "ShortEntrySignal";
            // 
            // listBoxLongEntry
            // 
            listBoxLongEntry.Font = new Font("Consolas", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            listBoxLongEntry.ForeColor = Color.LightSeaGreen;
            listBoxLongEntry.FormattingEnabled = true;
            listBoxLongEntry.HorizontalScrollbar = true;
            listBoxLongEntry.Location = new Point(15, 122);
            listBoxLongEntry.Margin = new Padding(4, 5, 4, 5);
            listBoxLongEntry.Name = "listBoxLongEntry";
            listBoxLongEntry.SelectionMode = SelectionMode.MultiSimple;
            listBoxLongEntry.Size = new Size(432, 420);
            listBoxLongEntry.TabIndex = 8;
            // 
            // listBoxShortEntry
            // 
            listBoxShortEntry.Font = new Font("Consolas", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            listBoxShortEntry.ForeColor = Color.DeepPink;
            listBoxShortEntry.FormattingEnabled = true;
            listBoxShortEntry.HorizontalScrollbar = true;
            listBoxShortEntry.Location = new Point(453, 122);
            listBoxShortEntry.Margin = new Padding(4, 5, 4, 5);
            listBoxShortEntry.Name = "listBoxShortEntry";
            listBoxShortEntry.SelectionMode = SelectionMode.MultiSimple;
            listBoxShortEntry.Size = new Size(432, 420);
            listBoxShortEntry.TabIndex = 9;
            // 
            // btnDeleteLongs
            // 
            btnDeleteLongs.FlatStyle = FlatStyle.Popup;
            btnDeleteLongs.Font = new Font("Consolas", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            btnDeleteLongs.ForeColor = Color.LightSeaGreen;
            btnDeleteLongs.Location = new Point(16, 600);
            btnDeleteLongs.Margin = new Padding(4, 5, 4, 5);
            btnDeleteLongs.Name = "btnDeleteLongs";
            btnDeleteLongs.Size = new Size(153, 21);
            btnDeleteLongs.TabIndex = 10;
            btnDeleteLongs.Text = "Borrar Seleccionados";
            btnDeleteLongs.UseVisualStyleBackColor = true;
            btnDeleteLongs.Click += btnDeleteLongs_Click;
            // 
            // btnSaveLongs
            // 
            btnSaveLongs.FlatStyle = FlatStyle.Popup;
            btnSaveLongs.Font = new Font("Consolas", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            btnSaveLongs.ForeColor = Color.LightSeaGreen;
            btnSaveLongs.Location = new Point(175, 600);
            btnSaveLongs.Margin = new Padding(4, 5, 4, 5);
            btnSaveLongs.Name = "btnSaveLongs";
            btnSaveLongs.Size = new Size(75, 21);
            btnSaveLongs.TabIndex = 11;
            btnSaveLongs.Text = "Salvar a txt";
            btnSaveLongs.UseVisualStyleBackColor = true;
            btnSaveLongs.Click += btnSaveLongs_Click;
            // 
            // btnSaveShorts
            // 
            btnSaveShorts.FlatStyle = FlatStyle.Popup;
            btnSaveShorts.Font = new Font("Consolas", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            btnSaveShorts.ForeColor = Color.DeepPink;
            btnSaveShorts.Location = new Point(613, 600);
            btnSaveShorts.Margin = new Padding(4, 5, 4, 5);
            btnSaveShorts.Name = "btnSaveShorts";
            btnSaveShorts.Size = new Size(75, 21);
            btnSaveShorts.TabIndex = 13;
            btnSaveShorts.Text = "Salvar a txt";
            btnSaveShorts.UseVisualStyleBackColor = true;
            btnSaveShorts.Click += btnSaveShorts_Click;
            // 
            // btnDeleteShorts
            // 
            btnDeleteShorts.FlatStyle = FlatStyle.Popup;
            btnDeleteShorts.Font = new Font("Consolas", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            btnDeleteShorts.ForeColor = Color.DeepPink;
            btnDeleteShorts.Location = new Point(454, 600);
            btnDeleteShorts.Margin = new Padding(4, 5, 4, 5);
            btnDeleteShorts.Name = "btnDeleteShorts";
            btnDeleteShorts.Size = new Size(153, 21);
            btnDeleteShorts.TabIndex = 12;
            btnDeleteShorts.Text = "Borrar Seleccionados";
            btnDeleteShorts.UseVisualStyleBackColor = true;
            btnDeleteShorts.Click += btnDeleteShorts_Click;
            // 
            // btnLongContiene
            // 
            btnLongContiene.FlatStyle = FlatStyle.Popup;
            btnLongContiene.Font = new Font("Consolas", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            btnLongContiene.ForeColor = Color.LightSeaGreen;
            btnLongContiene.Location = new Point(15, 562);
            btnLongContiene.Margin = new Padding(4, 5, 4, 5);
            btnLongContiene.Name = "btnLongContiene";
            btnLongContiene.Size = new Size(114, 21);
            btnLongContiene.TabIndex = 14;
            btnLongContiene.Text = "Solo contiene..";
            btnLongContiene.UseVisualStyleBackColor = true;
            btnLongContiene.Click += btnLongContiene_Click;
            // 
            // txtLongContiene
            // 
            txtLongContiene.BorderStyle = BorderStyle.FixedSingle;
            txtLongContiene.Font = new Font("Consolas", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            txtLongContiene.ForeColor = Color.LightSeaGreen;
            txtLongContiene.Location = new Point(135, 562);
            txtLongContiene.Margin = new Padding(4, 5, 4, 5);
            txtLongContiene.Name = "txtLongContiene";
            txtLongContiene.Size = new Size(312, 20);
            txtLongContiene.TabIndex = 15;
            // 
            // txtShortContiene
            // 
            txtShortContiene.BorderStyle = BorderStyle.FixedSingle;
            txtShortContiene.Font = new Font("Consolas", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            txtShortContiene.ForeColor = Color.DeepPink;
            txtShortContiene.Location = new Point(567, 562);
            txtShortContiene.Margin = new Padding(4, 5, 4, 5);
            txtShortContiene.Name = "txtShortContiene";
            txtShortContiene.Size = new Size(319, 20);
            txtShortContiene.TabIndex = 17;
            // 
            // btnShortContiene
            // 
            btnShortContiene.FlatStyle = FlatStyle.Popup;
            btnShortContiene.Font = new Font("Consolas", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            btnShortContiene.ForeColor = Color.DeepPink;
            btnShortContiene.Location = new Point(453, 562);
            btnShortContiene.Margin = new Padding(4, 5, 4, 5);
            btnShortContiene.Name = "btnShortContiene";
            btnShortContiene.Size = new Size(107, 21);
            btnShortContiene.TabIndex = 16;
            btnShortContiene.Text = "Solo contiene..";
            btnShortContiene.UseVisualStyleBackColor = true;
            btnShortContiene.Click += btnShortContiene_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Consolas", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            label3.ForeColor = Color.LightSeaGreen;
            label3.Location = new Point(135, 584);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(127, 13);
            label3.TabIndex = 18;
            label3.Text = "* Separados con un |";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Consolas", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            label4.ForeColor = Color.DeepPink;
            label4.Location = new Point(567, 584);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(127, 13);
            label4.TabIndex = 19;
            label4.Text = "* Separados con un |";
            // 
            // btnSaveAll
            // 
            btnSaveAll.FlatStyle = FlatStyle.Popup;
            btnSaveAll.Font = new Font("Consolas", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            btnSaveAll.ForeColor = Color.DodgerBlue;
            btnSaveAll.Location = new Point(743, 69);
            btnSaveAll.Margin = new Padding(4, 5, 4, 5);
            btnSaveAll.Name = "btnSaveAll";
            btnSaveAll.Size = new Size(143, 21);
            btnSaveAll.TabIndex = 26;
            btnSaveAll.Text = "Salvar conjunto";
            btnSaveAll.UseVisualStyleBackColor = true;
            btnSaveAll.Click += btnSaveAll_Click;
            // 
            // txtContarBblocks
            // 
            txtContarBblocks.BorderStyle = BorderStyle.FixedSingle;
            txtContarBblocks.Font = new Font("Consolas", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            txtContarBblocks.ForeColor = Color.LightSeaGreen;
            txtContarBblocks.Location = new Point(379, 68);
            txtContarBblocks.Margin = new Padding(4, 5, 4, 5);
            txtContarBblocks.Name = "txtContarBblocks";
            txtContarBblocks.Size = new Size(354, 20);
            txtContarBblocks.TabIndex = 21;
            // 
            // btnSaveUniqueFile
            // 
            btnSaveUniqueFile.FlatStyle = FlatStyle.Popup;
            btnSaveUniqueFile.Font = new Font("Consolas", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            btnSaveUniqueFile.ForeColor = Color.DodgerBlue;
            btnSaveUniqueFile.Location = new Point(13, 69);
            btnSaveUniqueFile.Margin = new Padding(4, 5, 4, 5);
            btnSaveUniqueFile.Name = "btnSaveUniqueFile";
            btnSaveUniqueFile.Size = new Size(143, 21);
            btnSaveUniqueFile.TabIndex = 27;
            btnSaveUniqueFile.Text = "Crear fichero único";
            btnSaveUniqueFile.UseVisualStyleBackColor = true;
            btnSaveUniqueFile.Click += btnSaveUniqueFile_Click;
            // 
            // UCSqxBblockAnalizer
            // 
            AutoScaleDimensions = new SizeF(7F, 14F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            Controls.Add(btnSaveUniqueFile);
            Controls.Add(btnSaveAll);
            Controls.Add(txtContarBblocks);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(txtShortContiene);
            Controls.Add(btnShortContiene);
            Controls.Add(txtLongContiene);
            Controls.Add(btnLongContiene);
            Controls.Add(btnSaveShorts);
            Controls.Add(btnDeleteShorts);
            Controls.Add(btnSaveLongs);
            Controls.Add(btnDeleteLongs);
            Controls.Add(listBoxShortEntry);
            Controls.Add(listBoxLongEntry);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnAnalizar);
            Controls.Add(btnGetFolder);
            Controls.Add(txtFolder);
            Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point);
            Margin = new Padding(4, 5, 4, 5);
            Name = "UCSqxBblockAnalizer";
            Size = new Size(900, 653);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtFolder;
        private Button btnGetFolder;
        private FolderBrowserDialog folderBrowserDialog;
        private Button btnAnalizar;
        private Label label1;
        private Label label2;
        private ListBox listBoxLongEntry;
        private ListBox listBoxShortEntry;
        private Button btnDeleteLongs;
        private Button btnSaveLongs;
        private Button btnSaveShorts;
        private Button btnDeleteShorts;
        private SaveFileDialog saveFileDialog;
        private Button btnLongContiene;
        private TextBox txtLongContiene;
        private TextBox txtShortContiene;
        private Button btnShortContiene;
        private Label label3;
        private Label label4;
        private Button btnSaveAll;
        private TextBox txtContarBblocks;
        private Button btnSaveUniqueFile;
    }
}