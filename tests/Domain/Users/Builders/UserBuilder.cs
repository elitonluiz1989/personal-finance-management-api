using Bogus;
using PersonalFinanceManagement.Domain.Users.Entities;
using PersonalFinanceManagement.Domain.Users.Enums;

namespace PersonalFinanceManagement.Tests.Domain.Users.Builders
{
    internal class UserBuilder
    {
        private int _id;
        private string _userName = string.Empty;
        private string _name = string.Empty;
        private string _email = string.Empty;
        private string _password = string.Empty;
        private UserRoleEnum _userRole;

        internal static UserBuilder New()
        {
            var faker = new Faker();

            return new UserBuilder()
            {
                _name = faker.Person.FullName,
                _userName = faker.Internet.UserName(),
                _email = faker.Internet.Email(),
                _password = faker.Internet.Password(),
                _userRole = faker.PickRandom<UserRoleEnum>()
            };
        }

        internal UserBuilder WithId(int id)
        {
            _id = id;

            return this;
        }

        internal UserBuilder WithoutId()
        {
            _id = default;

            return this;
        }

        internal UserBuilder WithUserName(string name)
        {
            _userName = name;

            return this;
        }

        internal UserBuilder WithoutUserName()
        {
            _userName = string.Empty;

            return this;
        }

        internal UserBuilder WithName(string name)
        {
            _name = name;

            return this;
        }

        internal UserBuilder WithoutName()
        {
            _name = string.Empty;

            return this;
        }

        internal UserBuilder WithEmail(string email)
        {
            _email = email;

            return this;
        }

        internal UserBuilder WithoutEmail()
        {
            _email = string.Empty;

            return this;
        }

        internal UserBuilder WithPassword(string password)
        {
            _password = password;

            return this;
        }

        internal UserBuilder WithoutPassword()
        {
            _password = string.Empty;

            return this;
        }

        internal UserBuilder WithUserRole(UserRoleEnum userRole)
        {
            _userRole = userRole;

            return this;
        }

        internal UserBuilder WithoutUserRole()
        {
            _userRole = default;

            return this;
        }

        internal User Build()
        {
            return new User()
            {
                Id = _id,
                UserName = _userName,
                Name = _name,
                Email = _email,
                Password = _password,
                Role = _userRole
            };
        }
    }
}
