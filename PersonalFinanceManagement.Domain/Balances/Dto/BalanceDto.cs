using PersonalFinanceManagement.Domain.Balances.Enums;
using PersonalFinanceManagement.Domain.Base.Dtos;

namespace PersonalFinanceManagement.Domain.Balances.Dtos
{
    public record BalanceDto : RecordedDto<int>
    {
        public int UserId { get; set; }
        public BalanceTypeEnum Type { get; set; }
        public BalanceStatusEnum Status { get; set; }
        public decimal Value { get; set; }
        public bool Financed { get; set; }
        public short? NumberOfInstallments { get; set; }
    }
}
