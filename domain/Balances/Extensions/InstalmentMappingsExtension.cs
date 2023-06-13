using PersonalFinanceManagement.Domain.Balances.Dtos;
using PersonalFinanceManagement.Domain.Balances.Entities;
using PersonalFinanceManagement.Domain.Base.Extensions;

namespace PersonalFinanceManagement.Domain.Balances.Extensions
{
    public static class InstalmentMappingsExtension
    {
        public static InstallmentDto ToInstallmentDto(this Installment installment)
        {
            var dto = new InstallmentDto()
            {
                Id = installment.Id,
                BalanceId = installment.BalanceId,
                Reference = installment.Reference,
                ReferenceFormatted = installment.Reference.ToMonthYear(),
                Number = installment.Number,
                Status = installment.Status,
                StatusDescription = installment.Status.GetDescrition(),
                Amount = installment.Amount
            };

            if (installment.Balance is Balance balance)
            {
                dto.Balance = new BalanceSimplifiedDto()
                {
                    Id = balance.Id,
                    UserId = balance.UserId,
                    Name = balance.Name,
                    Type = balance.Type,
                    TypeDescription = balance.Type.GetDescrition(),
                    InstallmentsNumber = balance.InstallmentsNumber
                };
            }

            return dto;
        }
    }
}
