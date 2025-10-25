using MicroserviceNogeco.Models;
using MicroserviceNogeco.Models.FactoryNotificationSender;
using MicroserviceNogeco.Models.StrategyNotificationSender;
using MicroserviceNogeco.Models.StrategySearchFrellancer;
using MicroserviceNogeco.Repository;
using System.Reflection;
// Service contém as regras de negocio, fazendo validações e tratamentos dos dados
// Basicamente o controller cuida da logica simples, recebe os dados e chama o SERVICE para aplicar a logica em determinados contextos

namespace MicroserviceNogeco.Service
{
    public class NotificationService
    {
        //Injeção de dependencia para que
        //private readonly IFrellaRepository _userRepository;
        private readonly NotificationFactory _factory;
        public NotificationService(NotificationFactory factory)
        {
            _factory = factory;
        }

        // Envio de notificação baseado em filtro de skill e dia da semana
        public async Task<string> Send_NormalNotification(Notification notification, IFrellancerSearchStrategy searchStrategy)
        {
            
            var usuarios = await searchStrategy.Search(notification);

            var senders = _factory.GetAllSenders(); // e-mail + whatsapp, etc.

            if (!usuarios.Any())
                return "Nenhum freelancer encontrado para os filtros.";

            foreach (var u in usuarios)
            {
                Console.WriteLine($"Enviando notificação para {u.Nome} ({u.Email})");

                // Enviar por todos os canais
                foreach (var sender in senders)
                {
                    try
                    {
                        await sender.Send(notification, u);
                        Console.WriteLine($"Notificação enviada via {sender.GetType().Name} para {u.Nome}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Erro ao enviar via {sender.GetType().Name} para {u.Nome}: {ex.Message}");
                    }
                }
            }

            return $"Notificação enviada para {usuarios.Count} freelancers em todos os canais disponíveis.";
        }


        public async Task<string> Send_EmergencyNotification(Notification notification, IFrellancerSearchStrategy searchStrategy)
        {
            // Buscar usuários que atendem ao filtro
            var usuarios = await searchStrategy.Search(notification);

            if (!usuarios.Any())
                return "Nenhum freelancer encontrado para os filtros.";

            
            foreach (var u in usuarios)
            {
                Console.WriteLine($"Enviando notificação para {u.Nome} ({u.Email})");
            }

            return $"Notificação enviada para {usuarios.Count} freelancers.";
        }


    }
}
