using PersonalFinanceManagement.Domain.Users.Dtos;
using PersonalFinanceManagement.Domain.Users.Entities;

namespace PersonalFinanceManagement.Domain.Users.Extensions
{
    public static class UsersMappingExtensions
    {
        public static UserDto ToUserDto(this User user)
        {
            return new UserDto()
            {
                Id = user.Id,
                UserName = user.UserName,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role
            };
        }

        public static UserBasicDto ToUserBasicDto(this User user)
        {
            return new UserBasicDto()
            {
                Id = user.Id,
                Name = user.Name
            };
        }
    }
}
