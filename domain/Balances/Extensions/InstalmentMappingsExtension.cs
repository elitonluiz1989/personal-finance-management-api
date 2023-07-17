using PersonalFinanceManagement.Domain.Balances.Dtos;
using PersonalFinanceManagement.Domain.Balances.Entities;
using PersonalFinanceManagement.Domain.Balances.Enums;
using PersonalFinanceManagement.Domain.Transactions.Extensions;

namespace PersonalFinanceManagement.Domain.Balances.Extensions
{
    public static class InstalmentMappingsExtension
    {
        public static InstallmentWithBalanceDto ToInstallmentWithBalanceAndRemainingAmountDto(this Installment installment)
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

            BalanceHandler(installment, dto);
            RemaingAmountHandler(installment, dto);

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

            TransactionItemsHandler(installment, dto);

            return dto;
        }

        private static void BalanceHandler(Installment installment, InstallmentWithBalanceDto dto)
        {
            if (installment.Balance is not Balance balance)
                return;

            dto.Balance = balance.ToBalanceSimplifiedDto();
        }

        private static void TransactionItemsHandler(Installment installment, InstallmentWithTransactionItemsDto dto)
        {
            if (installment.TransactionItems.Any() is false)
                return;

            dto.Items = installment
                .TransactionItems
                    .Select(ti => ti.TransactionItemWithTransactionDto())
                        .ToList();

            RemaingAmountHandler(installment, dto);
        }

        private static void RemaingAmountHandler<TDto>(Installment installment, TDto dto)
            where TDto : InstallmentDto
        {
            if (
                installment.Status is not InstallmentStatusEnum.PartiallyPaid ||
                installment.TransactionItems.Any() is false
            )
                return;

            dto.AmountPaid = installment.TransactionItems.Sum(ti => ti.AmountPaid);
            dto.AmountRemaining = installment.Amount - dto.AmountPaid;
        }
    }
}
