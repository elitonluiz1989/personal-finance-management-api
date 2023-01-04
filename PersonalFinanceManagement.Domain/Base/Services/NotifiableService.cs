using PersonalFinanceManagement.Domain.Base.Contracts;

namespace PersonalFinanceManagement.Domain.Base.Services
{
    public abstract class NotifiableService
    {
        protected bool HasNotifications => NotificationService?.HasNotifications() ?? false;
        protected readonly INotificationService NotificationService;

        public NotifiableService(INotificationService notificationService)
        {
            NotificationService = notificationService;
        }

        protected void AddNotification(string message)
        {
            NotificationService.AddNotification(message);
        }
    }
}
