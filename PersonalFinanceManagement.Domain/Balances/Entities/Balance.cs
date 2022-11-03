using FluentValidation;
using PersonalFinanceManagement.Domain.Balances.Enums;
using PersonalFinanceManagement.Domain.Base.Contracts;
using PersonalFinanceManagement.Domain.Base.Entites;
using PersonalFinanceManagement.Domain.Users.Entities;

namespace PersonalFinanceManagement.Domain.Balances.Entities
{
    public class Balance : Entity<int>, IEntityWithRegistrationDates, IEntityWithSoftDelete
    {
        public int UserId { get; set; }
        public BalanceTypeEnum Type { get; set; }
        public BalanceStatusEnum Status { get; set; }
        public decimal Value { get; set; }
        public bool Financed { get; set; }
        public short? NumberOfInstallments { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpadtedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual User? User { get; set; }
        public virtual List<Installment> Installments { get; set; } = new List<Installment>();
        public virtual List<Transaction> Transactions { get; set; } = new List<Transaction>();

        public Balance(
            int userId,
            BalanceTypeEnum type,
            BalanceStatusEnum status,
            decimal value,
            bool financed,
            short? numberOfInstallments
        )
        {
            UserId = userId;
            Type = type;
            Status = status;
            Value = value;
            Financed = financed;
            NumberOfInstallments = numberOfInstallments;
        }

        public void SetRegistrationDates()
        {
            if (IsRecorded)
            {
                UpadtedAt = DateTime.Now;

                return;
            }

            CreatedAt = DateTime.Now;
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
                .GreaterThan(0);

            Validator.RuleFor(p => Type)
                .IsInEnum();

            Validator.RuleFor(p => Status)
                .IsInEnum();

            Validator.RuleFor(p => Value)
                .GreaterThan(0);

            Validator.RuleFor(p => Financed)
                .NotNull();

            Validator.RuleFor(p => CreatedAt)
                .NotNull();
        }
    }
}
