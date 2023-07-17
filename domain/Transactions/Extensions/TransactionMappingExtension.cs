using PersonalFinanceManagement.Domain.Transactions.Dtos;
using PersonalFinanceManagement.Domain.Transactions.Entities;
using PersonalFinanceManagement.Domain.Users.Entities;
using PersonalFinanceManagement.Domain.Users.Extensions;

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

        public static TransactionForListingDto ToTransactionForListingDto(this Transaction transaction)
        {
            var dto = new TransactionForListingDto()
            {
                Id = transaction.Id,
                UserId = transaction.UserId,
                Type = transaction.Type,
                Date = transaction.Date,
                Amount = transaction.Amount
            };

            if (transaction.User is User user)
                dto.User = user.ToUserDto();

            if (transaction.Items.Any())
                dto.Items = transaction
                    .Items
                        .Select(ti => ti.ToTransactionItemWithInstallmentDto())
                            .ToList();

            return dto;
        }
    }
}
