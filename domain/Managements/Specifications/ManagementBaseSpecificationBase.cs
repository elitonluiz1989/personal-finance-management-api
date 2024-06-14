using Microsoft.EntityFrameworkCore;
using PersonalFinanceManagement.Domain.Base.Contracts;
using PersonalFinanceManagement.Domain.Managements.Entities;
using PersonalFinanceManagement.Domain.Managements.Filters;
using PersonalFinanceManagement.Domain.Managements.Utils;

namespace PersonalFinanceManagement.Domain.Managements.Specifications
{
    public class ManagementBaseSpecificationBase
    {

        protected readonly IDBContext _context;

        public ManagementBaseSpecificationBase(IDBContext context)
        {
            _context = context;
        }

        protected static Management? GetUserManagement(
            List<Management> managements,
            int userId
        )
        {
            return managements
                .Where(p => p.UserId == userId)
                .FirstOrDefault();
        }

        protected async Task<List<Management>> GetManagements(ManagementStoreFilter filter)
        {
            return await GetManagements(filter.Reference, filter.UserId);
        }

        protected async Task<List<Management>> GetManagements(int reference)
        {
            return await GetManagements(reference, userId: null);
        }

        protected async Task<List<Management>> GetPreviousManagements(ManagementStoreFilter filter)
        {
            int previousReference = ManagementUtils.GetPreviousReference(filter.Reference);

            if (previousReference <= 0)
                return new();

            List<Management> previousManagements = await GetManagements(
                previousReference,
                filter.UserId
            );

            return previousManagements;
        }

        protected async Task<List<Management>> GetPreviousManagements(int reference)
        {
            return await GetPreviousManagements(
                new ManagementStoreFilter { Reference = reference }
            );
        }

        private async Task<List<Management>> GetManagements(int reference, int? userId)
        {
            return await _context.Set<Management>()
                .Where(p =>
                    p.Reference == reference &&
                    (!userId.HasValue || p.UserId == userId)
                )
                .ToListAsync();
        }
    }
}