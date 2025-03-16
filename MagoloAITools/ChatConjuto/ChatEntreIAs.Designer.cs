namespace MagoloAITools.ChatConjuto
{
    partial class ChatEntreIAs
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
            btnStart = new Button();
            rtbConversation = new RichTextBox();
            txtSystemGPT = new TextBox();
            txtSystemClaude = new TextBox();
            txtSystemGemini = new TextBox();
            txtUserGPT = new TextBox();
            txtUserClaude = new TextBox();
            txtUserGemini = new TextBox();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            checkBoxGPT = new CheckBox();
            checkBoxClaude = new CheckBox();
            checkBoxGemini = new CheckBox();
            button1 = new Button();
            SuspendLayout();
            // 
            // btnStart
            // 
            btnStart.BackColor = SystemColors.ActiveCaption;
            btnStart.Location = new Point(13, 106);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(137, 65);
            btnStart.TabIndex = 0;
            btnStart.Text = "Iterar conversación";
            btnStart.UseVisualStyleBackColor = false;
            btnStart.Click += btnStart_Click;
            // 
            // rtbConversation
            // 
            rtbConversation.Location = new Point(13, 244);
            rtbConversation.Name = "rtbConversation";
            rtbConversation.Size = new Size(1222, 509);
            rtbConversation.TabIndex = 1;
            rtbConversation.Text = "";
            // 
            // txtSystemGPT
            // 
            txtSystemGPT.Location = new Point(230, 23);
            txtSystemGPT.Multiline = true;
            txtSystemGPT.Name = "txtSystemGPT";
            txtSystemGPT.Size = new Size(265, 102);
            txtSystemGPT.TabIndex = 4;
            // 
            // txtSystemClaude
            // 
            txtSystemClaude.Location = new Point(590, 23);
            txtSystemClaude.Multiline = true;
            txtSystemClaude.Name = "txtSystemClaude";
            txtSystemClaude.Size = new Size(265, 102);
            txtSystemClaude.TabIndex = 5;
            // 
            // txtSystemGemini
            // 
            txtSystemGemini.Location = new Point(970, 22);
            txtSystemGemini.Multiline = true;
            txtSystemGemini.Name = "txtSystemGemini";
            txtSystemGemini.Size = new Size(265, 102);
            txtSystemGemini.TabIndex = 6;
            // 
            // txtUserGPT
            // 
            txtUserGPT.Location = new Point(230, 137);
            txtUserGPT.Multiline = true;
            txtUserGPT.Name = "txtUserGPT";
            txtUserGPT.Size = new Size(265, 102);
            txtUserGPT.TabIndex = 7;
            // 
            // txtUserClaude
            // 
            txtUserClaude.Location = new Point(590, 137);
            txtUserClaude.Multiline = true;
            txtUserClaude.Name = "txtUserClaude";
            txtUserClaude.Size = new Size(265, 102);
            txtUserClaude.TabIndex = 8;
            // 
            // txtUserGemini
            // 
            txtUserGemini.Location = new Point(970, 137);
            txtUserGemini.Multiline = true;
            txtUserGemini.Name = "txtUserGemini";
            txtUserGemini.Size = new Size(265, 102);
            txtUserGemini.TabIndex = 9;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(879, 31);
            label2.Name = "label2";
            label2.Size = new Size(86, 15);
            label2.TabIndex = 10;
            label2.Text = "System Gemini";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(154, 31);
            label3.Name = "label3";
            label3.Size = new Size(69, 15);
            label3.TabIndex = 10;
            label3.Text = "System GPT";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(501, 31);
            label4.Name = "label4";
            label4.Size = new Size(85, 15);
            label4.TabIndex = 11;
            label4.Text = "System Claude";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(169, 140);
            label5.Name = "label5";
            label5.Size = new Size(54, 15);
            label5.TabIndex = 12;
            label5.Text = "User GPT";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(516, 140);
            label6.Name = "label6";
            label6.Size = new Size(70, 15);
            label6.TabIndex = 13;
            label6.Text = "User Claude";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(894, 140);
            label7.Name = "label7";
            label7.Size = new Size(71, 15);
            label7.TabIndex = 14;
            label7.Text = "User Gemini";
            // 
            // checkBoxGPT
            // 
            checkBoxGPT.AutoSize = true;
            checkBoxGPT.Checked = true;
            checkBoxGPT.CheckState = CheckState.Checked;
            checkBoxGPT.Location = new Point(231, 4);
            checkBoxGPT.Name = "checkBoxGPT";
            checkBoxGPT.Size = new Size(49, 19);
            checkBoxGPT.TabIndex = 15;
            checkBoxGPT.Text = "Usar";
            checkBoxGPT.UseVisualStyleBackColor = true;
            // 
            // checkBoxClaude
            // 
            checkBoxClaude.AutoSize = true;
            checkBoxClaude.Checked = true;
            checkBoxClaude.CheckState = CheckState.Checked;
            checkBoxClaude.Location = new Point(590, 4);
            checkBoxClaude.Name = "checkBoxClaude";
            checkBoxClaude.Size = new Size(49, 19);
            checkBoxClaude.TabIndex = 16;
            checkBoxClaude.Text = "Usar";
            checkBoxClaude.UseVisualStyleBackColor = true;
            // 
            // checkBoxGemini
            // 
            checkBoxGemini.AutoSize = true;
            checkBoxGemini.Checked = true;
            checkBoxGemini.CheckState = CheckState.Checked;
            checkBoxGemini.Location = new Point(970, 4);
            checkBoxGemini.Name = "checkBoxGemini";
            checkBoxGemini.Size = new Size(49, 19);
            checkBoxGemini.TabIndex = 17;
            checkBoxGemini.Text = "Usar";
            checkBoxGemini.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            button1.BackColor = Color.FromArgb(128, 255, 128);
            button1.Location = new Point(13, 74);
            button1.Name = "button1";
            button1.Size = new Size(137, 23);
            button1.TabIndex = 18;
            button1.Text = "Setear Preguntas";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // ChatEntreIAs
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1247, 762);
            Controls.Add(button1);
            Controls.Add(checkBoxGemini);
            Controls.Add(checkBoxClaude);
            Controls.Add(checkBoxGPT);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(txtUserGemini);
            Controls.Add(txtUserClaude);
            Controls.Add(txtUserGPT);
            Controls.Add(txtSystemGemini);
            Controls.Add(txtSystemClaude);
            Controls.Add(txtSystemGPT);
            Controls.Add(rtbConversation);
            Controls.Add(btnStart);
            Name = "ChatEntreIAs";
            Text = "ChatEntreIAs";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnStart;
        private RichTextBox rtbConversation;
        private TextBox txtSystemGPT;
        private TextBox txtSystemClaude;
        private TextBox txtSystemGemini;
        private TextBox txtUserGPT;
        private TextBox txtUserClaude;
        private TextBox txtUserGemini;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private CheckBox checkBoxGPT;
        private CheckBox checkBoxClaude;
        private CheckBox checkBoxGemini;
        private Button button1;
    }
}