using FluentValidation;
using PersonalFinanceManagement.Domain.Balances.Enums;
using PersonalFinanceManagement.Domain.Base.Contracts;
using PersonalFinanceManagement.Domain.Base.Entites;

namespace PersonalFinanceManagement.Domain.Balances.Entities
{
    public class Transaction : Entity<int>, IEntityWithSoftDelete
    {
        public int BalanceId { get; set; }
        public TransactionTypeEnum Type { get; set; }
        public short Reference { get; set; }
        public DateTime Date { get; set; }
        public decimal Value { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual Balance? Balance { get; set; }

        public Transaction(
            int balanceId,
            TransactionTypeEnum type,
            short reference,
            DateTime date,
            decimal value
        )
        {
            BalanceId = balanceId;
            Type = type;
            Reference = reference;
            Date = date;
            Value = value;
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

            Validator.RuleFor(p => Type)
                .IsInEnum();

            Validator.RuleFor<int>(p => Reference)
                .GreaterThan(0);

            Validator.RuleFor(p => Value)
                .GreaterThan(0);
        }
    }
}
