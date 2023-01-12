using PersonalFinanceManagement.Domain.Balances.Dtos;
using PersonalFinanceManagement.Domain.Transactions.Enums;

namespace PersonalFinanceManagement.Domain.Transactions.Dtos
{
    public class TransactionItemDto
    {
        public int TransactionId { get; set; }
        public int InstallmentId { get; set; }
        public TransactionItemTypeEnum Type { get; set; }
        public bool PartiallyPaid { get; set; }
        public decimal AmountPaid { get; set; }

        public TransactionDto? Transaction { get; set; }
        public InstallmentDto? Installment { get; set; }
    }
}
