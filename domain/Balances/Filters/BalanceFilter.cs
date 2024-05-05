using PersonalFinanceManagement.Domain.Base.Enums;
using PersonalFinanceManagement.Domain.Base.Filters;

namespace PersonalFinanceManagement.Domain.Balances.Filters
{
    public record BalanceFilter : Filter
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public List<CommonTypeEnum> Types { get; set; } = new List<CommonTypeEnum>();
        public bool? Financed { get; set; }
        public short? InstallmentsNumber { get; set; }
        public bool? Closed { get; set; }
    }
}
