using Microsoft.EntityFrameworkCore;
using PersonalFinanceManagement.Domain.Base.Contracts;
using PersonalFinanceManagement.Domain.Base.Specifications;
using PersonalFinanceManagement.Domain.Users.Contracts;
using PersonalFinanceManagement.Domain.Users.Dtos;
using PersonalFinanceManagement.Domain.Users.Entities;
using PersonalFinanceManagement.Domain.Users.Filters;

namespace PersonalFinanceManagement.Domain.Users.Specifications
{
    public class UserSpecification : Specification<User, int, UserFilter, UserDto>, IUserSpecification
    {
        public UserSpecification(IUserRepository repository)
            : base(repository)
        {
        }

        public override ISpecification<User, int, UserFilter, UserDto> WithFilter(UserFilter filter, int authenticatedUserId, bool isAdmin)
        {
            if (filter.Id > 0)
                _query = _query.Where(p => p.Id == filter.Id);

            if (!string.IsNullOrEmpty(filter.UserName))
                _query = _query.Where(p => p.UserName == filter.UserName);

            if (!string.IsNullOrEmpty(filter.Name))
                _query = _query.Where(p => p.Name == filter.Name);

            if (!string.IsNullOrEmpty(filter.Email))
                _query = _query.Where(p => p.Email == filter.Email);

            if (filter.Roles.Any() is true)
                _query = _query.Where(p => filter.Roles.Contains(p.Role));

            return this;
        }

        public async Task<List<UserDto>> Get(UserFilter filter)
        {
            var query = _repository.Query();

            if (filter.Id > 0)
                query = query.Where(p => p.Id == filter.Id);

            if (!string.IsNullOrEmpty(filter.UserName))
                query = query.Where(p => p.UserName == filter.UserName);

            if (!string.IsNullOrEmpty(filter.Name))
                query = query.Where(p => p.Name == filter.Name);

            if (!string.IsNullOrEmpty(filter.Email))
                query = query.Where(p => p.Email == filter.Email);

            if (filter.Roles.Any() is true)
                query = query.Where(p => filter.Roles.Contains(p.Role));

            
            return await query.Select(s => new UserDto()
            {
                Id = s.Id,
                UserName = s.UserName,
                Name =  s.Name,
                Email = s.Email,
                Role = s.Role
            })
            .ToListAsync();
        }

        public override async Task<IEnumerable<UserDto>> List()
        {
            return await _query.Select(s => new UserDto()
            {
                Id = s.Id,
                UserName = s.UserName,
                Name = s.Name,
                Email = s.Email,
                Role = s.Role
            })
            .ToListAsync();
        }

        public override async Task<UserDto?> First()
        {
            return await _query.Select(s => new UserDto()
            {
                Id = s.Id,
                UserName = s.UserName,
                Name = s.Name,
                Email = s.Email,
                Role = s.Role
            })
            .FirstOrDefaultAsync();
        }
    }
}
