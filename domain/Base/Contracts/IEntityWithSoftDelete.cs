namespace PersonalFinanceManagement.Domain.Base.Contracts
{
    public interface IEntityWithSoftDelete
    {
        DateTime? DeletedAt { get; set; }

        void SetAsDeleted();
    }
}
