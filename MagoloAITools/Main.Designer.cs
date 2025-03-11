namespace MagoloAITools
{
    partial class Main
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
            menuStrip1 = new MenuStrip();
            consultasToolStripMenuItem = new ToolStripMenuItem();
            ollamaToolStripMenuItem = new ToolStripMenuItem();
            openAIToolStripMenuItem = new ToolStripMenuItem();
            claudeToolStripMenuItem = new ToolStripMenuItem();
            geminiToolStripMenuItem = new ToolStripMenuItem();
            chatEntreIAsToolStripMenuItem = new ToolStripMenuItem();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { consultasToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(800, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // consultasToolStripMenuItem
            // 
            consultasToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { chatEntreIAsToolStripMenuItem, ollamaToolStripMenuItem, openAIToolStripMenuItem, claudeToolStripMenuItem, geminiToolStripMenuItem });
            consultasToolStripMenuItem.Name = "consultasToolStripMenuItem";
            consultasToolStripMenuItem.Size = new Size(71, 20);
            consultasToolStripMenuItem.Text = "Consultas";
            // 
            // ollamaToolStripMenuItem
            // 
            ollamaToolStripMenuItem.Name = "ollamaToolStripMenuItem";
            ollamaToolStripMenuItem.Size = new Size(180, 22);
            ollamaToolStripMenuItem.Text = "Ollama";
            ollamaToolStripMenuItem.Click += ollamaToolStripMenuItem_Click;
            // 
            // openAIToolStripMenuItem
            // 
            openAIToolStripMenuItem.Name = "openAIToolStripMenuItem";
            openAIToolStripMenuItem.Size = new Size(180, 22);
            openAIToolStripMenuItem.Text = "OpenAI";
            openAIToolStripMenuItem.Click += openAIToolStripMenuItem_Click;
            // 
            // claudeToolStripMenuItem
            // 
            claudeToolStripMenuItem.Name = "claudeToolStripMenuItem";
            claudeToolStripMenuItem.Size = new Size(180, 22);
            claudeToolStripMenuItem.Text = "Claude";
            claudeToolStripMenuItem.Click += claudeToolStripMenuItem_Click;
            // 
            // geminiToolStripMenuItem
            // 
            geminiToolStripMenuItem.Name = "geminiToolStripMenuItem";
            geminiToolStripMenuItem.Size = new Size(180, 22);
            geminiToolStripMenuItem.Text = "Gemini";
            geminiToolStripMenuItem.Click += geminiToolStripMenuItem_Click;
            // 
            // chatEntreIAsToolStripMenuItem
            // 
            chatEntreIAsToolStripMenuItem.Name = "chatEntreIAsToolStripMenuItem";
            chatEntreIAsToolStripMenuItem.Size = new Size(180, 22);
            chatEntreIAsToolStripMenuItem.Text = "Chat entre IAs";
            chatEntreIAsToolStripMenuItem.Click += chatEntreIAsToolStripMenuItem_Click;
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "Main";
            Text = "Form1";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem consultasToolStripMenuItem;
        private ToolStripMenuItem ollamaToolStripMenuItem;
        private ToolStripMenuItem openAIToolStripMenuItem;
        private ToolStripMenuItem claudeToolStripMenuItem;
        private ToolStripMenuItem geminiToolStripMenuItem;
        private ToolStripMenuItem chatEntreIAsToolStripMenuItem;
    }
}
