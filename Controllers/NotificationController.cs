using MicroserviceNogeco.Models;
using MicroserviceNogeco.Models.StrategySearchFrellancer;
using MicroserviceNogeco.Service;

// Um controle é onde são definidas as portas de entrada e saida da API
// Basicamente ela recebe as requisições HTTP
// Valida dados e chamas serviços adequados
namespace MicroserviceNogeco.Controllers
{
    public static class NotificationController 
    {
        public static void MapRoutesNotification(this WebApplication app)
        {
            var rout_notification = app.MapGroup("/notification");

            //Post - Coletar dados de um evento e enviar notificação
            rout_notification.MapPost("/", async ( Notification notification, NotificationService service, NormalSearchStrategy normalStrategy) =>
            {
                var mensagemErro = notification.Validate();

                if (!string.IsNullOrEmpty(mensagemErro))
                {
                    return Results.BadRequest(mensagemErro);
                }

                var resultado = await service.Send_NormalNotification(notification, normalStrategy);
                return Results.Ok(resultado);
            })
                .WithDescription("Este endpoint recebe os dados do evento e envia uma notificação para os usuários com base no filtro.");



            //Post - Coletar dados para um envio de emergencia
            rout_notification.MapPost("/emergency", async (Notification notification, NotificationService service, EmergencySearchStrategy emergencyStrategy) =>
            {
                var mensagemErro = notification.Validate();

                if (!string.IsNullOrEmpty(mensagemErro))
                {
                    return Results.BadRequest(mensagemErro);
                }

                var resultado = await service.Send_EmergencyNotification(notification, emergencyStrategy);
                return Results.Ok(resultado);
            });



            //Post - Coletar dados do evento cancelado e avisar os frellancers do cancelamento
            rout_notification.MapPost("/canceled_event", async () =>
            {
                //Criar a entidade do evento
                /*
                var mensagemErro = notification.Validate();

                if (!string.IsNullOrEmpty(mensagemErro))
                {
                    return Results.BadRequest(mensagemErro);
                }

                var resultado = await service.Send_EmergencyNotification(notification);
                return Results.Ok(resultado);
                */
            });
            
        }
    }
}
