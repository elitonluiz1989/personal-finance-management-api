using FluentValidation;
using PersonalFinanceManagement.Domain.Base.Contracts;
using PersonalFinanceManagement.Domain.Base.Entites;
using PersonalFinanceManagement.Domain.Users.Entities;

namespace PersonalFinanceManagement.Domain.Balances.Entities
{
    public class RefinancedBalance : Entity<int>, IEntityWithRegistrationDates
    {
        public int BalanceId { get; set; }
        public DateTime OriginalDate { get; set; }
        public decimal OriginalValue { get; set; }
        public bool OriginalFinanced { get; set; }
        public short OriginalInstallmentsNumber { get; set; }
        public DateTime Date { get; set; }
        public decimal Value { get; set; }
        public bool Financed { get; set; }
        public short InstallmentsNumber { get; set; }
        public bool Active { get; set; } = true;
        public DateTime CreatedAt { get; set; }

        public virtual User? User { get; set; }
        public virtual Balance? Balance { get; set; }

        public void SetRegistrationDates()
        {
            CreatedAt = DateTime.Now;
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
                .When(p => BalanceId == default);

            Validator.RuleFor(p => Date)
                .NotEqual(default(DateTime));

            Validator.RuleFor(p => OriginalValue)
                .GreaterThan(0);

            Validator.RuleFor(p => Value)
                .GreaterThan(0);
        }
    }
}
