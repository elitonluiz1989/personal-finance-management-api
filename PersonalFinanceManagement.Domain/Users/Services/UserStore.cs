using PersonalFinanceManagement.Domain.Base.Contracts;
using PersonalFinanceManagement.Domain.Base.Dtos;
using PersonalFinanceManagement.Domain.Base.Services;
using PersonalFinanceManagement.Domain.Users.Contracts;
using PersonalFinanceManagement.Domain.Users.Dtos;
using PersonalFinanceManagement.Domain.Users.Entities;

namespace PersonalFinanceManagement.Domain.Users.Services
{
    public class UserStore : BaseStore<User, int>, IUserStore
    {
        public UserStore(
            INotificationService notificationService,
            IUserRepository repository
        )
            : base(notificationService, repository)
        {
        }

        protected override async Task<User?> SetEntity<TDto>(TDto dto)
        {
            var userDto = ConvertDto(dto);

            if (userDto is null || HasNotifications)
                return null;

            if (userDto.IsRecorded)
                return await UpdateUser(userDto);

            return CreateUser(userDto);
        }

        private static User CreateUser(UserStoreDto userDto)
        {
            return new User()
            {
                UserName = userDto.UserName,
                Name = userDto.Name,
                Email = userDto.Email,
                Password = userDto.HashedPassword,
                Role = userDto.Role
            };
        }

        private async Task<User?> UpdateUser(UserStoreDto userDto)
        {
            var user = await _repository.Find(userDto.Id);

            if (user is null)
            {
                AddNotification($"{nameof(user)} is null");

                return null;
            }

            if (!string.IsNullOrEmpty(userDto.UserName))
                user.Name = userDto.Name;

            if (!string.IsNullOrEmpty(userDto.Name))
                user.Name = userDto.Name;

            if (!string.IsNullOrEmpty(userDto.Password))
                user.Password = userDto.HashedPassword;

            if (user.Role.Equals(userDto.Role) is false)
                user.Role = userDto.Role;

            return user;
        }

        private UserStoreDto? ConvertDto<TDto>(TDto dto) where TDto : RecordedDto<int>
        {
            var userDto = dto as UserStoreDto;

            if (userDto is null)
                AddNotification($"{nameof(userDto)} is null");

            return userDto;
        }
    }
}
