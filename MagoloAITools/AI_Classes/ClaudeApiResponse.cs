using Newtonsoft.Json;
using System.Text;
using System.Text.Json;

namespace MagoloAITools.AI_Classes
{
    public class ClaudeApiResponse
    {
        private readonly string DatoImportante = "qHl+Fo06v3P7Uej1ox0OK5ncABg8FrKQnNyHX5yTjfu0jdtrZhPDzGATfRfA2x3Fhs0SJjJZo3z0FmyhIQ3mtke95e0fXEERCVJszTiz4Cy4Gi1geLQbIhgj69tg7ZXQBQR0vGKrXAqr40Su2MINVQ==";
        private readonly HttpClient _httpClient;

        public ClaudeApiResponse()
        {
            //string encrypt = ApiSecretManager.EncryptSecret(DatoImportante);
            string decryptedSecret = ApiSecretManager.DecryptSecret(DatoImportante);

            // Configuración del cliente HTTP
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("x-api-key", decryptedSecret); // Reemplaza con tu API key
            _httpClient.DefaultRequestHeaders.Add("anthropic-version", "2023-06-01"); // Versión de la API

        }

        public async Task<string> GetClaudeResponseAsync(string systemMessage, string userPrompt)
        {
            // Parámetros para el retroceso exponencial
            int maxRetries = 5;
            int retryCount = 0;
            int baseDelayMs = 1000; // 1 segundo inicial

            while (true)
            {
                try
                {
                    // Preparando la solicitud
                    var requestData = new
                    {
                        model = "claude-3-5-sonnet-20240620",
                        max_tokens = 200,
                        temperature = 0.7,
                        system = systemMessage,
                        messages = new[] {
                            new { role = "user", content = userPrompt }
                        }
                    };

                    var content = new StringContent(
                        JsonConvert.SerializeObject(requestData),
                        Encoding.UTF8,
                        "application/json");

                    // Enviando la solicitud POST
                    var response = await _httpClient.PostAsync("https://api.anthropic.com/v1/messages", content);

                    // Verificando el código de estado
                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        using var document = JsonDocument.Parse(responseContent);

                        return document.RootElement
                            .GetProperty("content")[0]
                            .GetProperty("text")
                            .GetString();
                    }
                    else if ((int)response.StatusCode == 529 || (int)response.StatusCode == 429)
                    {
                        // Comprobar si hemos alcanzado el número máximo de reintentos
                        if (retryCount >= maxRetries)
                        {
                            var errorContent = await response.Content.ReadAsStringAsync();
                            throw new Exception($"Max retries reached. Last error: {response.StatusCode}, Details: {errorContent}");
                        }

                        // Calcular el tiempo de espera con retroceso exponencial
                        int delayMs = baseDelayMs * (int)Math.Pow(2, retryCount);
                        // Añadir algo de aleatoriedad para evitar que múltiples clientes se sincronicen
                        Random random = new Random();
                        delayMs += random.Next(0, 1000);

                        Console.WriteLine($"Received error {response.StatusCode}. Retrying in {delayMs}ms (Attempt {retryCount + 1}/{maxRetries})");

                        // Esperar antes de reintentar
                        await Task.Delay(delayMs);
                        retryCount++;
                        continue;
                    }
                    else
                    {
                        var errorContent = await response.Content.ReadAsStringAsync();
                        throw new HttpRequestException($"Error calling Claude API: {response.StatusCode}, Details: {errorContent}");
                    }
                }
                catch (HttpRequestException ex)
                {
                    // Manejar excepciones de red
                    if (retryCount >= maxRetries)
                    {
                        throw new Exception($"Max retries reached due to network error: {ex.Message}", ex);
                    }

                    int delayMs = baseDelayMs * (int)Math.Pow(2, retryCount);
                    Console.WriteLine($"Network error: {ex.Message}. Retrying in {delayMs}ms (Attempt {retryCount + 1}/{maxRetries})");

                    await Task.Delay(delayMs);
                    retryCount++;
                }
            }
        }

    }
}
