namespace PersonalFinanceManagement.Domain.Management.Contracts
{
    public interface IManagementSpecification
    {
        Task<object> List(int reference);
    }
}