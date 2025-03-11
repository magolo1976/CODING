using MagoloAITools.AI_Classes;
using MagoloAITools.AI_Tools;

namespace MagoloAITools.Consultas
{
    public partial class ClaudeForm : Form
    {
        public ClaudeForm()
        {
            InitializeComponent();
        }

        #region IA

        private async void btnClaudeConnect_Click(object sender, EventArgs e)
        {
            try
            {
                string systemPrompt = txtClaudePrompt.Text;
                string question = txtClaudeQuestion.Text;

                txtClaudeAnswer.Text = "******************************** PENSANDO !!!";

                string response = await new ClaudeApiResponse().GetClaudeResponseAsync(systemPrompt, question);

                RichTextFormatter.FormatText(txtClaudeAnswer, response);

            }
            catch (Exception ex)
            {
                txtClaudeAnswer.Text = $"Error principal: {ex.Message}";
            }
        }

        #endregion


    }
}
