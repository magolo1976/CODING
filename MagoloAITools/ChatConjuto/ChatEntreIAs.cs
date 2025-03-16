using MagoloAITools.AI_Classes;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;

namespace MagoloAITools.ChatConjuto
{
    public partial class ChatEntreIAs : Form
    {
        private readonly string DatoImportanteOpenAi = "DvXz3xrZ4/dgS9VyWNuv/wKgb4MZ6YurlR4xuwX8OUPNV0lKmU83upGAuYgUABqhXXMboKolqRYd3O89z1XZQghNgG7YMPbSxpUbzdpv+utnimlxwz4FJakMqO9BOR6FF3YYTkWBVkWZXegzlw+jYpnZ7dHQ0Ca7sp9cLmZdsWFaG4g0jhNTmwm6w9E7l5Ku1RpvchKepkozhtpRX8YkVyvpT/6JQWeYIi/GeAEmqFw=";
        private readonly string DatoImportanteClaude = "qHl+Fo06v3P7Uej1ox0OK5ncABg8FrKQnNyHX5yTjfu0jdtrZhPDzGATfRfA2x3Fhs0SJjJZo3z0FmyhIQ3mtke95e0fXEERCVJszTiz4Cy4Gi1geLQbIhgj69tg7ZXQBQR0vGKrXAqr40Su2MINVQ==";
        private readonly string DatoImportanteGemini = "s3iMasGtC0u1cDpqGxv6y3zQS2KsPGQu1cYvc/iTSjfVmTJRwwTVan+ixT5673lH";

        private readonly HttpClient _httpClient;

        // API Keys y URLs
        private readonly string _openAiApiKey;
        private readonly string _anthropicApiKey;
        private readonly string _googleApiKey;

        // Modelos
        private readonly string _gptModel = "gpt-4o-mini";
        private readonly string _claudeModel = "claude-3-5-sonnet-20240620";
        private readonly string _geminiModel = "gemini-1.5-flash";

        // Instrucciones de sistema
        private string _gptSystem = "You are a chatbot who is very argumentative; " +
                "you disagree with anything in the conversation and you challenge everything, in a snarky way.";

        private string _claudeSystem = "You are a very polite, courteous chatbot. You try to agree with " +
                "everything the other people in the conversation say, or find common ground. If another person is argumentative, " +
                "you try to calm them down and keep chatting.";

        private string _geminiSystem = "You are an extremely knowledgeable and know-it-all counselor chatbot. You try to help resolve disagreements, " +
                "and if a person is either too argumentative or too polite, you cannot help but to use quotes from famous psychologists to teach " +
                "your students to be kind yet maintain boundaries.";

        // Nombres de los agentes
        private string _gptName = "Bob";
        private string _claudeName = "Larry";
        private string _geminiName = "Frank";

        // Historial de mensajes
        private List<string> _gptMessages;
        private List<string> _claudeMessages;
        private List<string> _geminiMessages;

        private int Rondas = 0;

        /// <summary>
        /// Constructor para el simulador de conversación entre agentes de IA.
        /// </summary>
        /// <param name="openAiApiKey">API Key para OpenAI</param>
        /// <param name="anthropicApiKey">API Key para Anthropic</param>
        /// <param name="googleApiKey">API Key para Google</param>
        public ChatEntreIAs()
        {
            InitializeComponent();

            _openAiApiKey = ApiSecretManager.DecryptSecret(DatoImportanteOpenAi); ;
            _anthropicApiKey = ApiSecretManager.DecryptSecret(DatoImportanteClaude);
            _googleApiKey = ApiSecretManager.DecryptSecret(DatoImportanteGemini);

            _httpClient = new HttpClient();

            // Inicializar mensajes
            txtSystemGPT.Text = _gptSystem;
            txtSystemClaude.Text = _claudeSystem;
            txtSystemGemini.Text = _geminiSystem;

            txtUserGPT.Text = "Hi there";
            txtUserClaude.Text = "Hi";
            txtUserGemini.Text = "How is everyone?";

        }

        /// <summary>
        /// Construye un mensaje que combina las respuestas de dos agentes.
        /// </summary>
        private string ConstructJoinedUserMsg(bool useThisA, string msg1, string msg1Name, bool useThisB, string msg2, string msg2Name)
        {
            if (useThisA && useThisB)
                return $"{msg1Name} said: {msg1}. \n\nThen {msg2Name} said: {msg2}.";

            if (useThisA && !useThisB)
                return $"{msg1Name} said: {msg1}.";

            if (!useThisA && useThisB)
                return $"{msg2Name} said: {msg2}.";

            return "";
        }

