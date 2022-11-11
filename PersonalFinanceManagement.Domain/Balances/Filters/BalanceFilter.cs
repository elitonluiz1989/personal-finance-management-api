using PersonalFinanceManagement.Domain.Balances.Enums;

namespace PersonalFinanceManagement.Domain.Balances.Filters
{
    public record BalanceFilter
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public List<BalanceTypeEnum> Types { get; set; } = new List<BalanceTypeEnum>();
        public List<BalanceStatusEnum> Status { get; set; } = new List<BalanceStatusEnum>();
        public bool? Financed { get; set; }
        public short? NumberOfInstallments { get; set; }
    }
}
