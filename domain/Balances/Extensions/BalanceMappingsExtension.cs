using PersonalFinanceManagement.Domain.Balances.Dtos;
using PersonalFinanceManagement.Domain.Balances.Entities;

namespace PersonalFinanceManagement.Domain.Balances.Extensions
{
    public static class BalanceMappingsExtension
    {
        public static BalanceDto ToBalanceDto(this Balance balance)
        {
            var balanceDto = new BalanceDto()
            {
                Id = balance.Id,
                UserId = balance.UserId,
                Name = balance.Name,
                Type = balance.Type,
                Date = balance.Date,
                Amount = balance.Amount,
                Financed = balance.Financed,
                InstallmentsNumber = balance.InstallmentsNumber,
                Closed = balance.Closed
            };

            if (balance.Installments.Any())
                balanceDto.Installments = balance
                    .Installments
                        .Select(i => i.ToInstallmentWithTransactionItemsDto())
                            .ToList();

            return balanceDto;
        }

        public static BalanceSimplifiedDto ToBalanceSimplifiedDto(this Balance balance)
        {
            return new BalanceSimplifiedDto()
            {
                Id = balance.Id,
                UserId = balance.UserId,
                Name = balance.Name,
                Type = balance.Type,
                InstallmentsNumber = balance.InstallmentsNumber
            };
        }
    }
}
