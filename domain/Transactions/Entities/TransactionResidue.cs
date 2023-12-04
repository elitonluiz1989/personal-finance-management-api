using FluentValidation;
using PersonalFinanceManagement.Domain.Balances.Entities;
using PersonalFinanceManagement.Domain.Base.Entites;

namespace PersonalFinanceManagement.Domain.Transactions.Entities
{
    public class TransactionResidue : Entity<int>
    {
        public int TransactionItemOriginId { get; set; }
        public int TransactionItemId { get; set; }

        public virtual TransactionItem? TransactionItemOrigin { get; set; }
        public virtual TransactionItem? TransactionItem { get; set; }

        public Transaction? Transaction => TransactionItem?.Transaction;
        public Installment? Installment => TransactionItem?.Installment;
        public Balance? Balance => Installment?.Balance;

        protected override void SetValitionRules()
        {
            if (Validator is null)
                return;

            Validator.RuleFor(p => TransactionItemOrigin)
                .NotNull()
                .When(p => TransactionItemOriginId.Equals(default));

            Validator.RuleFor(p => TransactionItemOriginId)
                .GreaterThan(0)
                .When(p => TransactionItemOrigin is null);

            Validator.RuleFor(p => TransactionItem)
                .NotNull()
                .When(p => TransactionItemId.Equals(default));

            Validator.RuleFor(p => TransactionItemId)
                .GreaterThan(0)
                .When(p => TransactionItem is null);
        }
    }
}
