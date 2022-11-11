namespace PersonalFinanceManagement.Domain.Balances.Contracts
{
    public interface IBalanceDeleter
    {
        Task Delete(int id);
    }
}
