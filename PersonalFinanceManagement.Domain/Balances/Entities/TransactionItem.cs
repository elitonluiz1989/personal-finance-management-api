using FluentValidation;
using PersonalFinanceManagement.Domain.Base.Entites;

namespace PersonalFinanceManagement.Domain.Balances.Entities
{
    public class TransactionItem : Entity<int>
    {
        public int TransactionId { get; set; }
        public int InstallmentId { get; set; } 
        public bool PartiallyPaid { get; set; }

        public virtual Transaction? Transaction { get; set; }
        public virtual Installment? Installment { get; set; }

        protected override void SetValitionRules()
        {
            if (Validator is null)
                return;

            Validator.RuleFor(p => TransactionId)
                .GreaterThan(0);

            Validator.RuleFor(p => InstallmentId)
                .GreaterThan(0);
        }
    }
}
