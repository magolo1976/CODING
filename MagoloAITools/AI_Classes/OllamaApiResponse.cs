using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace MagoloAITools.AI_Classes
{
    public class OllamaApiResponse
    {
        private readonly HttpClient _httpClient;

        public OllamaApiResponse()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<string> GetOllamaResponseAsync(string _question, string _model)
        {
            // Define the URL and the headers
            string urlApi = "http://localhost:11434/api/chat";

            // Create the payload
            var payload = new
            {
                model = _model,
                messages = new[]  {
                    new {
                        role = "user",
                        content = _question
                    }
                },
                stream = false
            };

            // Serialize the payload to JSON
            var jsonPayload = JsonConvert.SerializeObject(payload);

            // Create the content to send in the POST request
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            try
            {
                // Send the POST request
                var response = await _httpClient.PostAsync(urlApi, content);
                response.EnsureSuccessStatusCode(); // Throw if not a success code.

                // Parse the response JSON
                var jsonResponse = await response.Content.ReadAsStringAsync();
                dynamic result = JsonConvert.DeserializeObject(jsonResponse);

                // Get the desired summary from the response
                return result.message.content;

            }
            catch (Exception ex)
            {
                // Handle any exceptions
                return  ($"Error occurred: {ex.Message}");
            }
        }
    }
}
