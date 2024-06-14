using Microsoft.EntityFrameworkCore;
using PersonalFinanceManagement.Domain.Base.Contracts;
using PersonalFinanceManagement.Domain.Base.Enums;
using PersonalFinanceManagement.Domain.Managements.Contracts;
using PersonalFinanceManagement.Domain.Managements.Dtos;
using PersonalFinanceManagement.Domain.Managements.Entities;
using PersonalFinanceManagement.Domain.Managements.Enums;
using PersonalFinanceManagement.Domain.Managements.Filters;
using PersonalFinanceManagement.Domain.Managements.Utils;
using Query = PersonalFinanceManagement.Domain.Managements.Queries.MangementQuery;

namespace PersonalFinanceManagement.Domain.Managements.Specifications
{
    public class ManagementStoreSpecification : ManagementBaseSpecificationBase, IManagementStoreSpecification
    {
        public ManagementStoreSpecification(IDBContext context) : base(context)
        {
        }

        public async Task<List<ManagementStoreDto>> Get(ManagementStoreFilter filter)
        {
            var results = new List<ManagementStoreDto>();
            IEnumerable<IGrouping<int, ManagementStoreResult>> data = await GetData(filter);
            List<Management> previousManagements = await GetPreviousManagements(filter);
            List<Management> managements = await GetManagements(filter);

            if (!data.Any())
            {
                return results;
            }

            foreach (var item in data)
            {
                Management? previousManagement = previousManagements
                    .Find(p => p.UserId == item.Key);
                List<ManagementItemAmountDto> items = item
                    .Select(p => new ManagementItemAmountDto
                    {
                        Type = p.Type,
                        Amount = p.Amount
                    })
                    .ToList();
                ManagementStoreDto dto = new()
                {
                    Id = GetManagementId(managements, userId: item.Key),
                    UserId = item.Key,
                    Reference = filter.Reference,
                    InitialAmount = previousManagement?.Amount ?? 0,
                    Amount = CalculateAmount(items),
                    InstallmentsIds = GetItemIds(item, ManagementItemTypeEnum.Installment),
                    TransactionsIds = GetItemIds(item, ManagementItemTypeEnum.Transaction)
                };

                results.Add(dto);
            }

            return results;
        }

        private async Task<IEnumerable<IGrouping<int, ManagementStoreResult>>> GetData(ManagementStoreFilter filter)
        {
            List<ManagementStoreResult> installments = await Query
                .GetInstallmentToStoreQuery(filter, _context)
                .ToListAsync();
            List<ManagementStoreResult> transactions = await Query
                .GetTransactionToStoreQuery(filter, _context)
                .ToListAsync();
            IEnumerable<ManagementStoreResult> union = installments.Union(transactions);
            IEnumerable<IGrouping<int, ManagementStoreResult>> data =
                union.GroupBy(p => p.UserId);

            return data;
        }

        private static int GetManagementId(
            List<Management> managements,
            int userId
        )
        {
            Management? management = GetUserManagement(managements, userId);

            if (management is null)
                return 0;

            return management.Id;
        }

        private static decimal CalculateAmount(List<ManagementItemAmountDto> items)
        {
            (CommonTypeEnum type, decimal value) =
                ManagementUtils.CalculateReferenceAmount(items);

            if (type == CommonTypeEnum.Debt)
                value *= -1;

            return value;
        }

        private static int[] GetItemIds(
            IGrouping<int, ManagementStoreResult> data,
            ManagementItemTypeEnum type
        )
        {
            return data
                .Where(p => p.ManagementType == type)
                .Select(p => p.Id)
                .ToArray();
        }
    }
}
