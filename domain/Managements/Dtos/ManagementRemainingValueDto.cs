using PersonalFinanceManagement.Domain.Balances.Extensions;
using PersonalFinanceManagement.Domain.Base.Enums;
using PersonalFinanceManagement.Domain.Base.Extensions;
using System.Text.Json.Serialization;

namespace PersonalFinanceManagement.Domain.Managements.Dtos
{
    public class ManagementRemainingValueDto
    {
        public string Description { get; private set; } = string.Empty;
        public CommonTypeEnum Type { get; private set; }
        public decimal Value { get; private set; }

        [JsonIgnore]
        public bool IsCredit => Type == CommonTypeEnum.Credit;

        public ManagementRemainingValueDto(IEnumerable<ManagementRemainingValueResult> items, int reference)
        {
            DataHandler(items, reference);
        }

        public void DataHandler(IEnumerable<ManagementRemainingValueResult> items, int reference)
        {
            decimal value = CalculateValue(items);
            string previousReference = reference.ToDateTime().AddMonths(-1).ToReference().ToMonthYear();

            Description = $"Remaining value of {previousReference}";
            Value = Math.Abs(value);
            Type = value > 0 ? CommonTypeEnum.Credit : CommonTypeEnum.Debt;
        }

        private static decimal CalculateValue(IEnumerable<ManagementRemainingValueResult> items)
        {
            decimal creditValue = GetValuesByType(items, CommonTypeEnum.Credit);
            decimal debitValue = GetValuesByType(items, CommonTypeEnum.Debt);

            if (creditValue == 0)
            {
                return debitValue;
            }

            if (debitValue == 0)
            {
                return creditValue;
            }

            return creditValue - debitValue;
        }

        private static decimal GetValuesByType(
            IEnumerable<ManagementRemainingValueResult> items,
            CommonTypeEnum type
        )
        {
            return items
                .Where(p => p.Type == type)
                .Sum(p => p.Amount - p.AmountPaid.GetValueOrDefault());
        }
    }
}
