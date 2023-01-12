using FluentValidation.Results;
using PersonalFinanceManagement.Domain.Base.Contracts;

namespace PersonalFinanceManagement.Domain.Base.Services
{
    public class NotificationService : INotificationService
    {
        private readonly List<string> _notifications = new();

        public void AddNotification(List<ValidationFailure> errors)
        {
            foreach (var error in errors)
            {
                AddNotification(error.ErrorMessage);
            }

        }

        public void AddNotification(string notification)
        {
            _notifications.Add(notification);
        }

        public List<string> GetNotifications()
        {
            return _notifications;
        }

        public bool HasNotifications()
        {
            return _notifications.Any();
        }
    }
}
