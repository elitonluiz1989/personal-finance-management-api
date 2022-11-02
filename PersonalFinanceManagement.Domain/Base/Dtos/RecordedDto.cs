using PersonalFinanceManagement.Domain.Base.Contracts;

namespace PersonalFinanceManagement.Domain.Base.Dtos
{
    public record RecordedDto<TId>
        where TId : struct
    {
        public TId Id { get; set; }

        public bool IsRecorded => Id.Equals(default(TId)) is false;
    }
}
