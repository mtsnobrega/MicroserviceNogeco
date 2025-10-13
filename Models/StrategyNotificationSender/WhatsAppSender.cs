using MicroserviceNogeco.Models;
using MicroserviceNogeco.Models.StrategyNotificationSender;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

public class WhatsAppSender : INotificationSender
{
    private readonly HttpClient _httpClient;
    private readonly string _apiToken;
    private readonly string _whatsappBusinessId; // ID do seu número de telefone (710611172145596)
    //private readonly string _baseUrl = "https://graph.facebook.com/v22.0";
    private readonly string _baseUrl = "https://example.com/mock";

    // Você deve injetar as dependências ou configurar as chaves (token e ID)
    public WhatsAppSender(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _apiToken = configuration["WhatsApp:ApiToken"]; // Token longo de Autorização
        _whatsappBusinessId = configuration["WhatsApp:BusinessId"]; // 710611172145596
    }

    public string ChannelName => "WhatsApp";

    public async Task Send(Notification notification, Frellancer recipient)
    {
        // 1. URL de Envio da Meta: BASE_URL/BUSINESS_ID/messages
        string requestUrl = $"{_baseUrl}/{_whatsappBusinessId}/messages";

        // 2. Montar o número de destino do Frellancer (assumindo que seja no modelo)
        // Você precisará adicionar 'NumeroTelefone' à classe Frellancer no formato E.164 (+5511999999999)
        string recipientPhone = recipient.NumeroTelefone;

        // 3. Montar o Corpo da Requisição (JSON)
        // Usamos um objeto C# anônimo para criar o JSON do template 'hello_world'
        var requestBody = new
        {
            messaging_product = "whatsapp",
            to = recipientPhone,
            type = "template",
            template = new
            {
                name = "hello_world", // Nome do seu template aprovado
                language = new { code = "en_US" } // Mantenha ou mude para 'pt_BR'
            }
        };

        // 4. Configurar Autorização (Bearer Token)
        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", _apiToken);

        // 5. Serializar o objeto C# para JSON
        var content = new StringContent(
            JsonSerializer.Serialize(requestBody),
            Encoding.UTF8,
            "application/json"
        );

        // 6. Enviar a requisição POST
        HttpResponseMessage response = await _httpClient.PostAsync(requestUrl, content);

        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine($"[WHATSAPP] SUCESSO! Template 'hello_world' enviado para {recipient.Nome}.");
        }
        else
        {
            string errorContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"[WHATSAPP] ERRO: Falha ao enviar para {recipient.Nome}. Status: {response.StatusCode}. Detalhes: {errorContent}");
        }
    }
}


