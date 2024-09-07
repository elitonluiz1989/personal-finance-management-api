using Microsoft.EntityFrameworkCore;
using PersonalFinanceManagement.Domain.Balances.Extensions;
using PersonalFinanceManagement.Domain.Base.Contracts;
using PersonalFinanceManagement.Domain.Base.Enums;
using PersonalFinanceManagement.Domain.Managements.Contracts;
using PersonalFinanceManagement.Domain.Managements.Dtos;
using PersonalFinanceManagement.Domain.Managements.Entities;
using PersonalFinanceManagement.Domain.Managements.Enums;
using PersonalFinanceManagement.Domain.Users.Dtos;
using Query = PersonalFinanceManagement.Domain.Managements.Queries.MangementQuery;

namespace PersonalFinanceManagement.Domain.Managements.Specifications
{
    public class ManagementSpecification : ManagementBaseSpecificationBase, IManagementSpecification
    {
        public ManagementSpecification(IDBContext context) : base(context)
        {
        }

        public async Task<List<ManagementDto>> Get(int reference)
        {
            var results = new List<ManagementDto>();
            List<ManagementResult> installments = await Query
                .GetInstallmentQuery(reference, _context)
                .ToListAsync();
            List<ManagementResult> transactions = await Query
                .GetTransactionQuery(reference, _context)
                .ToListAsync();
            IEnumerable<ManagementResult> union = installments.Union(transactions);
            IEnumerable<IGrouping<UserBasicDto, ManagementResult>> data =
                GetManagementGroup(union);

            if (!data.Any())
            {
                return results;
            }

            List<Management> previousManagements = await GetPreviousManagements(reference);
            List<Management> managements = await GetManagements(reference);

            foreach (var item in data)
            {
                Management management = GetUserManagement(managements, item.Key.Id) ?? new();
                ManagementDto dto = GetManagementDto(
                    results,
                    item.Key,
                    management.Id
                );

                InitalAmountHandler(dto, previousManagements);

                foreach (var subItem in item)
                {
                    ManagementItemDto managementItem = CreateManagementItemDto(subItem);

                    dto.Items.Add(managementItem);
                }

                dto.Total = new ManagementTotalDto(dto.Items);
                dto.Status = GetStatus(management, dto.Total);
            }

            return results;
        }

        private static ManagementStatusEnum GetStatus(Management management, ManagementTotalDto total)
        {
            if (management.Id == 0)
                return ManagementStatusEnum.Unsaved;

            decimal totalTreated = total.Type == CommonTypeEnum.Debt ? total.Value * -1 : total.Value;

            if (management.Amount == totalTreated)
                return ManagementStatusEnum.Updated;

            return ManagementStatusEnum.Outdated;
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
            UserBasicDto user,
           int managementId
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
            newManagement.Id = managementId;

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

        private static void InitalAmountHandler(ManagementDto dto, List<Management> previousManagements)
        {
            Management? previousManagement = GetUserManagement(previousManagements, dto!.User!.Id);

            if (previousManagement is null)
                return;

            if (previousManagement.Amount == 0)
                return;

            ManagementItemDto managementItem = CreateInitialAmountDto(previousManagement);

            dto.Items.Add(managementItem);
        }

        private static ManagementItemDto CreateInitialAmountDto(Management management)
        {
            CommonTypeEnum type = management.Amount < 0
                ? CommonTypeEnum.Debt
                : CommonTypeEnum.Credit;

            return new ManagementItemDto
            {
                Reference = management.Reference,
                Type = type,
                Date = management.Reference.ToDateTime().ToShortDateString(),
                Description = "Initial amount",
                Amount = Math.Abs(management.Amount)
            };
        }
    }
}
