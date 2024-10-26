using System.Collections.Immutable;
using Microsoft.EntityFrameworkCore;
using PersonalFinanceManagement.Domain.Base.Contracts;
using PersonalFinanceManagement.Domain.Managements.Contracts;
using PersonalFinanceManagement.Domain.Managements.Dtos;
using PersonalFinanceManagement.Domain.Users.Dtos;
using Query = PersonalFinanceManagement.Domain.Managements.Queries.MangementQuery;

namespace PersonalFinanceManagement.Domain.Managements.Specifications
{
    public class ManagementSpecification : IManagementSpecification
    {
        private readonly IDBContext _context;

        public ManagementSpecification(IDBContext context)
        {
            _context = context;
        }

        public async Task<List<ManagementDto>> Get(int reference)
        {
            var results = new List<ManagementDto>();
            List<ManagementRemainingValueResult> remainingValue = await Query
                .GetRemainingValueQuery(reference, _context)
                .ToListAsync();
            List<ManagementResult> installments = await Query
                .GetInstallmentQuery(reference, _context)
                .ToListAsync();
            List<ManagementResult> transactions = await Query
                .GetTransactionQuery(reference, _context)
                .ToListAsync();
            IEnumerable<ManagementResult> union = installments.Union(transactions)
                .OrderBy(p => p.Date)
                    .ThenBy(p => p.CreatedAt);
            var data = GetManagementGroup(union).ToImmutableArray();

            if (data.Length == 0)
                return results;

            foreach (var item in data)
            {
                ManagementDto dto = GetManagementDto(
                    results,
                    item.Key
                );

                dto.RemainingValue = RemainingValueHandle(reference, item.Key.Id, remainingValue);

                foreach (var subItem in item)
                {
                    ManagementItemDto managementItem = CreateManagementItemDto(subItem);

                    dto.Items.Add(managementItem);
                }

                dto.Total = new ManagementTotalDto(dto.Items, dto.RemainingValue);
            }

            return results;
        }

        private static ManagementRemainingValueDto? RemainingValueHandle(
            int reference,
            int userId,
            List<ManagementRemainingValueResult> remainingValue
        )
        {
            IEnumerable<ManagementRemainingValueResult> userRemainingValue = remainingValue
                .Where(p => p.UserId == userId)
                .ToImmutableArray();

            if (!userRemainingValue.Any())
                return default;
            
            var renamingValue = new ManagementRemainingValueDto(userRemainingValue, reference);

            if (renamingValue.Value == 0)
                return default;
            
            return renamingValue;
        }

        private static IEnumerable<IGrouping<UserBasicDto, ManagementResult>> GetManagementGroup(
            IEnumerable<ManagementResult> remainingInstallments
        )
        {
            return remainingInstallments.GroupBy(p => new UserBasicDto
            {
                Id = p.UserId,
                Name = p.UserName
            });
        }

        private static ManagementDto GetManagementDto(
            List<ManagementDto> results,
            UserBasicDto user
        )
        {
            ManagementDto? management = results.FirstOrDefault(p =>
                p.User is not null &&
                p.User.Id == user.Id
            );

            if (management is not null)
            {
                return management;
            }

            ManagementDto newManagement = CreateManagementDto(user);

            results.Add(newManagement);

            return newManagement;
        }

        private static ManagementDto CreateManagementDto(UserBasicDto user)
        {
            return new ManagementDto
            {
                User = new UserDto
                {
                    Id = user.Id,
                    Name = user.Name,
                }
            };
        }

        private static ManagementItemDto CreateManagementItemDto(ManagementResult item)
        {
            return new ManagementItemDto
            {
                Id = item.Id,
                Reference = item.Reference,
                Type = item.Type,
                ManagementType = item.ManagementType,
                Date = item.Date,
                Description = item.Description,
                Amount = item.Amount
            };
        }
    }
}
