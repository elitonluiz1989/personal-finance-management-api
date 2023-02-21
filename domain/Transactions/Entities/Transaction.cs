using FluentValidation;
using PersonalFinanceManagement.Domain.Base.Contracts;
using PersonalFinanceManagement.Domain.Base.Entites;
using PersonalFinanceManagement.Domain.Transactions.Enums;
using PersonalFinanceManagement.Domain.Users.Entities;

namespace PersonalFinanceManagement.Domain.Transactions.Entities
{
    public class Transaction : Entity<int>, IEntityWithRegistrationDates, IEntityWithSoftDelete
    {
        public int UserId { get; set; }
        public TransactionTypeEnum Type { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpadtedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual User? User { get; set; }
        public virtual List<TransactionItem> Items { get; set; } = new();

        public Transaction()
        {
        }

        public void SetRegistrationDates()
        {
            if (IsRecorded)
            {
                UpadtedAt = DateTime.Now;
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

            Validator.RuleFor(p => Type)
                .IsInEnum();

            Validator.RuleFor(p => Date)
                .NotNull()
                .NotEqual(default(DateTime));

            Validator.RuleFor(p => Amount)
                .GreaterThan(0);
        }
    }
}
