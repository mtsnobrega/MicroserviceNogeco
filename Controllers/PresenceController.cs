using MicroserviceNogeco.Models;
using MicroserviceNogeco.Service;
using Microsoft.AspNetCore.Http;

namespace MicroserviceNogeco.Controllers
{
    public static class PresenceController
    {
        public static void MapRoutesPresence(this WebApplication app)
        {
            var rout_notification = app.MapGroup("/presence").WithTags("Presence_Control");


            //Post - Coletar dados de um frellancer que aceitou o trabalho
            rout_notification.MapPost("/accept-work", async (Frellancer frella_accept) =>
            {
                var mensagemErro = frella_accept.ValidateFrella();

                if (!string.IsNullOrEmpty(mensagemErro))
                {
                    return Results.BadRequest(mensagemErro);
                }

                //var resultado = await service.Send_NormalNotification(notification);
                return Results.Ok("ola mundo");
            });
        }
    }
}
