namespace MagoloAITools.Consultas
{
    partial class ClaudeForm
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
            txtClaudePrompt = new TextBox();
            btnOpenAIConnect = new Button();
            label3 = new Label();
            label4 = new Label();
            txtClaudeQuestion = new TextBox();
            txtClaudeAnswer = new RichTextBox();
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
            // txtClaudePrompt
            // 
            txtClaudePrompt.Location = new Point(12, 36);
            txtClaudePrompt.Multiline = true;
            txtClaudePrompt.Name = "txtClaudePrompt";
            txtClaudePrompt.ScrollBars = ScrollBars.Vertical;
            txtClaudePrompt.Size = new Size(947, 62);
            txtClaudePrompt.TabIndex = 28;
            // 
            // btnOpenAIConnect
            // 
            btnOpenAIConnect.Location = new Point(10, 192);
            btnOpenAIConnect.Name = "btnOpenAIConnect";
            btnOpenAIConnect.Size = new Size(132, 23);
            btnOpenAIConnect.TabIndex = 25;
            btnOpenAIConnect.Text = "Claude Connect";
            btnOpenAIConnect.UseVisualStyleBackColor = true;
            btnOpenAIConnect.Click += btnClaudeConnect_Click;
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
            // txtClaudeQuestion
            // 
            txtClaudeQuestion.Location = new Point(12, 121);
            txtClaudeQuestion.Multiline = true;
            txtClaudeQuestion.Name = "txtClaudeQuestion";
            txtClaudeQuestion.ScrollBars = ScrollBars.Vertical;
            txtClaudeQuestion.Size = new Size(947, 62);
            txtClaudeQuestion.TabIndex = 22;
            // 
            // txtClaudeAnswer
            // 
            txtClaudeAnswer.Dock = DockStyle.Bottom;
            txtClaudeAnswer.Location = new Point(0, 239);
            txtClaudeAnswer.Name = "txtClaudeAnswer";
            txtClaudeAnswer.Size = new Size(974, 483);
            txtClaudeAnswer.TabIndex = 30;
            txtClaudeAnswer.Text = "";
            // 
            // ClaudeForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(974, 722);
            Controls.Add(txtClaudeAnswer);
            Controls.Add(label6);
            Controls.Add(txtClaudePrompt);
            Controls.Add(btnOpenAIConnect);
            Controls.Add(label3);
            Controls.Add(label4);
            Controls.Add(txtClaudeQuestion);
            Name = "ClaudeForm";
            Text = "CLAUDE";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label6;
        private TextBox txtClaudePrompt;
        private Button btnOpenAIConnect;
        private Label label3;
        private Label label4;
        private TextBox txtClaudeQuestion;
        private RichTextBox txtClaudeAnswer;
    }
}