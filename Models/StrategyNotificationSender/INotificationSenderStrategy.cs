namespace MicroserviceNogeco.Models.StrategyNotificationSender
{
    public interface INotificationSenderStrategy
    {
        string ChannelName { get; }
        Task Send(Notification notification, Frellancer recipient);
    }
}
