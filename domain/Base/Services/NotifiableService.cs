using FluentValidation.Results;
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

        protected void AddNotification(List<ValidationFailure> messages)
        {
            NotificationService.AddNotification(messages);
        }

        protected void AddNotification(string message)
        {
            NotificationService.AddNotification(message);
        }

        protected bool ValidateNullableObject<T>(T obj)
        {
            if (obj is not null)
                return true;

            Type type = typeof(T);

            AddNotification($"{type.Name} is null");

            return false;
        }
    }
}
