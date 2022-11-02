using PersonalFinanceManagement.Domain.Base.Contracts;
using PersonalFinanceManagement.Domain.Users.Contracts;
using PersonalFinanceManagement.Domain.Users.Dtos;
using PersonalFinanceManagement.Domain.Users.Entities;

namespace PersonalFinanceManagement.Domain.Users.Services
{
    public class UserStore : IUserStore
    {
        private readonly IUserRepository _userRepository;

        public UserStore(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Store(UserStoreDto dto)
        {
            if (dto is null)
                throw new ArgumentNullException(nameof(dto));

            var user = await DefineUser(dto);

            if (user.Validate() is false)
                throw new Exception("Error");

            if (user.IsRecorded is false)
                _userRepository.Save(user);
        }

        private async Task<User> DefineUser(UserStoreDto dto)
        {
            if (dto.IsRecorded)
            {
                var user = await _userRepository.Find(dto.Id);

                if (user is null)
                    throw new ArgumentNullException(nameof(user));

                if (!string.IsNullOrEmpty(dto.UserName))
                    user.Name = dto.Name;

                if (!string.IsNullOrEmpty(dto.Name))
                    user.Name = dto.Name;

                if (!string.IsNullOrEmpty(dto.Password))
                    user.Password = dto.HashedPassword;

                if (user.Role.Equals(dto.Role) is false)
                    user.Role = dto.Role;

                return user;
            }

            return new User() {
                UserName = dto.UserName,
                Name = dto.Name,
                Email = dto.Email,
                Password = dto.HashedPassword,
                Role = dto.Role
            };
        }
    }
}
