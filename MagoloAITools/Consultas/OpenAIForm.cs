using MagoloAITools.AI_Classes;
using MagoloAITools.AI_Tools;

namespace MagoloAITools.Consultas
{
    public partial class OpenAIForm : Form
    {
        public OpenAIForm()
        {
            InitializeComponent();
        }

        #region IA

        private async void btnOpenAIConnect_Click(object sender, EventArgs e)
        {
            try
            {
                string systemPrompt = txtOpenAIPrompt.Text;
                string question = txtOpenAIQuestion.Text;

                txtOpenAIAnswer.Text = "******************************** PENSANDO !!!";

                string response = await new OpenAIApiResponse().GetOpenAIResponseAsync(systemPrompt, question);

                RichTextFormatter.FormatText(txtOpenAIAnswer, response);

            }
            catch (Exception ex)
            {
                txtOpenAIAnswer.Text = $"Error principal: {ex.Message}";
            }
        }

        #endregion
    }
}
