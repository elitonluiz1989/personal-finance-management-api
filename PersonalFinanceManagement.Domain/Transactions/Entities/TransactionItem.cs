using FluentValidation;
using PersonalFinanceManagement.Domain.Balances.Entities;
using PersonalFinanceManagement.Domain.Base.Entites;
using PersonalFinanceManagement.Domain.Transactions.Enums;

namespace PersonalFinanceManagement.Domain.Transactions.Entities
{
    public class TransactionItem : Entity<int>
    {
        public int TransactionId { get; set; }
        public int InstallmentId { get; set; }
        public TransactionItemTypeEnum Type { get; set; }
        public bool PartiallyPaid { get; set; }
        public decimal AmountPaid { get; set; }

        public virtual Transaction? Transaction { get; set; }
        public virtual Installment? Installment { get; set; }

        protected override void SetValitionRules()
        {
            if (Validator is null)
                return;

            Validator.RuleFor(p => Transaction)
                .NotNull()
                .When(p => TransactionId.Equals(default));

            Validator.RuleFor(p => TransactionId)
                .GreaterThan(0)
                .When(p => Transaction is null);

            Validator.RuleFor(p => InstallmentId)
                .GreaterThan(0);
        }
    }
}
