namespace PersonalFinanceManagement.Domain.Balances.Contracts.Balances
{
    public interface IBalanceDeleter
    {
        Task Delete(int id);
    }
}
