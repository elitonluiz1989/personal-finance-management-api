using PersonalFinanceManagement.Domain.Managements.Dtos;

namespace PersonalFinanceManagement.Domain.Managements.Contracts
{
    public interface IManagementStore
    {
        Task Store(ManagementStoreDto dto);
    }
}
