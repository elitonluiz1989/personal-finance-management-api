using FluentValidation;
using PersonalFinanceManagement.Domain.Balances.Entities;
using PersonalFinanceManagement.Domain.Base.Contracts;
using PersonalFinanceManagement.Domain.Base.Entites;
using PersonalFinanceManagement.Domain.Transactions.Entities;
using PersonalFinanceManagement.Domain.Users.Entities;

namespace PersonalFinanceManagement.Domain.Managements.Entities
{
    public class Management : Entity<int>, IEntityWithRegistrationDates, IEntityWithSoftDelete
    {
        public int UserId { get; set; }
        public int Reference { get; set; }
        public decimal InitialAmount { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual User? User { get; set; }
        public virtual List<Installment> Installments { get; set; } = new();
        public virtual List<Transaction> Transactions { get; set; } = new();

        public void SetRegistrationDates()
        {
            if (IsRecorded)
            {
                UpdatedAt = DateTime.Now;
            }
            else
            {
                CreatedAt = DateTime.Now;
            }
        }

        public void SetAsDeleted()
        {
            DeletedAt = DateTime.Now;
        }

        protected override void SetValitionRules()
        {
            if (Validator is null)
                return;

            Validator.RuleFor(p => UserId)
                .GreaterThan(0)
                .When(p => User is null);

            Validator.RuleFor(p => User)
                .NotNull()
                .When(p => UserId <= 0);

            Validator.RuleFor(p => InitialAmount)
                .GreaterThan(0);

            Validator.RuleFor(p => Amount)
                .GreaterThan(0);
        }
    }
}
