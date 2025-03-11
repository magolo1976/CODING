namespace MagoloAITools.Consultas
{
    partial class GeminiForm
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
            label6 = new Label();
            txtGeminiPrompt = new TextBox();
            btnOpenAIConnect = new Button();
            label3 = new Label();
            label4 = new Label();
            txtGeminiQuestion = new TextBox();
            txtGeminiAnswer = new RichTextBox();
            SuspendLayout();
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(12, 13);
            label6.Name = "label6";
            label6.Size = new Size(91, 15);
            label6.TabIndex = 29;
            label6.Text = "System Prompt:";
            // 
            // txtGeminiPrompt
            // 
            txtGeminiPrompt.Location = new Point(12, 36);
            txtGeminiPrompt.Multiline = true;
            txtGeminiPrompt.Name = "txtGeminiPrompt";
            txtGeminiPrompt.ScrollBars = ScrollBars.Vertical;
            txtGeminiPrompt.Size = new Size(947, 62);
            txtGeminiPrompt.TabIndex = 28;
            // 
            // btnOpenAIConnect
            // 
            btnOpenAIConnect.Location = new Point(10, 192);
            btnOpenAIConnect.Name = "btnOpenAIConnect";
            btnOpenAIConnect.Size = new Size(132, 23);
            btnOpenAIConnect.TabIndex = 25;
            btnOpenAIConnect.Text = "Gemini Connect";
            btnOpenAIConnect.UseVisualStyleBackColor = true;
            btnOpenAIConnect.Click += btnGeminiConnect_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(10, 221);
            label3.Name = "label3";
            label3.Size = new Size(49, 15);
            label3.TabIndex = 24;
            label3.Text = "Answer:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 104);
            label4.Name = "label4";
            label4.Size = new Size(58, 15);
            label4.TabIndex = 23;
            label4.Text = "Question:";
            // 
            // txtGeminiQuestion
            // 
            txtGeminiQuestion.Location = new Point(12, 121);
            txtGeminiQuestion.Multiline = true;
            txtGeminiQuestion.Name = "txtGeminiQuestion";
            txtGeminiQuestion.ScrollBars = ScrollBars.Vertical;
            txtGeminiQuestion.Size = new Size(947, 62);
            txtGeminiQuestion.TabIndex = 22;
            // 
            // txtGeminiAnswer
            // 
            txtGeminiAnswer.Dock = DockStyle.Bottom;
            txtGeminiAnswer.Location = new Point(0, 239);
            txtGeminiAnswer.Name = "txtGeminiAnswer";
            txtGeminiAnswer.Size = new Size(974, 483);
            txtGeminiAnswer.TabIndex = 30;
            txtGeminiAnswer.Text = "";
            // 
            // GeminiForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(974, 722);
            Controls.Add(txtGeminiAnswer);
            Controls.Add(label6);
            Controls.Add(txtGeminiPrompt);
            Controls.Add(btnOpenAIConnect);
            Controls.Add(label3);
            Controls.Add(label4);
            Controls.Add(txtGeminiQuestion);
            Name = "GeminiForm";
            Text = "GEMINI";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label6;
        private TextBox txtGeminiPrompt;
        private Button btnOpenAIConnect;
        private Label label3;
        private Label label4;
        private TextBox txtGeminiQuestion;
        private RichTextBox txtGeminiAnswer;
    }
}