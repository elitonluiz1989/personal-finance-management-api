using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PersonalFinanceManagement.Domain.Base.Contracts;
using PersonalFinanceManagement.Domain.Base.Tools.Security;
using PersonalFinanceManagement.Domain.Users.Entities;
using PersonalFinanceManagement.Domain.Users.Enums;
using PersonalFinanceManagement.Infra.Data.Contracts;
using PersonalFinanceManagement.Infra.Data.Repositories;

namespace PersonalFinanceManagement.Infra.Data.Seeders
{
    internal class UserSeeder : Seeder
    {
        private readonly IRepository<User, int> _repository;

        public UserSeeder(
            IDBContext dbContext,
            IUnitOfWork unitOfWork
        ) : base(dbContext, unitOfWork)
        {
            _repository = new Repository<User, int>(dbContext);
        }

        public override async Task Run()
        {
            var settings = Configuration?.GetSection("users");

            if (settings is null)
                return;

            var users = settings
                .GetChildren()
                .Select(s => s.AsEnumerable())
                .ToList();

            if (users.Any() is false)
                return;
            
            foreach (var temp in users)
            {
                var user = new User();

                foreach (var item in temp)
                {
                    if (item.Key.Contains(nameof(user.UserName)))
                        user.UserName = item.Value;

                    if (item.Key.Contains(nameof(user.Name)))
                        user.Name = item.Value;

                    if (item.Key.Contains(nameof(user.Email)))
                        user.Email = item.Value;

                    if (item.Key.Contains(nameof(user.Password)))
                        user.Password = Hash.Encrypt(item.Value);

                    if (item.Key.Contains(nameof(user.Role)))
                    {
                        var success = Enum.TryParse(item.Value, out UserRoleEnum userRole);

                        if (success)
                            user.Role = userRole;
                    }
                }

                var recordedUser = await _repository.Query()
                    .Where(p => p.UserName == user.UserName || p.Email == user.Email)
                    .ToListAsync();

                if (recordedUser.Any() || user.Validate() is false)
                    continue;

                _repository.Save(user);
            }

            UnitOfWork.Commit();
        }
    }
}
