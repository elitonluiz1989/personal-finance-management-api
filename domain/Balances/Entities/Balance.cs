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
        public string Name { get; set; } = string.Empty;
        public BalanceTypeEnum Type { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public bool Financed { get; set; }
        public short InstallmentsNumber { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpadtedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public bool Closed => VerifyIfClosed();

        public virtual User? User { get; set; }
        public virtual List<Installment> Installments { get; set; } = new List<Installment>();
        public virtual List<RefinancedBalance> RefinancedBalances { get; set; } = new List<RefinancedBalance>();

        private bool? _closed;

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

        public decimal GetAmountToTransaction()
        {
            var totalUnpaid = Installments
                ?.Where(i => i.Status == InstallmentStatusEnum.Created)
                ?.Sum(i => i.Amount) ?? 0;
            var installmentsPartiallyPaid = Installments
                ?.Where(i => i.Status == InstallmentStatusEnum.PartiallyPaid)
                ?.ToList();

            if (installmentsPartiallyPaid is null || installmentsPartiallyPaid.Any() is false)
                return totalUnpaid;

            var remainingAmount = installmentsPartiallyPaid.Sum(ipp => ipp.GetAmountToTransactions());

            return totalUnpaid - remainingAmount;
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
                .When(p => UserId == default);

            Validator.RuleFor(p => Type)
                .IsInEnum();

            Validator.RuleFor(p => Date)
                .NotEqual(default(DateTime));

            Validator.RuleFor(p => Amount)
                .GreaterThan(0);

            Validator.RuleFor<int>(p => InstallmentsNumber)
                .GreaterThan(0)
                .When(p => Financed is true);
        }

        private bool VerifyIfClosed()
        {
            if (_closed.HasValue is false)
                _closed = Installments?.All(i => i.Active is false) ?? false;

            return _closed.Value;
        }
    }
}
