using PersonalFinanceManagement.Domain.Balances.Enums;
using PersonalFinanceManagement.Domain.Base.Extensions;

namespace PersonalFinanceManagement.Domain.Balances.Dtos
{
    public record BalanceSimplifiedDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public BalanceTypeEnum Type { get; set; }
        public short InstallmentsNumber { get; set; }

        public string TypeDescription => Type.GetDescription();
    }
}
