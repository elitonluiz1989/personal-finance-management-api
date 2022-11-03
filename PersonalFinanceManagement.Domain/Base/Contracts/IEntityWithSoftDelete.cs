using PersonalFinanceManagement.Domain.Base.Entites;

namespace PersonalFinanceManagement.Domain.Base.Contracts
{
    public interface IEntityWithSoftDelete
    {
        void SetAsDeleted();
    }
}
