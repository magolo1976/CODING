using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace MagoloAITools.AI_Classes
{
    public class GeminiApiResponse
    {
        private readonly string DatoImportante = "s3iMasGtC0u1cDpqGxv6y3zQS2KsPGQu1cYvc/iTSjfVmTJRwwTVan+ixT5673lH";
        private readonly string _modelName = "gemini-1.5-flash";
        private readonly string _apiKey;

        public GeminiApiResponse()
        {
            //string encrypt = ApiSecretManager.EncryptSecret(DatoImportante);
            _apiKey = ApiSecretManager.DecryptSecret(DatoImportante);
        }

        public async Task<string> GetGeminiResponseAsync(string systemPrompt, string userQuestion)
        {
            // Preparar la URL con la clave API
            string apiUrl = $"https://generativelanguage.googleapis.com/v1beta/models/{ _modelName }:generateContent?key={ _apiKey }";

            string datoReturn = "";

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string combinedPrompt = systemPrompt + userQuestion;

                var requestBody = new {
                    contents = new[] {
                        new { parts = new[] { new { text = combinedPrompt } } }
                    }
                };

                string jsonRequestBody = JsonConvert.SerializeObject(requestBody);
                var content = new StringContent(jsonRequestBody, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    dynamic jsonResponse = JsonConvert.DeserializeObject(responseContent);

                    datoReturn = jsonResponse.candidates[0].content.parts[0].text;
                }
                else
                {
                    datoReturn = $"Error: {response.StatusCode}\n";
                    datoReturn += await response.Content.ReadAsStringAsync();
                }
            }

            return datoReturn;
        }
    }
}
