using FluentValidation;
using PersonalFinanceManagement.Domain.Balances.Enums;
using PersonalFinanceManagement.Domain.Base.Contracts;
using PersonalFinanceManagement.Domain.Base.Entites;
using PersonalFinanceManagement.Domain.Transactions.Entities;

namespace PersonalFinanceManagement.Domain.Balances.Entities
{
    public class Installment : Entity<int>, IEntityWithSoftDelete
    {
        public int BalanceId { get; set; }
        public int Reference { get; set; }
        public short Number { get; set; }
        public InstallmentStatusEnum Status { get; set; }
        public decimal Amount { get; set; }
        public DateTime? DeletedAt { get; set; }

        public bool Active => Status != InstallmentStatusEnum.Paid;
        public bool HasTransactions => TransactionItems?.Any() ?? false;

        public virtual Balance? Balance { get; set; }
        public virtual List<TransactionItem> TransactionItems { get; set; } = new();

        public Installment()
        {
            Status = InstallmentStatusEnum.Created;
        }

        public void SetAsDeleted()
        {
            DeletedAt = DateTime.Now;
        }

        public decimal GetAmountToTransactions()
        {
            var totalPartiallyPaid = TransactionItems
                ?.Where(i => i.PartiallyPaid && i.Transaction?.DeletedAt is null)
                ?.Sum(i => i.AmountPaid) ?? 0;

            if (totalPartiallyPaid > 0)
                return Amount - totalPartiallyPaid;

            return Amount;
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
                .When(p => BalanceId <= 0);

            Validator.RuleFor(p => Amount)
                .GreaterThan(0);
        }
    }
}
