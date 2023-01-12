using PersonalFinanceManagement.Domain.Base.Dtos;

namespace PersonalFinanceManagement.Domain.Base.Contracts
{
    public interface IBaseStore<TKey>
        where TKey : struct
    {
        Task Store(RecordedDto<TKey> dto);
    }
}