        /// <summary>
        /// Llama a la API de OpenAI (GPT) y obtiene una respuesta.
        /// </summary>
        private async Task<string> CallGptAsync()
        {
            // Preparar URL y headers
            var url = "https://api.openai.com/v1/chat/completions";
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _openAiApiKey);
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Construir la lista de mensajes
            var messages = new List<object>
            {
                new { role = "system", content = txtSystemGPT.Text } // _gptSystem }
            };

            for (int i = 0; i < Math.Min(_gptMessages.Count, Math.Min(_claudeMessages.Count, _geminiMessages.Count)); i++)
            {
                messages.Add(new { role = "assistant", content = _gptMessages[i] });
                messages.Add(new { role = "user", content = ConstructJoinedUserMsg(checkBoxClaude.Checked, _claudeMessages[i], _claudeName, checkBoxGemini.Checked, _geminiMessages[i], _geminiName) });
            }

            // Crear payload
            var payload = new
            {
                model = _gptModel,
                messages
            };

            var jsonContent = JsonSerializer.Serialize(payload);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            // Realizar la llamada
            var response = await _httpClient.PostAsync(url, content);
            var responseString = await response.Content.ReadAsStringAsync();

            // Parsear la respuesta
            using JsonDocument doc = JsonDocument.Parse(responseString);
            var choices = doc.RootElement.GetProperty("choices");
            var message = choices[0].GetProperty("message");
            var text = message.GetProperty("content").GetString();

            rtbConversation.Text += $"================== GPT: \n{text}\n==================\n\n";

            return text;
        }

        /// <summary>
        /// Llama a la API de Anthropic (Claude) y obtiene una respuesta.
        /// </summary>
        private async Task<string> CallClaudeAsync()
        {
            // Preparar URL y headers
            var url = "https://api.anthropic.com/v1/messages";
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("x-api-key", _anthropicApiKey);
            _httpClient.DefaultRequestHeaders.Add("anthropic-version", "2023-06-01");

            // Construir la lista de mensajes
            var messages = new List<object>();

            for (int i = 0; i < Math.Min(_gptMessages.Count, Math.Min(_claudeMessages.Count, _geminiMessages.Count)); i++)
            {
                messages.Add(new { role = "user", content = ConstructJoinedUserMsg(checkBoxGemini.Checked, _geminiMessages[i], _geminiName, checkBoxGPT.Checked, _gptMessages[i], _gptName) });
                messages.Add(new { role = "assistant", content = _claudeMessages[i] });
            }

            messages.Add(new { role = "user", content = $"{_gptName} said {_gptMessages[_gptMessages.Count - 1]}" });

            // Crear payload
            var payload = new
            {
                model = _claudeModel,
                system = txtSystemClaude.Text, //_claudeSystem,
                messages,
                max_tokens = 500
            };

            var jsonContent = JsonSerializer.Serialize(payload);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            // Realizar la llamada
            var response = await _httpClient.PostAsync(url, content);
            var responseString = await response.Content.ReadAsStringAsync();

            // Parsear la respuesta
            using JsonDocument doc = JsonDocument.Parse(responseString);
            var contentElement = doc.RootElement.GetProperty("content");
            var text = contentElement[0].GetProperty("text").GetString();

            rtbConversation.Text += $"================== CLAUDE: \n{text}\n==================\n\n";

            return text;
        }

