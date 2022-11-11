using PersonalFinanceManagement.Domain.Balances.Enums;

namespace PersonalFinanceManagement.Domain.Balances.Dtos
{
    public class TransactionDto
    {
        public int Id { get; set; }
        public int BalanceId { get; set; }
        public BalanceDto? Balance { get; set; }
        public TransactionTypeEnum Type { get; set; }
        public short Reference { get; set; }
        public DateTime Date { get; set; }
        public decimal Value { get; set; }
    }
}
