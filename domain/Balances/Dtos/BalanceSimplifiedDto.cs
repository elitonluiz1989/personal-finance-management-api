using PersonalFinanceManagement.Domain.Balances.Enums;

namespace PersonalFinanceManagement.Domain.Balances.Dtos
{
    public record BalanceSimplifiedDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public BalanceTypeEnum Type { get; set; }
        public string TypeDescription { get; set; } = string.Empty;
        public short InstallmentsNumber { get; set; }
    }
}
