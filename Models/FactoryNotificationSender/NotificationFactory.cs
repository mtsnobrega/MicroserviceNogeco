using MicroserviceNogeco.Models.StrategyNotificationSender;

namespace MicroserviceNogeco.Models.FactoryNotificationSender
{
    /// <summary>
    /// Representa uma Simple Factory para gerenciar e fornecer
    /// todas as estratégias de envio de notificação registradas no sistema.
    /// Ela é impulsionada por injeção de dependencia, não criando as instâncias diretamente.
    /// Container de DI faz a coleta de todas as implementações e as injeta na Factory como uma coleção.
    /// 
    /// NOTA SOBRE O LIFETIME:
    /// Como ela foi registrada 'builder.Services.AddScoped<NotificationFactory>()'.
    /// Isso garante que ela seja criada apenas UMA VEZ por requisição HTTP,
    /// o que é ideal para manter a consistência dentro do escopo de uma única transação/operação.
    /// </summary>

    public class NotificationFactory
    {
        // Armazena a coleção de todas as estratégias de envio.(whats e email)
        private readonly IEnumerable<INotificationSenderStrategy> _strategies;

        /// <summary>
        /// Construtor que recebe a coleção de estratégias via Injeção de Dependência.
        /// O Container de DI é responsável por encontrar todas
        /// as classes que implementam 'INotificationSenderStrategy' e passar a coleção para cá.
        /// </summary>
        public NotificationFactory(IEnumerable<INotificationSenderStrategy> strategies)
        {
            _strategies = strategies;
        }

        /// <summary>
        /// Retorna a lista completa de todas as estratégias de envio de notificação.
        /// 
        /// O componente de serviço (ex: NotificationService) que usa este método
        /// para iterar sobre o resultado para enviar a notificação por todos
        /// os canais de forma desacoplada.
        /// 
        /// Dessa forma para adicionar uma nova estrategia de envio, basta implementar a interface
        /// e ela ja sera adiciona a lista gerada pela factory
        /// </summary>
        public IEnumerable<INotificationSenderStrategy> GetAllSenders()
        {
            return _strategies;
        }
    
    }
}
