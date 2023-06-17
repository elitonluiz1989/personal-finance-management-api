namespace PersonalFinanceManagement.Domain.Balances.Dtos
{
    public record BalanceDto : BalanceSimplifiedDto
    {
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public bool Financed { get; set; }
        public bool Closed { get; set; }
        public List<InstallmentWithTransactionItemsDto> Installments { get; set; } = new();
    }
}
