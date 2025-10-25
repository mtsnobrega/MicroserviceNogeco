using MicroserviceNogeco.Models;
using MicroserviceNogeco.Models.StrategyNotificationSender;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

public class WhatsAppSenderStrategy : INotificationSenderStrategy
{
    private readonly HttpClient _httpClient;
    private readonly string _apiToken;
    private readonly string _whatsappBusinessId; 
    private readonly string _baseUrl = "https://graph.facebook.com/v22.0";
    
    public WhatsAppSenderStrategy(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _apiToken = configuration["WhatsApp:ApiToken"]; // Token longo de Autorização
        _whatsappBusinessId = configuration["WhatsApp:BusinessId"]; // 710611172145596
    }
    
    public string ChannelName => "WhatsApp";

    public async Task Send(Notification notification, Frellancer recipient)
    {
        string requestUrl = $"{_baseUrl}/{_whatsappBusinessId}/messages";

        string recipientPhone = recipient.NumeroTelefone;

        string GetSafeText(string? value, string defaultValue)
        {
            // Se a string for nula ou vazia ou apenas espaços em branco, retorne o valor padrão
            return string.IsNullOrWhiteSpace(value) ? defaultValue : value.Trim();
        }

        //Ajustando a skills para adicionar como uam string
        var skillsText = notification.Skills.Any()
        ? string.Join(", ", notification.Skills)
        : "-";
        //new { type = "image", image = new { link = "https://img.quizur.com/f/img5d7a70033db291.35505361.jpg?lastEdited=1568305161" } }
        // Corpo da requsisção json para whatsapp business 
        var requestBody = new
        {
            messaging_product = "whatsapp",
            to = recipientPhone,
            type = "template",
            template = new
            {
                name = "teste_aceite",
                language = new { code = "pt_BR" },
                components = new object[]
        {
            new
            {
                type = "header",
                parameters = new object[]
                {
                    new { type = "image", image = new { link = "http://54.227.41.97/wp-content/uploads/2025/10/unnamed.jpg" } }
                }
            },
            new
            {
                type = "body",
                parameters = new object[]
                {
                    new { type = "text", parameter_name = "nome_frelancer", text = recipient.Nome },
                    new { type = "text", parameter_name = "nome_empresa", text = notification.NomeEmpresa },
                    new { type = "text", parameter_name = "nome_evento", text = notification.NomeEvento },
                    new { type = "text", parameter_name = "descricao_evento", text = notification.DescricaoEvento },
                    new { type = "text", parameter_name = "data_evento", text = notification.DataEvento },
                    new { type = "text", parameter_name = "hora_evento", text = notification.HoraEvento },
                    new { type = "text", parameter_name = "local_evento", text = notification.LocalEvento },
                    new { type = "text", parameter_name = "valor_evento", text = notification.ValorPago },
                    new { type = "text", parameter_name = "hab_evento", text = string.Join(", ", notification.Skills) }
                }
            }
        }
            }
        };
        //var json = JsonSerializer.Serialize(requestBody, new JsonSerializerOptions { WriteIndented = true });
        //Console.WriteLine(json);
        //Console.WriteLine("Skills extraídas: " + string.Join(", ", notification.Skills));

        // serializer com casing minúsculo
        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };

        // 4. Configurar Autorização (Bearer Token)
        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", _apiToken);

        var content = new StringContent(
            JsonSerializer.Serialize(requestBody, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }),
            Encoding.UTF8,
            "application/json"
        );



        // 6. Enviar a requisição POST
        try
        {
            HttpResponseMessage response = await _httpClient.PostAsync(requestUrl, content);
            Console.WriteLine($"[WHATSAPP] SUCESSO! mensagem enviado para {recipient.Nome}.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erro ao enviar zap: " + ex.Message);
        }

        /*
        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine($"[WHATSAPP] SUCESSO! mensagem enviado para {recipient.Nome}.");
        }
        else
        {
            string errorContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"[WHATSAPP] ERRO: Falha ao enviar para {recipient.Nome}. Status: {response.StatusCode}. Detalhes: {errorContent}");
        }
        */
    }
}


