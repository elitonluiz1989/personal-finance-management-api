using PersonalFinanceManagement.Domain.Transactions.Enums;

namespace PersonalFinanceManagement.Domain.Transactions.Dtos
{
    public class TransactionItemWithTransactionDto
    {
        public int TransactionId { get; set; }
        public TransactionItemTypeEnum Type { get; set; }
        public bool PartiallyPaid { get; set; }
        public decimal AmountPaid { get; set; }

        public TransactionDto? Transaction { get; set; }
    }
}
