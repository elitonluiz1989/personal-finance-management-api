
using PersonalFinanceManagement.Domain.Base.Dtos;
using PersonalFinanceManagement.Domain.Base.Enums;
using System.Text.Json.Serialization;

namespace PersonalFinanceManagement.Domain.Balances.Dtos
{
    public record BalanceStoreDto : RecordedDto<int>
    {
        public int UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public CommonTypeEnum Type { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public bool Financed { get; set; }
        public short InstallmentsNumber { get; set; }
        public  bool Residue { get; set; }

        [JsonIgnore]
        public short InstallmentsNumberValidate => Financed ? InstallmentsNumber : (short)1;
    }
}
