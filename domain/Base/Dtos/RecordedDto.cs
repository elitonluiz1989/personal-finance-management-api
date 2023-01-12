using System.Text.Json.Serialization;

namespace PersonalFinanceManagement.Domain.Base.Dtos
{
    public record RecordedDto<TId>
        where TId : struct
    {
        public TId Id { get; set; }

        [JsonIgnore]
        public bool IsRecorded => Id.Equals(default(TId)) is false;
    }
}
