using PersonalFinanceManagement.Domain.Balances.Entities;
using PersonalFinanceManagement.Domain.Balances.Extensions;
using PersonalFinanceManagement.Domain.Transactions.Dtos;
using PersonalFinanceManagement.Domain.Transactions.Entities;

namespace PersonalFinanceManagement.Domain.Transactions.Extensions
{
    public static class TransactionItemMappingExtension
    {
        public static TransactionItemWithInstallmentDto ToTransactionItemWithInstallmentDto(this TransactionItem transactionItem)
        {
            var dto = new TransactionItemWithInstallmentDto()
            {
                InstallmentId = transactionItem.InstallmentId,
                Type = transactionItem.Type,
                PartiallyPaid = transactionItem.PartiallyPaid,
                AmountPaid = transactionItem.AmountPaid
            };

            if (transactionItem.Installment is Installment installment)
                dto.Installment = installment.ToInstallmentWithBalanceAndRemainingAmountDto();

            return dto;
        }

        public static TransactionItemWithTransactionDto TransactionItemWithTransactionDto(this TransactionItem transactionItem)
        {
            var dto = new TransactionItemWithTransactionDto()
            {
                TransactionId = transactionItem.TransactionId,
                Type = transactionItem.Type,
                PartiallyPaid = transactionItem.PartiallyPaid,
                AmountPaid = transactionItem.AmountPaid
            };

            if (transactionItem.Transaction is Transaction transaction)
                dto.Transaction = transaction.ToTransactionDto();

            return dto;
        }
    }
}
