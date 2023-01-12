using PersonalFinanceManagement.Domain.Balances.Enums;

namespace PersonalFinanceManagement.Domain.Balances.Filters
{
    public record BalanceFilter
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public List<BalanceTypeEnum> Types { get; set; } = new List<BalanceTypeEnum>();
        public bool? Financed { get; set; }
        public short? InstallmentsNumber { get; set; }
        public bool? Closed { get; set; }
    }
}
