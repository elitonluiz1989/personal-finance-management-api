using PersonalFinanceManagement.Domain.Users.Dtos;

namespace PersonalFinanceManagement.Domain.Users.Contracts
{
    public interface IUserStore
    {
        Task Store(UserStoreDto dto);
    }
}
