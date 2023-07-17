using PersonalFinanceManagement.Domain.Base.Contracts;
using PersonalFinanceManagement.Domain.Base.Specifications;
using PersonalFinanceManagement.Domain.Users.Contracts;
using PersonalFinanceManagement.Domain.Users.Dtos;
using PersonalFinanceManagement.Domain.Users.Entities;
using PersonalFinanceManagement.Domain.Users.Extensions;
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
                Query = Query.Where(p => p.Id == filter.Id);

            if (!string.IsNullOrEmpty(filter.UserName))
                Query = Query.Where(p => p.UserName == filter.UserName);

            if (!string.IsNullOrEmpty(filter.Name))
                Query = Query.Where(p => p.Name == filter.Name);

            if (!string.IsNullOrEmpty(filter.Email))
                Query = Query.Where(p => p.Email == filter.Email);

            if (filter.Roles.Any() is true)
                Query = Query.Where(p => filter.Roles.Contains(p.Role));

            return this;
        }

        protected override IQueryable<UserDto> GetQuery()
        {
            return Query.Select(s => s.ToUserDto());
        }
    }
}
