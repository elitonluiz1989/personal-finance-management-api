using PersonalFinanceManagement.Domain.Base.Enums;

namespace PersonalFinanceManagement.Domain.Management.Dtos
{
    public class ManagementTotalDto
    {
        public CommonTypeEnum Type { get; private set; }
        public decimal Value { get; init; }

        public ManagementTotalDto(List<ManagementItemDto> items)
        {
            Value = Calculate(items);
        }

        private decimal Calculate(List<ManagementItemDto> items)
        {
            var debtAmount = items
                .Where(p => p.Type == CommonTypeEnum.Debt)
                .Sum(p => p.Amount);
            var creditAmount = items
                .Where(p => p.Type == CommonTypeEnum.Credit)
                .Sum(p => p.Amount);

            Type = debtAmount > creditAmount ? CommonTypeEnum.Debt : CommonTypeEnum.Credit;

            var total = debtAmount - creditAmount;

            return total;

        }
    }
}
