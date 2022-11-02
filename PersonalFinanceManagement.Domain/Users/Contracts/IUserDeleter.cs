namespace PersonalFinanceManagement.Domain.Users.Contracts
{
    public interface IUserDeleter
    {
        Task Delete(int id);
    }
}
