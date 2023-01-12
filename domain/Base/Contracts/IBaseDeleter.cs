namespace PersonalFinanceManagement.Domain.Base.Contracts
{
    public interface IBaseDeleter<Tkey>
    {
        Task Delete(Tkey id);
    }
}
