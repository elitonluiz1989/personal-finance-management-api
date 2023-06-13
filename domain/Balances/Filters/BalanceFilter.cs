using PersonalFinanceManagement.Domain.Balances.Enums;
using PersonalFinanceManagement.Domain.Base.Filters;

namespace PersonalFinanceManagement.Domain.Balances.Filters
{
    public record BalanceFilter : Filter
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public List<BalanceTypeEnum> Types { get; set; } = new List<BalanceTypeEnum>();
        public bool? Financed { get; set; }
        public short? InstallmentsNumber { get; set; }
        public bool? Closed { get; set; }
    }
}
