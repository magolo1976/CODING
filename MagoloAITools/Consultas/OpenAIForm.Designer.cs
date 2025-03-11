namespace MagoloAITools.Consultas
{
    partial class OpenAIForm
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
            txtOpenAIPrompt = new TextBox();
            btnOpenAIConnect = new Button();
            label3 = new Label();
            label4 = new Label();
            txtOpenAIQuestion = new TextBox();
            txtOpenAIAnswer = new RichTextBox();
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
            // txtOpenAIPrompt
            // 
            txtOpenAIPrompt.Location = new Point(12, 36);
            txtOpenAIPrompt.Multiline = true;
            txtOpenAIPrompt.Name = "txtOpenAIPrompt";
            txtOpenAIPrompt.ScrollBars = ScrollBars.Vertical;
            txtOpenAIPrompt.Size = new Size(947, 62);
            txtOpenAIPrompt.TabIndex = 28;
            // 
            // btnOpenAIConnect
            // 
            btnOpenAIConnect.Location = new Point(10, 192);
            btnOpenAIConnect.Name = "btnOpenAIConnect";
            btnOpenAIConnect.Size = new Size(132, 23);
            btnOpenAIConnect.TabIndex = 25;
            btnOpenAIConnect.Text = "OpenAI Connect";
            btnOpenAIConnect.UseVisualStyleBackColor = true;
            btnOpenAIConnect.Click += btnOpenAIConnect_Click;
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
            // txtOpenAIQuestion
            // 
            txtOpenAIQuestion.Location = new Point(12, 121);
            txtOpenAIQuestion.Multiline = true;
            txtOpenAIQuestion.Name = "txtOpenAIQuestion";
            txtOpenAIQuestion.ScrollBars = ScrollBars.Vertical;
            txtOpenAIQuestion.Size = new Size(947, 62);
            txtOpenAIQuestion.TabIndex = 22;
            // 
            // txtOpenAIAnswer
            // 
            txtOpenAIAnswer.Dock = DockStyle.Bottom;
            txtOpenAIAnswer.Location = new Point(0, 252);
            txtOpenAIAnswer.Name = "txtOpenAIAnswer";
            txtOpenAIAnswer.Size = new Size(974, 470);
            txtOpenAIAnswer.TabIndex = 30;
            txtOpenAIAnswer.Text = "";
            // 
            // OpenAIForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(974, 722);
            Controls.Add(txtOpenAIAnswer);
            Controls.Add(label6);
            Controls.Add(txtOpenAIPrompt);
            Controls.Add(btnOpenAIConnect);
            Controls.Add(label3);
            Controls.Add(label4);
            Controls.Add(txtOpenAIQuestion);
            Name = "OpenAIForm";
            Text = "OPEN AI";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label6;
        private TextBox txtOpenAIPrompt;
        private Button btnOpenAIConnect;
        private Label label3;
        private Label label4;
        private TextBox txtOpenAIQuestion;
        private RichTextBox txtOpenAIAnswer;
    }
}