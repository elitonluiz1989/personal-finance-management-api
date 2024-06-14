using PersonalFinanceManagement.Domain.Managements.Dtos;
using PersonalFinanceManagement.Domain.Managements.Filters;

namespace PersonalFinanceManagement.Domain.Managements.Contracts
{
    public interface IManagementStoreSpecification
    {
        Task<List<ManagementStoreDto>> Get(ManagementStoreFilter filter);
    }
}
