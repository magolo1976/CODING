namespace MagoloRuleExtraction
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
            dataToolStripMenuItem = new ToolStripMenuItem();
            featureSelectionToolStripMenuItem = new ToolStripMenuItem();
            ruleExtractionToolStripMenuItem = new ToolStripMenuItem();
            validationToolStripMenuItem = new ToolStripMenuItem();
            forwardToolStripMenuItem = new ToolStripMenuItem();
            backtestToolStripMenuItem = new ToolStripMenuItem();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.BackColor = Color.HotPink;
            menuStrip1.Items.AddRange(new ToolStripItem[] { dataToolStripMenuItem, featureSelectionToolStripMenuItem, ruleExtractionToolStripMenuItem, validationToolStripMenuItem, forwardToolStripMenuItem, backtestToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(647, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // dataToolStripMenuItem
            // 
            dataToolStripMenuItem.Name = "dataToolStripMenuItem";
            dataToolStripMenuItem.Size = new Size(43, 20);
            dataToolStripMenuItem.Text = "Data";
            dataToolStripMenuItem.Click += dataToolStripMenuItem_Click;
            // 
            // featureSelectionToolStripMenuItem
            // 
            featureSelectionToolStripMenuItem.Name = "featureSelectionToolStripMenuItem";
            featureSelectionToolStripMenuItem.Size = new Size(109, 20);
            featureSelectionToolStripMenuItem.Text = "Feature Selection";
            featureSelectionToolStripMenuItem.Click += featureSelectionToolStripMenuItem_Click;
            // 
            // ruleExtractionToolStripMenuItem
            // 
            ruleExtractionToolStripMenuItem.Name = "ruleExtractionToolStripMenuItem";
            ruleExtractionToolStripMenuItem.Size = new Size(98, 20);
            ruleExtractionToolStripMenuItem.Text = "Rule Extraction";
            ruleExtractionToolStripMenuItem.Click += ruleExtractionToolStripMenuItem_Click;
            // 
            // validationToolStripMenuItem
            // 
            validationToolStripMenuItem.Name = "validationToolStripMenuItem";
            validationToolStripMenuItem.Size = new Size(71, 20);
            validationToolStripMenuItem.Text = "Validation";
            validationToolStripMenuItem.Click += validationToolStripMenuItem_Click;
            // 
            // forwardToolStripMenuItem
            // 
            forwardToolStripMenuItem.Name = "forwardToolStripMenuItem";
            forwardToolStripMenuItem.Size = new Size(62, 20);
            forwardToolStripMenuItem.Text = "Forward";
            forwardToolStripMenuItem.Click += forwardToolStripMenuItem_Click;
            // 
            // backtestToolStripMenuItem
            // 
            backtestToolStripMenuItem.Name = "backtestToolStripMenuItem";
            backtestToolStripMenuItem.Size = new Size(63, 20);
            backtestToolStripMenuItem.Text = "Backtest";
            backtestToolStripMenuItem.Click += backtestToolStripMenuItem_Click;
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.bg1;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(647, 421);
            Controls.Add(menuStrip1);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            MainMenuStrip = menuStrip1;
            Name = "Main";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Magolo Rule Extraction";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem dataToolStripMenuItem;
        private ToolStripMenuItem featureSelectionToolStripMenuItem;
        private ToolStripMenuItem ruleExtractionToolStripMenuItem;
        private ToolStripMenuItem validationToolStripMenuItem;
        private ToolStripMenuItem forwardToolStripMenuItem;
        private ToolStripMenuItem backtestToolStripMenuItem;
    }
}
