using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace MTT_IA
{
    public class OpenAIApiResponse
    {
        private readonly string DatoImportante = "xJp0Y+J38j1EwxwsbgH4irKg4JZkTQ92XsXqmYkWTyryNlH4SldaolKIvFNLLHNJY8acRS/gEJx+QXbC4eKQcnrSICv1IwuzqTfIva4OX1BZ6ZnGC9CrIpOpv/jyVOqvZgLHK6RthIs2dSHzFT5X9ur1DG1DQLM27QiojEbLhKTz5N7VFKNHHE7xAWZOJwPAagNPM5mnCgr7PQaSLGD5TYu9km0RHsqmxWwrEwjKIEI=";
        private readonly HttpClient httpClient;

        public OpenAIApiResponse()
        {
            string decryptedSecret = ApiSecretManager.DecryptSecret(DatoImportante);

            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", decryptedSecret);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<string> GetOpenAIResponseAsync(string systemPrompt, string userQuestion)
        {
            
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
                var response = await httpClient.PostAsync("https://api.openai.com/v1/chat/completions", content);
                response.EnsureSuccessStatusCode();

                var jsonResponse = await response.Content.ReadAsStringAsync();
                dynamic responseObject = JsonConvert.DeserializeObject(jsonResponse);

                return responseObject.choices[0].message.content;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error calling OpenAI API: {ex.Message}");
                return string.Empty;
            }
        }
    }
}
