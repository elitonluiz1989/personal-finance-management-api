using PersonalFinanceManagement.Domain.Users.Contracts;

namespace PersonalFinanceManagement.Domain.Users.Services
{
    public class UserDeleter : IUserDeleter
    {
        private readonly IUserRepository _repository;

        public UserDeleter(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task Delete(int id)
        {
            var user = await _repository.Find(id);

            if (user is null)
                throw new ArgumentNullException(nameof(user));

            _repository.Delete(user);
        }
    }
}
