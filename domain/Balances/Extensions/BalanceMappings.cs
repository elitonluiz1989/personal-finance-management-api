﻿using PersonalFinanceManagement.Domain.Balances.Dtos;
using PersonalFinanceManagement.Domain.Balances.Entities;
using PersonalFinanceManagement.Domain.Transactions.Dtos;

namespace PersonalFinanceManagement.Domain.Balances.Extensions
{
    public static class BalanceMappingsExtension
    {
        public static BalanceDto ToBalanceDto(this Balance balance)
        {
            return new BalanceDto()
            {
                Id = balance.Id,
                UserId = balance.UserId,
                Name = balance.Name,
                Type = balance.Type,
                Date = balance.Date,
                Amount = balance.Amount,
                Financed = balance.Financed,
                InstallmentsNumber = balance.InstallmentsNumber,
                Closed = balance.Closed,
                Installments = balance.Installments.Select(i => new InstallmentDto()
                {
                    Id = i.Id,
                    BalanceId = i.BalanceId,
                    Reference = i.Reference,
                    Number = i.Number,
                    Status = i.Status,
                    Amount = i.Amount,
                    Items = i.Items.Select(ti => new TransactionItemDto()
                    {
                        TransactionId = ti.TransactionId,
                        Type = ti.Type,
                        PartiallyPaid = ti.PartiallyPaid,
                        AmountPaid = ti.AmountPaid,
                        Transaction = ti.Transaction is not null ?
                            new TransactionDto()
                            {
                                Id = ti.Transaction.Id,
                                Type = ti.Transaction.Type,
                                Date = ti.Transaction.Date,
                                Amount = ti.Transaction.Amount
                            } :
                            null
                    })
                    .ToList()
                })
                .ToList()
            };
        }
    }
}
