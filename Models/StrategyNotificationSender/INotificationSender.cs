namespace MicroserviceNogeco.Models.StrategyNotificationSender
{
    public interface INotificationSender
    {
        Task Send(Notification notification, Frellancer recipient);
    }
}
