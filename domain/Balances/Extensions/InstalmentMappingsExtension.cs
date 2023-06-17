using PersonalFinanceManagement.Domain.Balances.Dtos;
using PersonalFinanceManagement.Domain.Balances.Entities;
using PersonalFinanceManagement.Domain.Transactions.Extensions;

namespace PersonalFinanceManagement.Domain.Balances.Extensions
{
    public static class InstalmentMappingsExtension
    {
        public static InstallmentWithBalanceDto ToInstallmentWithBalanceDto(this Installment installment)
        {
            var dto = new InstallmentWithBalanceDto()
            {
                Id = installment.Id,
                BalanceId = installment.BalanceId,
                Reference = installment.Reference,
                Number = installment.Number,
                Status = installment.Status,
                Amount = installment.Amount
            };

            if (installment.Balance is Balance balance)
                dto.Balance = balance.ToBalanceSimplifiedDto();

            return dto;
        }

        public static InstallmentWithTransactionItemsDto ToInstallmentWithTransactionItemsDto(this Installment installment)
        {
            var dto = new InstallmentWithTransactionItemsDto()
            {
                Id = installment.Id,
                BalanceId = installment.BalanceId,
                Reference = installment.Reference,
                Number = installment.Number,
                Status = installment.Status,
                Amount = installment.Amount
            };

            if (installment.TransactionItems.Any())
                dto.Items = installment
                    .TransactionItems
                        .Select(ti => ti.TransactionItemWithTransactionDto())
                            .ToList();

            return dto;
        }
    }
}
