using PersonalFinanceManagement.Domain.Balances.Enums;

namespace PersonalFinanceManagement.Domain.Balances.Dtos
{
    public record InstallmentDto
    {
        public int Id { get; set; }
        public int BalanceId { get; set; }
        public BalanceDto? Balance { get; set; }
        public int Reference { get; set; }
        public short Number { get; set; }
        public InstallmentStatusEnum Status { get; set; }
        public decimal Value { get; set; }
        public decimal AmountPaid { get; set; }
        public List<TransactionItemDto> Items { get; set; } = new();
    }
}
