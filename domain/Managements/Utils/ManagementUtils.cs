using PersonalFinanceManagement.Domain.Balances.Extensions;
using PersonalFinanceManagement.Domain.Base.Enums;
using PersonalFinanceManagement.Domain.Base.Extensions;
using PersonalFinanceManagement.Domain.Managements.Dtos;

namespace PersonalFinanceManagement.Domain.Managements.Utils
{
    internal static class ManagementUtils
    {
        internal static int GetPreviousReference(int reference)
        {
            try
            {
                var previousReference = reference
                    .ToDateTime()
                    .AddMonths(-1)
                    .ToReference();

                return previousReference;
            }
            catch
            {
                return 0;
            }
        }

        internal static (CommonTypeEnum Type, decimal Amount) CalculateReferenceAmount(List<ManagementItemAmountDto> items)
        {
            decimal debtAmount = items
                .Where(p => p.Type == CommonTypeEnum.Debt)
                .Sum(p => p.Amount);
            decimal creditAmount = items
                .Where(p => p.Type == CommonTypeEnum.Credit)
                .Sum(p => p.Amount);
            decimal amount = debtAmount - creditAmount;
            var type = debtAmount > creditAmount
                ? CommonTypeEnum.Debt
                : CommonTypeEnum.Credit;

            return (type, amount);
        }
    }
}
