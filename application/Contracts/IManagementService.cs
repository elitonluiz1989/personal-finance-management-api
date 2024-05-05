namespace PersonalFinanceManagement.Application.Contracts
{
    public interface IManagementService
    {
        Task<object> List(int reference, int userId, bool isAdmin);
    }
}
