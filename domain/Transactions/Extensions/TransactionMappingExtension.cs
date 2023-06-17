using PersonalFinanceManagement.Domain.Transactions.Dtos;
using PersonalFinanceManagement.Domain.Transactions.Entities;

namespace PersonalFinanceManagement.Domain.Transactions.Extensions
{
    public static class TransactionMappingExtension
    {
        public static TransactionDto ToTransactionDto(this Transaction transaction)
        {
            return new TransactionDto()
            {
                Id = transaction.Id,
                UserId = transaction.UserId,
                Type = transaction.Type,
                Date = transaction.Date,
                Amount = transaction.Amount
            };
        }

        public static TransactionWithTransactionItemsDto ToTransactionWithTransactionItemsDto(this Transaction transaction)
        {
            var dto = new TransactionWithTransactionItemsDto()
            {
                Id = transaction.Id,
                UserId = transaction.UserId,
                Type = transaction.Type,
                Date = transaction.Date,
                Amount = transaction.Amount
            };

            if (transaction.Items.Any())
                dto.Items = transaction
                    .Items
                        .Select(ti => ti.ToTransactionItemWithInstallmentDto())
                            .ToList();

            return dto;
        }
    }
}
