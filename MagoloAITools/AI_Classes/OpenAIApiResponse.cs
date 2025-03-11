using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace MagoloAITools.AI_Classes
{
    public class OpenAIApiResponse
    {
        private readonly string DatoImportante = "DvXz3xrZ4/dgS9VyWNuv/wKgb4MZ6YurlR4xuwX8OUPNV0lKmU83upGAuYgUABqhXXMboKolqRYd3O89z1XZQghNgG7YMPbSxpUbzdpv+utnimlxwz4FJakMqO9BOR6FF3YYTkWBVkWZXegzlw+jYpnZ7dHQ0Ca7sp9cLmZdsWFaG4g0jhNTmwm6w9E7l5Ku1RpvchKepkozhtpRX8YkVyvpT/6JQWeYIi/GeAEmqFw=";
        private readonly HttpClient _httpClient;

        public OpenAIApiResponse()
        {
            //string encrypt = ApiSecretManager.EncryptSecret(DatoImportante);
            string decryptedSecret = ApiSecretManager.DecryptSecret(DatoImportante);

            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", decryptedSecret);
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<string> GetOpenAIResponseAsync(string systemPrompt, string userQuestion)
        {
            // POR SI SE OBSOLETA EL MODELO: https://docs.anthropic.com/en/docs/resources/model-deprecations

            var payload = new[] {
                new { role = "system", content = systemPrompt },
                new { role = "user", content = userQuestion }
            };

            var requestBody = new {
                model = "gpt-4o-mini",
                messages = payload
            };

            var jsonRequestBody = JsonConvert.SerializeObject(requestBody);
            var content = new StringContent(jsonRequestBody, Encoding.UTF8, "application/json");

            try
            {
                var response = await _httpClient.PostAsync("https://api.openai.com/v1/chat/completions", content);
                response.EnsureSuccessStatusCode();

                var jsonResponse = await response.Content.ReadAsStringAsync();
                dynamic responseObject = JsonConvert.DeserializeObject(jsonResponse);

                return responseObject.choices[0].message.content;
            }
            catch (Exception ex)
            {
                return $"Error calling OpenAI API: {ex.Message}";
            }
        }
    }
}
