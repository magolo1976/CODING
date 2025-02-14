namespace MTT01_winforms
{
    partial class MTT_Main
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
            menuStrip1 = new MenuStrip();
            dataToolStripMenuItem = new ToolStripMenuItem();
            loadCSVToolStripMenuItem = new ToolStripMenuItem();
            strategyQuantToolStripMenuItem = new ToolStripMenuItem();
            buildingBlocksToolStripMenuItem = new ToolStripMenuItem();
            visualizationToolStripMenuItem = new ToolStripMenuItem();
            viewFIleToolStripMenuItem = new ToolStripMenuItem();
            viewFileGroupsToolStripMenuItem = new ToolStripMenuItem();
            compararImágenesToolStripMenuItem = new ToolStripMenuItem();
            wekaToolStripMenuItem = new ToolStripMenuItem();
            extractorDeReglasToolStripMenuItem = new ToolStripMenuItem();
            reglaMT4ToolStripMenuItem = new ToolStripMenuItem();
            unEAConReglasToolStripMenuItem = new ToolStripMenuItem();
            eAProbadorDeReglasToolStripMenuItem = new ToolStripMenuItem();
            eAReglasEspecificasToolStripMenuItem = new ToolStripMenuItem();
            cálculoToolStripMenuItem = new ToolStripMenuItem();
            distribuciónNormalToolStripMenuItem = new ToolStripMenuItem();
            kolmogorovSmirnovToolStripMenuItem = new ToolStripMenuItem();
            tradingDeParesToolStripMenuItem = new ToolStripMenuItem();
            webScrappingToolStripMenuItem = new ToolStripMenuItem();
            alphavantageToolStripMenuItem = new ToolStripMenuItem();
            neuronasToolStripMenuItem = new ToolStripMenuItem();
            entrenamientoToolStripMenuItem = new ToolStripMenuItem();
            ruleExtractionToolStripMenuItem = new ToolStripMenuItem();
            cargaDeFicheroToolStripMenuItem = new ToolStripMenuItem();
            openFileDialog1 = new OpenFileDialog();
            folderBrowserDialog1 = new FolderBrowserDialog();
            btnOllamaConnect = new Button();
            txtOllamaAnswer = new TextBox();
            txtOpenAIAnswer = new TextBox();
            txtOllamaQuestion = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            txtOpenAIQuestion = new TextBox();
            btnOpenAIConnect = new Button();
            label5 = new Label();
            cmbModels = new ComboBox();
            label6 = new Label();
            txtOpenAIPrompt = new TextBox();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.BackColor = Color.HotPink;
            menuStrip1.Font = new Font("Consolas", 9F);
            menuStrip1.Items.AddRange(new ToolStripItem[] { dataToolStripMenuItem, strategyQuantToolStripMenuItem, visualizationToolStripMenuItem, wekaToolStripMenuItem, cálculoToolStripMenuItem, webScrappingToolStripMenuItem, neuronasToolStripMenuItem, ruleExtractionToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1128, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // dataToolStripMenuItem
            // 
            dataToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { loadCSVToolStripMenuItem });
            dataToolStripMenuItem.Name = "dataToolStripMenuItem";
            dataToolStripMenuItem.Size = new Size(47, 20);
            dataToolStripMenuItem.Text = "Data";
            // 
            // loadCSVToolStripMenuItem
            // 
            loadCSVToolStripMenuItem.Name = "loadCSVToolStripMenuItem";
            loadCSVToolStripMenuItem.Size = new Size(130, 22);
            loadCSVToolStripMenuItem.Text = "Load CSV";
            loadCSVToolStripMenuItem.Click += loadCSVToolStripMenuItem_Click;
            // 
            // strategyQuantToolStripMenuItem
            // 
            strategyQuantToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { buildingBlocksToolStripMenuItem });
            strategyQuantToolStripMenuItem.Name = "strategyQuantToolStripMenuItem";
            strategyQuantToolStripMenuItem.Size = new Size(117, 20);
            strategyQuantToolStripMenuItem.Text = "Strategy Quant";
            // 
            // buildingBlocksToolStripMenuItem
            // 
            buildingBlocksToolStripMenuItem.Name = "buildingBlocksToolStripMenuItem";
            buildingBlocksToolStripMenuItem.Size = new Size(179, 22);
            buildingBlocksToolStripMenuItem.Text = "Building Blocks";
            buildingBlocksToolStripMenuItem.Click += buildingBlocksToolStripMenuItem_Click;
            // 
            // visualizationToolStripMenuItem
            // 
            visualizationToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { viewFIleToolStripMenuItem, viewFileGroupsToolStripMenuItem, compararImágenesToolStripMenuItem });
            visualizationToolStripMenuItem.Name = "visualizationToolStripMenuItem";
            visualizationToolStripMenuItem.Size = new Size(110, 20);
            visualizationToolStripMenuItem.Text = "Visualization";
            // 
            // viewFIleToolStripMenuItem
            // 
            viewFIleToolStripMenuItem.Name = "viewFIleToolStripMenuItem";
            viewFIleToolStripMenuItem.Size = new Size(193, 22);
            viewFIleToolStripMenuItem.Text = "View File";
            viewFIleToolStripMenuItem.Click += viewFIleToolStripMenuItem_Click;
            // 
            // viewFileGroupsToolStripMenuItem
            // 
            viewFileGroupsToolStripMenuItem.Name = "viewFileGroupsToolStripMenuItem";
            viewFileGroupsToolStripMenuItem.Size = new Size(193, 22);
            viewFileGroupsToolStripMenuItem.Text = "View File Groups";
            viewFileGroupsToolStripMenuItem.Click += viewFileGroupsToolStripMenuItem_Click;
            // 
            // compararImágenesToolStripMenuItem
            // 
            compararImágenesToolStripMenuItem.Name = "compararImágenesToolStripMenuItem";
            compararImágenesToolStripMenuItem.Size = new Size(193, 22);
            compararImágenesToolStripMenuItem.Text = "Comparar Imágenes";
            compararImágenesToolStripMenuItem.Click += compararImágenesToolStripMenuItem_Click;
            // 
            // wekaToolStripMenuItem
            // 
            wekaToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { extractorDeReglasToolStripMenuItem, reglaMT4ToolStripMenuItem });
            wekaToolStripMenuItem.Name = "wekaToolStripMenuItem";
            wekaToolStripMenuItem.Size = new Size(40, 20);
            wekaToolStripMenuItem.Text = "EAs";
            // 
            // extractorDeReglasToolStripMenuItem
            // 
            extractorDeReglasToolStripMenuItem.Name = "extractorDeReglasToolStripMenuItem";
            extractorDeReglasToolStripMenuItem.Size = new Size(207, 22);
            extractorDeReglasToolStripMenuItem.Text = "Extractor de Reglas";
            extractorDeReglasToolStripMenuItem.Click += extractorDeReglasToolStripMenuItem_Click;
            // 
            // reglaMT4ToolStripMenuItem
            // 
            reglaMT4ToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { unEAConReglasToolStripMenuItem, eAProbadorDeReglasToolStripMenuItem, eAReglasEspecificasToolStripMenuItem });
            reglaMT4ToolStripMenuItem.Name = "reglaMT4ToolStripMenuItem";
            reglaMT4ToolStripMenuItem.Size = new Size(207, 22);
            reglaMT4ToolStripMenuItem.Text = "MT4";
            // 
            // unEAConReglasToolStripMenuItem
            // 
            unEAConReglasToolStripMenuItem.Name = "unEAConReglasToolStripMenuItem";
            unEAConReglasToolStripMenuItem.Size = new Size(249, 22);
            unEAConReglasToolStripMenuItem.Text = "EA con reglas específicas";
            unEAConReglasToolStripMenuItem.Click += unEAConReglasToolStripMenuItem_Click;
            // 
            // eAProbadorDeReglasToolStripMenuItem
            // 
            eAProbadorDeReglasToolStripMenuItem.Name = "eAProbadorDeReglasToolStripMenuItem";
            eAProbadorDeReglasToolStripMenuItem.Size = new Size(249, 22);
            eAProbadorDeReglasToolStripMenuItem.Text = "Generar EA TotalRules";
            eAProbadorDeReglasToolStripMenuItem.Click += eAProbadorDeReglasToolStripMenuItem_Click;
            // 
            // eAReglasEspecificasToolStripMenuItem
            // 
            eAReglasEspecificasToolStripMenuItem.Name = "eAReglasEspecificasToolStripMenuItem";
            eAReglasEspecificasToolStripMenuItem.Size = new Size(249, 22);
            eAReglasEspecificasToolStripMenuItem.Text = "Generar EA RRR";
            eAReglasEspecificasToolStripMenuItem.Click += eAReglasEspecificasToolStripMenuItem_Click;
            // 
            // cálculoToolStripMenuItem
            // 
            cálculoToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { distribuciónNormalToolStripMenuItem, kolmogorovSmirnovToolStripMenuItem, tradingDeParesToolStripMenuItem });
            cálculoToolStripMenuItem.Name = "cálculoToolStripMenuItem";
            cálculoToolStripMenuItem.Size = new Size(68, 20);
            cálculoToolStripMenuItem.Text = "Cálculo";
            // 
            // distribuciónNormalToolStripMenuItem
            // 
            distribuciónNormalToolStripMenuItem.Name = "distribuciónNormalToolStripMenuItem";
            distribuciónNormalToolStripMenuItem.Size = new Size(207, 22);
            distribuciónNormalToolStripMenuItem.Text = "Distribución Normal";
            distribuciónNormalToolStripMenuItem.Click += distribuciónNormalToolStripMenuItem_Click;
            // 
            // kolmogorovSmirnovToolStripMenuItem
            // 
            kolmogorovSmirnovToolStripMenuItem.Name = "kolmogorovSmirnovToolStripMenuItem";
            kolmogorovSmirnovToolStripMenuItem.Size = new Size(207, 22);
            kolmogorovSmirnovToolStripMenuItem.Text = "Kolmogorov-Smirnov";
            kolmogorovSmirnovToolStripMenuItem.Click += kolmogorovSmirnovToolStripMenuItem_Click;
            // 
            // tradingDeParesToolStripMenuItem
            // 
            tradingDeParesToolStripMenuItem.Name = "tradingDeParesToolStripMenuItem";
            tradingDeParesToolStripMenuItem.Size = new Size(207, 22);
            tradingDeParesToolStripMenuItem.Text = "Trading de Pares";
            tradingDeParesToolStripMenuItem.Click += tradingDeParesToolStripMenuItem_Click;
            // 
            // webScrappingToolStripMenuItem
            // 
            webScrappingToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { alphavantageToolStripMenuItem });
            webScrappingToolStripMenuItem.Name = "webScrappingToolStripMenuItem";
            webScrappingToolStripMenuItem.Size = new Size(110, 20);
            webScrappingToolStripMenuItem.Text = "Web Scrapping";
            // 
            // alphavantageToolStripMenuItem
            // 
            alphavantageToolStripMenuItem.Name = "alphavantageToolStripMenuItem";
            alphavantageToolStripMenuItem.Size = new Size(179, 22);
            alphavantageToolStripMenuItem.Text = "Alphavantage.co";
            alphavantageToolStripMenuItem.Click += alphavantageToolStripMenuItem_Click;
            // 
            // neuronasToolStripMenuItem
            // 
            neuronasToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { entrenamientoToolStripMenuItem });
            neuronasToolStripMenuItem.Name = "neuronasToolStripMenuItem";
            neuronasToolStripMenuItem.Size = new Size(75, 20);
            neuronasToolStripMenuItem.Text = "Neuronas";
            // 
            // entrenamientoToolStripMenuItem
            // 
            entrenamientoToolStripMenuItem.Name = "entrenamientoToolStripMenuItem";
            entrenamientoToolStripMenuItem.Size = new Size(165, 22);
            entrenamientoToolStripMenuItem.Text = "Entrenamiento";
            entrenamientoToolStripMenuItem.Click += entrenamientoToolStripMenuItem_Click;
            // 
            // ruleExtractionToolStripMenuItem
            // 
            ruleExtractionToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { cargaDeFicheroToolStripMenuItem });
            ruleExtractionToolStripMenuItem.Name = "ruleExtractionToolStripMenuItem";
            ruleExtractionToolStripMenuItem.Size = new Size(124, 20);
            ruleExtractionToolStripMenuItem.Text = "Rule Extraction";
            // 
            // cargaDeFicheroToolStripMenuItem
            // 
            cargaDeFicheroToolStripMenuItem.Name = "cargaDeFicheroToolStripMenuItem";
            cargaDeFicheroToolStripMenuItem.Size = new Size(186, 22);
            cargaDeFicheroToolStripMenuItem.Text = "Carga de Fichero";
            cargaDeFicheroToolStripMenuItem.Click += cargaDeFicheroToolStripMenuItem_Click;
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // btnOllamaConnect
            // 
            btnOllamaConnect.Location = new Point(12, 132);
            btnOllamaConnect.Name = "btnOllamaConnect";
            btnOllamaConnect.Size = new Size(132, 23);
            btnOllamaConnect.TabIndex = 1;
            btnOllamaConnect.Text = "Ollama Connect";
            btnOllamaConnect.UseVisualStyleBackColor = true;
            btnOllamaConnect.Click += btnConnect_Click;
            // 
            // txtOllamaAnswer
            // 
            txtOllamaAnswer.Location = new Point(12, 178);
            txtOllamaAnswer.Multiline = true;
            txtOllamaAnswer.Name = "txtOllamaAnswer";
            txtOllamaAnswer.ScrollBars = ScrollBars.Vertical;
            txtOllamaAnswer.Size = new Size(548, 563);
            txtOllamaAnswer.TabIndex = 2;
            // 
            // txtOpenAIAnswer
            // 
            txtOpenAIAnswer.Location = new Point(566, 267);
            txtOpenAIAnswer.Multiline = true;
            txtOpenAIAnswer.Name = "txtOpenAIAnswer";
            txtOpenAIAnswer.ScrollBars = ScrollBars.Vertical;
            txtOpenAIAnswer.Size = new Size(548, 474);
            txtOpenAIAnswer.TabIndex = 3;
            // 
            // txtOllamaQuestion
            // 
            txtOllamaQuestion.Location = new Point(12, 67);
            txtOllamaQuestion.Multiline = true;
            txtOllamaQuestion.Name = "txtOllamaQuestion";
            txtOllamaQuestion.ScrollBars = ScrollBars.Vertical;
            txtOllamaQuestion.Size = new Size(548, 62);
            txtOllamaQuestion.TabIndex = 5;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 44);
            label1.Name = "label1";
            label1.Size = new Size(119, 14);
            label1.TabIndex = 6;
            label1.Text = "Ollama Question:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 161);
            label2.Name = "label2";
            label2.Size = new Size(105, 14);
            label2.TabIndex = 7;
            label2.Text = "Ollama Answer:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(564, 252);
            label3.Name = "label3";
            label3.Size = new Size(56, 14);
            label3.TabIndex = 10;
            label3.Text = "Answer:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(566, 135);
            label4.Name = "label4";
            label4.Size = new Size(70, 14);
            label4.TabIndex = 9;
            label4.Text = "Question:";
            // 
            // txtOpenAIQuestion
            // 
            txtOpenAIQuestion.Location = new Point(566, 152);
            txtOpenAIQuestion.Multiline = true;
            txtOpenAIQuestion.Name = "txtOpenAIQuestion";
            txtOpenAIQuestion.ScrollBars = ScrollBars.Vertical;
            txtOpenAIQuestion.Size = new Size(548, 62);
            txtOpenAIQuestion.TabIndex = 8;
            // 
            // btnOpenAIConnect
            // 
            btnOpenAIConnect.Location = new Point(564, 223);
            btnOpenAIConnect.Name = "btnOpenAIConnect";
            btnOpenAIConnect.Size = new Size(132, 23);
            btnOpenAIConnect.TabIndex = 11;
            btnOpenAIConnect.Text = "OpenAI Connect";
            btnOpenAIConnect.UseVisualStyleBackColor = true;
            btnOpenAIConnect.Click += btnOpenAIConnect_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(337, 44);
            label5.Name = "label5";
            label5.Size = new Size(98, 14);
            label5.TabIndex = 12;
            label5.Text = "Ollama Model:";
            // 
            // cmbModels
            // 
            cmbModels.FormattingEnabled = true;
            cmbModels.Location = new Point(439, 39);
            cmbModels.Name = "cmbModels";
            cmbModels.Size = new Size(121, 22);
            cmbModels.TabIndex = 13;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(566, 44);
            label6.Name = "label6";
            label6.Size = new Size(105, 14);
            label6.TabIndex = 15;
            label6.Text = "System Prompt:";
            // 
            // txtOpenAIPrompt
            // 
            txtOpenAIPrompt.Location = new Point(566, 67);
            txtOpenAIPrompt.Multiline = true;
            txtOpenAIPrompt.Name = "txtOpenAIPrompt";
            txtOpenAIPrompt.ScrollBars = ScrollBars.Vertical;
            txtOpenAIPrompt.Size = new Size(548, 62);
            txtOpenAIPrompt.TabIndex = 14;
            // 
            // MTT_Main
            // 
            AutoScaleDimensions = new SizeF(7F, 14F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1128, 753);
            Controls.Add(label6);
            Controls.Add(txtOpenAIPrompt);
            Controls.Add(cmbModels);
            Controls.Add(label5);
            Controls.Add(btnOpenAIConnect);
            Controls.Add(label3);
            Controls.Add(label4);
            Controls.Add(txtOpenAIQuestion);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txtOllamaQuestion);
            Controls.Add(txtOpenAIAnswer);
            Controls.Add(txtOllamaAnswer);
            Controls.Add(btnOllamaConnect);
            Controls.Add(menuStrip1);
            Font = new Font("Consolas", 9F);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MainMenuStrip = menuStrip1;
            Name = "MTT_Main";
            Text = "Magolo Trading Tools";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem dataToolStripMenuItem;
        private ToolStripMenuItem strategyQuantToolStripMenuItem;
        private ToolStripMenuItem buildingBlocksToolStripMenuItem;
        private ToolStripMenuItem visualizationToolStripMenuItem;
        private ToolStripMenuItem viewFIleToolStripMenuItem;
        private OpenFileDialog openFileDialog1;
        private ToolStripMenuItem viewFileGroupsToolStripMenuItem;
        private FolderBrowserDialog folderBrowserDialog1;
        private ToolStripMenuItem wekaToolStripMenuItem;
        private ToolStripMenuItem reglaMT4ToolStripMenuItem;
        private ToolStripMenuItem unEAConReglasToolStripMenuItem;
        private ToolStripMenuItem eAProbadorDeReglasToolStripMenuItem;
        private ToolStripMenuItem eAReglasEspecificasToolStripMenuItem;
        private ToolStripMenuItem loadCSVToolStripMenuItem;
        private ToolStripMenuItem extractorDeReglasToolStripMenuItem;
        private ToolStripMenuItem cálculoToolStripMenuItem;
        private ToolStripMenuItem distribuciónNormalToolStripMenuItem;
        private ToolStripMenuItem kolmogorovSmirnovToolStripMenuItem;
        private ToolStripMenuItem tradingDeParesToolStripMenuItem;
        private ToolStripMenuItem compararImágenesToolStripMenuItem;
        private ToolStripMenuItem webScrappingToolStripMenuItem;
        private ToolStripMenuItem alphavantageToolStripMenuItem;
        private Button btnOllamaConnect;
        private TextBox txtOllamaAnswer;
        private TextBox txtOpenAIAnswer;
        private ToolStripMenuItem neuronasToolStripMenuItem;
        private ToolStripMenuItem entrenamientoToolStripMenuItem;
        private ToolStripMenuItem ruleExtractionToolStripMenuItem;
        private ToolStripMenuItem cargaDeFicheroToolStripMenuItem;
        private TextBox txtOllamaQuestion;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private TextBox txtOpenAIQuestion;
        private Button btnOpenAIConnect;
        private Label label5;
        private ComboBox cmbModels;
        private Label label6;
        private TextBox txtOpenAIPrompt;
    }
}