using Microsoft.EntityFrameworkCore;
using PersonalFinanceManagement.Domain.Balances.Contracts.Balances;
using PersonalFinanceManagement.Domain.Balances.Dtos;
using PersonalFinanceManagement.Domain.Balances.Entities;
using PersonalFinanceManagement.Domain.Balances.Filters;
using PersonalFinanceManagement.Domain.Transactions.Dtos;
using PersonalFinanceManagement.Domain.Transactions.Entities;

namespace PersonalFinanceManagement.Domain.Balances.Specifications
{
    public class BalanceSpecification : IBalanceSpecification
    {
        private readonly IBalanceRepository _repository;

        public BalanceSpecification(IBalanceRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<BalanceDto>> Get(BalanceFilter filter)
        {
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

            return await query.Select(s => new BalanceDto()
            {
                Id = s.Id,
                UserId = s.UserId,
                Type = s.Type,
                Amount = s.Amount,
                Financed = s.Financed,
                InstallmentsNumber = s.InstallmentsNumber,
                Installments = s.Installments
                    .Select(i => GetInstallment(i))
                    .ToList()
            })
            .ToListAsync();
        }

        private static InstallmentDto GetInstallment(Installment installment)
        {
            return new InstallmentDto()
            {
                Id = installment.Id,
                BalanceId = installment.BalanceId,
                Reference = installment.Reference,
                Number = installment.Number,
                Amount = installment.Amount,
                Items = installment.Items.Select(ti => new TransactionItemDto()
                {
                    TransactionId = ti.TransactionId,
                    Transaction = GetTransaction(ti.Transaction),
                    Type = ti.Type
                })
                .ToList()
            };
        }

        private static TransactionDto? GetTransaction(Transaction? transaction)
        {
            if (transaction is null)
                return default;

            return new TransactionDto()
            {
                Id = transaction.Id,
                Type = transaction.Type,
                Date = transaction.Date,
                Amount = transaction.Amount
            };
        }
    }
}
