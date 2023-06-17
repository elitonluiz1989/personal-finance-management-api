using PersonalFinanceManagement.Domain.Balances.Enums;

namespace PersonalFinanceManagement.Domain.Balances.Dtos
{
    public record InstallmentWithBalanceDto : InstallmentDto
    {
        public BalanceSimplifiedDto? Balance { get; set; }
    }
}
