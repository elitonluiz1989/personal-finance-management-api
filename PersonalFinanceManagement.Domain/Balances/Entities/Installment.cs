using FluentValidation;
using PersonalFinanceManagement.Domain.Base.Contracts;
using PersonalFinanceManagement.Domain.Base.Entites;

namespace PersonalFinanceManagement.Domain.Balances.Entities
{
    public class Installment : Entity<int>, IEntityWithSoftDelete
    {
        public int BalanceId { get; set; }
        public short Reference { get; set; }
        public short Number { get; set; }
        public decimal Value { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual Balance? Balance { get; set; }

        public Installment()
        {
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
                .GreaterThan(0);

            Validator.RuleFor<int>(p => Reference)
                .GreaterThan(0);

            Validator.RuleFor(p => Value)
                .GreaterThan(0);
        }
    }
}
