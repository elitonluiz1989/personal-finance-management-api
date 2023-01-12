using FluentValidation;
using PersonalFinanceManagement.Domain.Balances.Entities;
using PersonalFinanceManagement.Domain.Base.Entites;
using PersonalFinanceManagement.Domain.Transactions.Entities;
using PersonalFinanceManagement.Domain.Users.Enums;

namespace PersonalFinanceManagement.Domain.Users.Entities
{
    public class User : Entity<int>
    {
        public string UserName { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public UserRoleEnum Role { get; set; }

        public virtual List<Balance> Balances { get; set; } = new List<Balance>();
        public virtual List<RefinancedBalance> RefinancedBalances { get; set; } = new List<RefinancedBalance>();
        public virtual List<Transaction> Transactions { get; set; } = new List<Transaction>();

        public User()
        {
        }

        protected override void SetValitionRules()
        {
            if (Validator is null)
                return;

            Validator.RuleFor(p => UserName)
                .MaximumLength(50)
                .NotEmpty();

            Validator.RuleFor(p => Name)
                .MaximumLength(75)
                .NotEmpty();

            Validator.RuleFor(p => Email)
                .NotEmpty();

            Validator.RuleFor(p => Password)
                .NotEmpty();

            Validator.RuleFor(p => Role)
                .IsInEnum();
        }
    }
}
