using PersonalFinanceManagement.Domain.Balances.Dtos;
using PersonalFinanceManagement.Domain.Balances.Entities;

namespace PersonalFinanceManagement.Domain.Balances.Extensions
{
    public static class InstalmentMappingsExtension
    {
        public static InstallmentDto ToInstallmentDto(this Installment installment)
        {
            return new InstallmentDto()
            {
                Id = installment.Id,
                BalanceId = installment.BalanceId,
                UserId = installment.Balance?.UserId ?? default,
                Reference = installment.Reference,
                Number = installment.Number,
                Status = installment.Status,
                Amount = installment.Amount
            };
        }
    }
}
