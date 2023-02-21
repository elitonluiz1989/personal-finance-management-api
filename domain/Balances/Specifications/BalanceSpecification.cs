using Microsoft.EntityFrameworkCore;
using PersonalFinanceManagement.Domain.Balances.Contracts.Balances;
using PersonalFinanceManagement.Domain.Balances.Dtos;
using PersonalFinanceManagement.Domain.Balances.Extensions;
using PersonalFinanceManagement.Domain.Balances.Filters;
using PersonalFinanceManagement.Domain.Users.Contracts;
using PersonalFinanceManagement.Domain.Users.Enums;

namespace PersonalFinanceManagement.Domain.Balances.Specifications
{
    public class BalanceSpecification : IBalanceSpecification
    {
        private readonly IUserRepository _userRepository;
        private readonly IBalanceRepository _repository;

        public BalanceSpecification(
            IUserRepository userRepository,
            IBalanceRepository repository
        )
        {
            _userRepository = userRepository;
            _repository = repository;
        }

        public async Task<List<BalanceDto>> Get(BalanceFilter filter, int userId)
        {
            var userRole = await _userRepository.GetUserRole(userId);
            var query = _repository.Query();

            if (filter.Id > 0)
                query = query.Where(p => p.Id == filter.Id);

            if (filter.UserId > 0)
                query = query.Where(p => p.UserId == filter.UserId);

            if (filter.Types.Any())
                query = query.Where(p => filter.Types.Contains(p.Type));

            if (filter.Financed.HasValue)
                query = query.Where(p => p.Financed == filter.Financed);

            if (filter.InstallmentsNumber.HasValue)
                query = query.Where(p => p.InstallmentsNumber == p.InstallmentsNumber);

            if (filter.Closed.HasValue)
                query = query.Where(p => p.Closed == filter.Closed);

            if (userRole is not UserRoleEnum.Administrator)
                query = query.Where(p => p.UserId == userId);

            return await query
                .Include(p => p.Installments.Where(i => i.DeletedAt == null))
                .Select(s => BalanceMappingsExtension.ToBalanceDto(s))
                .ToListAsync();
        }

        public async Task<BalanceDto?> Find(int id, int userId)
        {
            var userRole = await _userRepository.GetUserRole(userId);
            var query = _repository.Query()
                .Where(p => p.Id == id);

            if (userRole is not UserRoleEnum.Administrator)
                query = query.Where(p => p.UserId == userId);

            return await query
                .Include(p => p.Installments)
                .Select(s => BalanceMappingsExtension.ToBalanceDto(s))
                .FirstOrDefaultAsync();
        }
    }
}
