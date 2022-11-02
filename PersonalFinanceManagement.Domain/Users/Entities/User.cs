using FluentValidation;
using PersonalFinanceManagement.Domain.Base.Entites;
using PersonalFinanceManagement.Domain.Users.Enums;

namespace PersonalFinanceManagement.Domain.Users.Entities
{
    public class User : Entity<int>
    {
        public Guid Guid { get; private set; } = Guid.NewGuid();
        public string UserName { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public UserRoleEnum Role { get; set; }

        public User()
        {
        }

        public User(
            string userName,
            string name,
            string email,
            string password,
            UserRoleEnum userRole
        )
        {
            UserName = userName;
            Name = name;
            Email = email;
            Password = password;
            Role = userRole;
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
