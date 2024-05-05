using FluentValidation;
using PersonalFinanceManagement.Domain.Balances.Enums;
using PersonalFinanceManagement.Domain.Base.Contracts;
using PersonalFinanceManagement.Domain.Base.Entites;
using PersonalFinanceManagement.Domain.Base.Enums;
using PersonalFinanceManagement.Domain.Users.Entities;

namespace PersonalFinanceManagement.Domain.Balances.Entities
{
    public class Balance : Entity<int>, IEntityWithRegistrationDates, IEntityWithSoftDelete
    {
        public int UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public CommonTypeEnum Type { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public bool Financed { get; set; }
        public short InstallmentsNumber { get; set; }
        public bool Residue { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public bool Closed =>
            Installments?.All(i => i.Active is false) ?? false;

        public bool HasTransactions =>
            Installments?.Any(i => i.HasTransactions) ?? false;

        public virtual User? User { get; set; }
        public virtual List<Installment> Installments { get; set; } = new List<Installment>();
        public virtual List<RefinancedBalance> RefinancedBalances { get; set; } = new List<RefinancedBalance>();

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

            Validator.RuleFor(p => Name)
                .NotEmpty()
                .MaximumLength(100);

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
    }
}
