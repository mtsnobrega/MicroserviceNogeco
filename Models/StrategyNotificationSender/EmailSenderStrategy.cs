using System.Net;
using System.Net.Mail;
using System.Net.Http.Headers;
using MicroserviceNogeco.Models;
using MicroserviceNogeco.Models.StrategyNotificationSender;

namespace MicroserviceNogeco.Models.StrategyNotificationSender
{
    public class EmailSenderStrategy : INotificationSenderStrategy
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;
        public string ChannelName => "email";

        public EmailSenderStrategy(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }
        public async Task Send(Notification notification, Frellancer recipient)
        {
            MailMessage mailMessage = new MailMessage();

            try
            {
                var smtpClient = new SmtpClient("smtp.gmail.com", 587);
                smtpClient.EnableSsl = true;
                smtpClient.Timeout = 60 * 60;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new System.Net.NetworkCredential("msnobrega@aluno.riobrancofac.edu.br", "");
                mailMessage.From = new MailAddress("msnobrega@aluno.riobrancofac.edu.br", "Teste001");
                mailMessage.Subject = "Notificação de Evento - Nogeco";
                mailMessage.Body = $"Olá {recipient.Nome},\n\n" +
                    $"Você tem uma nova notificação de evento:\n\n" +
                    $"Empresa: {notification.NomeEmpresa}\n" +
                    $"Valor Pago: {notification.ValorPago}\n" +
                    $"Nome do Evento: {notification.NomeEvento}\n" +
                    $"Descrição do Evento: {notification.DescricaoEvento}\n" +
                    $"Data do Evento: {notification.DataEvento}\n" +
                    $"Local do Evento: {notification.LocalEvento}\n" +
                    $"Hora do Evento: {notification.HoraEvento}\n" +
                    $"Skills Requeridas: {(notification.Skills.Any() ? string.Join(", ", notification.Skills) : "-")}\n\n" +
                    "Por favor, verifique os detalhes e confirme sua participação.\n\n" +
                    "Atenciosamente,\nEquipe Nogeco";
                mailMessage.IsBodyHtml = true;
                mailMessage.Priority = MailPriority.Normal;
                mailMessage.To.Add(recipient.Email);

                await smtpClient.SendMailAsync(mailMessage);
                Console.WriteLine("[ENVIANDO EMAIL] ");
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao enviar email: " + ex.Message);
            }
        }
     
    }
}
