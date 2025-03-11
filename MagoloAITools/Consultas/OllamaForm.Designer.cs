namespace MagoloAITools.Consultas
{
    partial class OllamaForm
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
            cmbModels = new ComboBox();
            label5 = new Label();
            label2 = new Label();
            label1 = new Label();
            txtOllamaQuestion = new TextBox();
            btnOllamaConnect = new Button();
            txtOllamaAnswer = new RichTextBox();
            SuspendLayout();
            // 
            // cmbModels
            // 
            cmbModels.FormattingEnabled = true;
            cmbModels.Location = new Point(843, 7);
            cmbModels.Name = "cmbModels";
            cmbModels.Size = new Size(121, 23);
            cmbModels.TabIndex = 27;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(741, 12);
            label5.Name = "label5";
            label5.Size = new Size(85, 15);
            label5.TabIndex = 26;
            label5.Text = "Ollama Model:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 132);
            label2.Name = "label2";
            label2.Size = new Size(90, 15);
            label2.TabIndex = 21;
            label2.Text = "Ollama Answer:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 15);
            label1.Name = "label1";
            label1.Size = new Size(99, 15);
            label1.TabIndex = 20;
            label1.Text = "Ollama Question:";
            // 
            // txtOllamaQuestion
            // 
            txtOllamaQuestion.Location = new Point(12, 38);
            txtOllamaQuestion.Multiline = true;
            txtOllamaQuestion.Name = "txtOllamaQuestion";
            txtOllamaQuestion.ScrollBars = ScrollBars.Vertical;
            txtOllamaQuestion.Size = new Size(952, 62);
            txtOllamaQuestion.TabIndex = 19;
            // 
            // btnOllamaConnect
            // 
            btnOllamaConnect.Location = new Point(12, 103);
            btnOllamaConnect.Name = "btnOllamaConnect";
            btnOllamaConnect.Size = new Size(132, 23);
            btnOllamaConnect.TabIndex = 16;
            btnOllamaConnect.Text = "Ollama Connect";
            btnOllamaConnect.UseVisualStyleBackColor = true;
            btnOllamaConnect.Click += btnOllamaConnect_Click;
            // 
            // txtOllamaAnswer
            // 
            txtOllamaAnswer.Dock = DockStyle.Bottom;
            txtOllamaAnswer.Location = new Point(0, 159);
            txtOllamaAnswer.Name = "txtOllamaAnswer";
            txtOllamaAnswer.Size = new Size(974, 563);
            txtOllamaAnswer.TabIndex = 28;
            txtOllamaAnswer.Text = "";
            // 
            // OllamaForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(974, 722);
            Controls.Add(txtOllamaAnswer);
            Controls.Add(cmbModels);
            Controls.Add(label5);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txtOllamaQuestion);
            Controls.Add(btnOllamaConnect);
            Name = "OllamaForm";
            Text = "OLLAMA";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private ComboBox cmbModels;
        private Label label5;
        private Label label2;
        private Label label1;
        private TextBox txtOllamaQuestion;
        private Button btnOllamaConnect;
        private RichTextBox txtOllamaAnswer;
    }
}