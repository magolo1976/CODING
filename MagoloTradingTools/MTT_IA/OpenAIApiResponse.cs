using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace MTT_IA
{
    public class OpenAIApiResponse
    {
        private readonly string ApiKey = "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";
        private readonly HttpClient httpClient;

        public OpenAIApiResponse()
        {
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ApiKey);
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
