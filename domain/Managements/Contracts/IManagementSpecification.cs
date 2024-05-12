namespace PersonalFinanceManagement.Domain.Managements.Contracts
{
    public interface IManagementSpecification
    {
        Task<object> List(int reference);
    }
}