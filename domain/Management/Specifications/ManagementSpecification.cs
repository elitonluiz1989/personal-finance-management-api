using Microsoft.EntityFrameworkCore;
using PersonalFinanceManagement.Domain.Balances.Extensions;
using PersonalFinanceManagement.Domain.Base.Contracts;
using PersonalFinanceManagement.Domain.Base.Enums;
using PersonalFinanceManagement.Domain.Base.Extensions;
using PersonalFinanceManagement.Domain.Management.Contracts;
using PersonalFinanceManagement.Domain.Management.Dtos;
using PersonalFinanceManagement.Domain.Management.Enums;
using PersonalFinanceManagement.Domain.Users.Dtos;
using Query = PersonalFinanceManagement.Domain.Management.Queries.MangementQuery;

namespace PersonalFinanceManagement.Domain.Management.Specifications
{
    public class ManagementSpecification : IManagementSpecification
    {
        protected readonly IDBContext _context;

        public ManagementSpecification(IDBContext context)
        {
            _context = context;
        }

        public async Task<object> List(int reference)
        {
            var results = new List<ManagementDto>();
            await RemainingValueHandler(reference, results);
            await ManagementItemsHandler(reference, results);

            return results;
        }

        private async Task RemainingValueHandler(int reference, List<ManagementDto> results)
        {
            DateTime referenceDate = reference.ToDateTime();
            int previousReference = referenceDate.AddMonths(-1).ToReference();
            
            List<ManagementResult> remainingInstallments = await Query
                .GetRemainingInstallmentQuery(previousReference, _context)
                .ToListAsync();
            IEnumerable<IGrouping<UserBasicDto, ManagementResult>> groupedValues = 
                GetManagementGroup(remainingInstallments);

            foreach (var group in groupedValues)
            {
                decimal creditRemaningValue = group
                    .Where(p => p.Type == CommonTypeEnum.Credit)
                    .Sum(p => p.Amount);
                decimal debitRemaningValue = group
                    .Where(p => p.Type == CommonTypeEnum.Debt)
                    .Sum(p => p.Amount);
                decimal remainingValue = creditRemaningValue - debitRemaningValue;

                if (remainingValue == 0)
                {
                    continue;
                }

                ManagementDto management = CreateManagementDto(group.Key);
                management.Items = new List<ManagementItemDto>
                {
                    new()
                    {
                        Reference = previousReference,
                        Type = remainingValue > 0 ? CommonTypeEnum.Credit : CommonTypeEnum.Debt,
                        ManagementType = ManagementItemTypeEnum.RemainingValue,
                        Description = $"Remaming value of {previousReference.ToMonthYear()}",
                        Amount = Math.Abs(remainingValue)
                    }
                };

                results.Add(management);
            }
        }

        private async Task ManagementItemsHandler(int reference, List<ManagementDto> results)
        {
            List<ManagementResult> installments = await Query
                .GetInstallmentQuery(reference, _context)
                .ToListAsync();
            List<ManagementResult> transactions = await Query
                .GetTransactionQuery(reference, _context)
                .ToListAsync();
            IEnumerable<ManagementResult> union = installments.Union(transactions);
            IEnumerable<IGrouping<UserBasicDto, ManagementResult>> groupedValues =
                GetManagementGroup(union);

            if (groupedValues.Count() == 0)
            {
                return;
            }

            foreach (var innerGroup in groupedValues)
            {
                ManagementDto management = GetManagementResult(
                    results,
                    innerGroup.Key
                );

                foreach (var item in innerGroup)
                {
                    ManagementItemDto managementItem = CreateManagementItemDto(item);

                    management.Items.Add(managementItem);
                }
            }
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

        private static ManagementDto GetManagementResult(List<ManagementDto> results, UserBasicDto user)
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
                RecordId = item.Id,
                Reference = item.Reference,
                Type = item.Type,
                ManagementType = item.ManagementType,
                Description = item.Description,
                Amount = item.Amount
            };
        }
    }
}
