
using MagoloAITools.AI_Classes;
using MagoloAITools.AI_Tools;
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Forms;

namespace MagoloAITools.Consultas
{
    public partial class OllamaForm : Form
    {
        public OllamaForm()
        {
            InitializeComponent();

            cmbModels.Items.Add("llama3.2");
            cmbModels.Items.Add("qwen2.5:32b");
            cmbModels.SelectedIndex = 0;
        }

        #region IA

        private async void btnOllamaConnect_Click(object sender, EventArgs e)
        {
            try
            {
                string question = txtOllamaQuestion.Text.Trim();
                string model = cmbModels.SelectedItem.ToString();

                txtOllamaAnswer.Text = "******************************** PENSANDO !!!";

                string response = await new OllamaApiResponse().GetOllamaResponseAsync(question, model);

                RichTextFormatter.FormatText(txtOllamaAnswer, response);

            }
            catch (Exception ex)
            {
                txtOllamaAnswer.Text = $"Error principal: {ex.Message}";
            }
        }

        #endregion
    }
}
