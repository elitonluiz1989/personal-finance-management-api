using PersonalFinanceManagement.Domain.Base.Enums;

namespace PersonalFinanceManagement.Domain.Managements.Dtos
{
    public class ManagementTotalDto
    {
        public CommonTypeEnum Type { get; private set; }
        public decimal Value { get; private set; }

        public ManagementTotalDto(List<ManagementItemDto> items, ManagementRemainingValueDto? remainingValue)
        {
            DataHandler(items, remainingValue);
        }

        private void DataHandler(List<ManagementItemDto> items, ManagementRemainingValueDto? remainingValue)
        {
            decimal total = CaculateTotal(items, remainingValue);

            Type = total > 0 ? CommonTypeEnum.Debt : CommonTypeEnum.Credit;
            Value = Math.Abs(total);
        }

        private static decimal CaculateTotal(List<ManagementItemDto> items, ManagementRemainingValueDto? remainingValue)
        {
            decimal debtAmount = items
                .Where(p => p.Type == CommonTypeEnum.Debt)
                .Sum(p => p.Amount);
            decimal creditAmount = items
                .Where(p => p.Type == CommonTypeEnum.Credit)
                .Sum(p => p.Amount);

            if (remainingValue is not null)
            {
                if (remainingValue.IsCredit)
                    creditAmount += remainingValue.Value;
                else
                    debtAmount += remainingValue.Value;
            }

            decimal total = debtAmount - creditAmount;
            return total;
        }
    }
}
