using FluentValidation.Results;

namespace PersonalFinanceManagement.Domain.Base.Contracts
{
    public interface INotificationService
    {
        void AddNotification(List<ValidationFailure> errors);
        public void AddNotification(string notification);
        List<string> GetNotifications();
        bool HasNotifications();
    }
}
