using MagoloAITools.AI_Classes;
using MagoloAITools.AI_Tools;

namespace MagoloAITools.Consultas
{
    public partial class GeminiForm : Form
    {
        public GeminiForm()
        {
            InitializeComponent();
        }

        #region IA

        private async void btnGeminiConnect_Click(object sender, EventArgs e)
        {
            try
            {
                string systemPrompt = txtGeminiPrompt.Text;
                string question = txtGeminiQuestion.Text;

                txtGeminiAnswer.Text = "******************************** PENSANDO !!!";

                string response = await new GeminiApiResponse().GetGeminiResponseAsync(systemPrompt, question);

                RichTextFormatter.FormatText(txtGeminiAnswer, response);

            }
            catch (Exception ex)
            {
                txtGeminiAnswer.Text = $"Error principal: {ex.Message}";
            }
        }

        #endregion

    }
}
