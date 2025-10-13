using MicroserviceNogeco.Models;
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
        private readonly INotificationSender _sender;

        public NotificationService(IFrellaRepository userRepository, INotificationSender sender)
        {
            _sender = sender;
        }

        // Envio de notificação baseado em filtro de skill e dia da semana
        public async Task<string> Send_NormalNotification(Notification notification, IFrellancerSearchStrategy searchStrategy)
        {

            // Buscar usuários que atendem ao filtro
            var usuarios = await searchStrategy.Search(notification);

            if (!usuarios.Any())
                return "Nenhum freelancer encontrado para os filtros.";

            // Simulação de envio
            foreach (var u in usuarios)
            {
                Console.WriteLine($"Enviando notificação para {u.Nome} ({u.Email})");
                await _sender.Send(notification, u);
            }

            return $"Notificação enviada para {usuarios.Count} freelancers.";
        }





        public async Task<string> Send_EmergencyNotification(Notification notification, IFrellancerSearchStrategy searchStrategy)
        {
            // Buscar usuários que atendem ao filtro
            var usuarios = await searchStrategy.Search(notification);

            if (!usuarios.Any())
                return "Nenhum freelancer encontrado para os filtros.";

            // Simulação de envio
            foreach (var u in usuarios)
            {
                Console.WriteLine($"Enviando notificação para {u.Nome} ({u.Email})");
            }

            return $"Notificação enviada para {usuarios.Count} freelancers.";
        }


    }
}
