using PersonalFinanceManagement.Application.Contracts;
using PersonalFinanceManagement.Domain.Managements.Contracts;

namespace PersonalFinanceManagement.Application.Services
{
    public class ManagementService : IManagementService
    {
        private readonly IManagementSpecification _specification;

        public ManagementService(IManagementSpecification specification)
        {
            _specification = specification;
        }

        public async Task<object> List(int reference, int userId, bool isAdmin)
        {
            var results = await _specification.List(reference);

            return results;
        }
    }
}
