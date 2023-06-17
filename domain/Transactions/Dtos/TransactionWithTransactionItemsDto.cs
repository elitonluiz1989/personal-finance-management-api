namespace PersonalFinanceManagement.Domain.Transactions.Dtos
{
    public record TransactionWithTransactionItemsDto : TransactionDto
    {
        public List<TransactionItemWithInstallmentDto> Items { get; set; } = new();
    }
}
