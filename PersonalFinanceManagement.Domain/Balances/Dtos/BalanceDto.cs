using PersonalFinanceManagement.Domain.Balances.Enums;
using PersonalFinanceManagement.Domain.Base.Dtos;

namespace PersonalFinanceManagement.Domain.Balances.Dtos
{
    public record BalanceDto : RecordedDto<int>
    {
        public int UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public BalanceTypeEnum Type { get; set; }
        public BalanceStatusEnum Status { get; set; }
        public DateTime Date { get; set; }
        public decimal Value { get; set; }
        public bool Financed { get; set; }
        public short InstallmentsNumber { get; set; }
        public List<InstallmentDto> Installments { get; set; } = new();

        public short InstallmentsNumberValidate => Financed ? InstallmentsNumber : (short)1;
    }
}
