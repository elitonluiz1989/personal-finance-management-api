using PersonalFinanceManagement.Domain.Base.Contracts;
using PersonalFinanceManagement.Domain.Base.Services;
using PersonalFinanceManagement.Domain.Users.Contracts;
using PersonalFinanceManagement.Domain.Users.Entities;

namespace PersonalFinanceManagement.Domain.Users.Services
{
    public class UserDeleter : BaseDeleter<User, int, IUserRepository>, IUserDeleter
    {
        public UserDeleter(
            INotificationService notificationService,
            IUserRepository repository
        )
            : base(notificationService, repository)
        {
        }
    }
}
