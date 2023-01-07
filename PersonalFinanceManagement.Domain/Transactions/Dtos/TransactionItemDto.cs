using PersonalFinanceManagement.Domain.Balances.Dtos;

namespace PersonalFinanceManagement.Domain.Transactions.Dtos
{
    public class TransactionItemDto
    {
        public int TransactionId { get; set; }
        public TransactionDto? Transaction { get; set; }
        public int InstallmentId { get; set; }
        public InstallmentDto? Installment { get; set; }
        public bool PartiallyPaid { get; set; }
    }
}
