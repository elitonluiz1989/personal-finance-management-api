using PersonalFinanceManagement.Domain.Balances.Enums;
using PersonalFinanceManagement.Domain.Balances.Extensions;
using PersonalFinanceManagement.Domain.Base.Extensions;

namespace PersonalFinanceManagement.Domain.Balances.Dtos
{
    public record InstallmentDto
    {
        public int Id { get; set; }
        public int BalanceId { get; set; }
        public int Reference { get; set; }
        public short Number { get; set; }
        public InstallmentStatusEnum Status { get; set; }
        public decimal Amount { get; set; }
        public decimal AmountPaid { get; set; }
        public decimal AmountRemaining { get; set; }

        public string ReferenceFormatted => Reference.ToMonthYear();
        public string StatusDescription => Status.GetDescription();
    }
}
