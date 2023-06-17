using PersonalFinanceManagement.Domain.Transactions.Dtos;

namespace PersonalFinanceManagement.Domain.Balances.Dtos
{
    public record InstallmentWithTransactionItemsDto : InstallmentDto
    {
        public List<TransactionItemWithTransactionDto> Items { get; set; } = new();
    }
}
