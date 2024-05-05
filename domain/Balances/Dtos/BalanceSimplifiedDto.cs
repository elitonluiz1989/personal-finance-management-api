using PersonalFinanceManagement.Domain.Base.Enums;
using PersonalFinanceManagement.Domain.Base.Extensions;

namespace PersonalFinanceManagement.Domain.Balances.Dtos
{
    public record BalanceSimplifiedDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public CommonTypeEnum Type { get; set; }
        public short InstallmentsNumber { get; set; }

        public string TypeDescription => Type.GetDescription();
    }
}
