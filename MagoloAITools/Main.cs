using MagoloAITools.AI_Classes;
using MagoloAITools.Consultas;

namespace MagoloAITools
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        #region CONSULTAS

        private void ollamaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OllamaForm consulta = new OllamaForm();
            consulta.Show();
        }

        private void openAIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenAIForm consulta = new OpenAIForm();
            consulta.Show();
        }

        private void claudeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClaudeForm consulta = new ClaudeForm();
            consulta.Show();
        }

        private void geminiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GeminiForm conslta = new GeminiForm();
            conslta.Show();
        }

        private void chatEntreIAsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        #endregion

    }
}
