using FluentValidation;
using PersonalFinanceManagement.Domain.Balances.Enums;
using PersonalFinanceManagement.Domain.Base.Contracts;
using PersonalFinanceManagement.Domain.Base.Entites;

namespace PersonalFinanceManagement.Domain.Balances.Entities
{
    public class Installment : Entity<int>, IEntityWithSoftDelete
    {
        public int BalanceId { get; set; }
        public int Reference { get; set; }
        public short Number { get; set; }
        public InstallmentStatusEnum Status { get; set; }
        public decimal Value { get; set; }
        public decimal AmountPaid { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual Balance? Balance { get; set; }
        public virtual List<TransactionItem> Items { get; set; } = new();

        public Installment()
        {
            Status = InstallmentStatusEnum.Created;
        }

        public void SetAsDeleted()
        {
            DeletedAt = DateTime.Now;
        }

        protected override void SetValitionRules()
        {
            if (Validator is null)
                return;

            Validator.RuleFor(p => BalanceId)
                .GreaterThan(0)
                .When(p => Balance is null);

            Validator.RuleFor(p => Balance)
                .NotNull()
                .When(p => BalanceId <= 0);

            Validator.RuleFor(p => Reference)
                .GreaterThan(0);

            Validator.RuleFor(p => Value)
                .GreaterThan(0);
        }
    }
}
