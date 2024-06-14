using PersonalFinanceManagement.Domain.Managements.Dtos;

namespace PersonalFinanceManagement.Domain.Managements.Contracts
{
    public interface IManagementSpecification
    {
        Task<List<ManagementDto>> Get(int reference);
    }
}