        /// <summary>
        /// Llama a la API de Google (Gemini) y obtiene una respuesta.
        /// </summary>
        private async Task<string> CallGeminiAsync()
        {
            // Preparar URL y headers
            var url = $"https://generativelanguage.googleapis.com/v1beta/models/{_geminiModel}:generateContent?key={_googleApiKey}";
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Construir la lista de mensajes para el formato de Gemini
            var contents = new List<object>();

            for (int i = 0; i < Math.Min(_gptMessages.Count, Math.Min(_claudeMessages.Count, _geminiMessages.Count)); i++)
            {
                contents.Add(new
                {
                    role = "user",
                    parts = new[] { new { text = ConstructJoinedUserMsg(checkBoxGPT.Checked, _gptMessages[i], _gptName, checkBoxClaude.Checked, _claudeMessages[i], _claudeName) } }
                });

                contents.Add(new
                {
                    role = "model",
                    parts = new[] { new { text = _geminiMessages[i] } }
                });
            }

            // Agregar el último intercambio
            contents.Add(new
            {
                role = "user",
                parts = new[] { new { text = ConstructJoinedUserMsg(checkBoxGPT.Checked, _gptMessages[_gptMessages.Count - 1], _gptName,
                                                              checkBoxClaude.Checked, _claudeMessages[_claudeMessages.Count - 1], _claudeName) } }
            });

            // Crear payload con system instruction
            var payload = new
            {
                contents = new[] {
                        new { parts = new[] { new { text = txtSystemGemini.Text } } } //_geminiSystem } } }
                    }
            };

            var jsonContent = JsonSerializer.Serialize(payload);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            // Realizar la llamada
            var response = await _httpClient.PostAsync(url, content);
            var responseString = await response.Content.ReadAsStringAsync();

            // Parsear la respuesta
            using JsonDocument doc = JsonDocument.Parse(responseString);
            var candidates = doc.RootElement.GetProperty("candidates");
            var candidate = candidates[0];
            var content0 = candidate.GetProperty("content");
            var parts = content0.GetProperty("parts");
            var text = parts[0].GetProperty("text").GetString();

            rtbConversation.Text += $"================== GEMINI: \n{text}\n==================\n\n";

            return text;
        }

        /// <summary>
        /// Ejecuta una ronda completa de conversación entre los tres agentes.
        /// </summary>
        public async Task RunConversationRoundAsync()
        {
            // Obtener respuesta de GPT
            if (checkBoxGPT.Checked)
            {
                var gptResponse = await CallGptAsync();
                _gptMessages.Add(gptResponse);
            }

            // Obtener respuesta de Claude
            if (checkBoxClaude.Checked)
            {
                var claudeResponse = await CallClaudeAsync();
                _claudeMessages.Add(claudeResponse);
            }

            // Obtener respuesta de Gemini
            if (checkBoxGemini.Checked)
            {
                var geminiResponse = await CallGeminiAsync();
                _geminiMessages.Add(geminiResponse);
            }
        }

        /// <summary>
        /// Ejecuta múltiples rondas de conversación.
        /// </summary>
        /// <param name="rounds">Número de rondas a ejecutar</param>
        public async Task RunMultipleRoundsAsync()
        {
            // Ejecutar el número especificado de rondas
            rtbConversation.Text += ($"=== Ronda {Rondas + 1} ===\n");
            
            await RunConversationRoundAsync();

            Rondas++;
        }

        /// <summary>
        /// Obtiene los mensajes actuales de cada agente.
        /// </summary>
        public (List<string> GptMessages, List<string> ClaudeMessages, List<string> GeminiMessages) GetCurrentMessages()
        {
            return (_gptMessages, _claudeMessages, _geminiMessages);
        }

        /// <summary>
        /// Establece mensajes personalizados para iniciar la conversación.
        /// </summary>
        public void SetInitialMessages(string gptMessage, string claudeMessage, string geminiMessage)
        {
            _gptMessages = new List<string> { gptMessage };
            _claudeMessages = new List<string> { claudeMessage };
            _geminiMessages = new List<string> { geminiMessage };
        }

        /// <summary>
        /// Cambia los nombres de los agentes.
        /// </summary>
        public void SetAgentNames(string gptName, string claudeName, string geminiName)
        {
            _gptName = gptName;
            _claudeName = claudeName;
            _geminiName = geminiName;
        }

        /// <summary>
        /// Cambia las personalidades (instrucciones de sistema) de los agentes.
        /// </summary>
        public void SetAgentPersonalities(string gptSystem, string claudeSystem, string geminiSystem)
        {
            _gptSystem = gptSystem;
            _claudeSystem = claudeSystem;
            _geminiSystem = geminiSystem;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            RunMultipleRoundsAsync();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _gptMessages = new List<string> { txtUserGPT.Text };
            _claudeMessages = new List<string> { txtUserClaude.Text };
            _geminiMessages = new List<string> { txtUserGemini.Text };

            Rondas = 0;

            rtbConversation.Text = "===================== SETEO CORRECTO =====================\n";
        }
    }
}
