using PersonalFinanceManagement.Domain.Balances.Dtos;
using PersonalFinanceManagement.Domain.Transactions.Enums;

namespace PersonalFinanceManagement.Domain.Transactions.Dtos
{
    public class TransactionItemWithInstallmentDto
    {
        public int InstallmentId { get; set; }
        public TransactionItemTypeEnum Type { get; set; }
        public bool PartiallyPaid { get; set; }
        public decimal AmountPaid { get; set; }

        public InstallmentWithBalanceDto? Installment { get; set; }
    }
}
