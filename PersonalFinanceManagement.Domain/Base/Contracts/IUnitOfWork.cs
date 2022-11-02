namespace PersonalFinanceManagement.Domain.Base.Contracts
{
    public interface IUnitOfWork
    {
        void Commit();
    }
}
