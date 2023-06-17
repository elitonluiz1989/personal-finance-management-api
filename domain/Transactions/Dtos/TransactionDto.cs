﻿using PersonalFinanceManagement.Domain.Transactions.Enums;
using PersonalFinanceManagement.Domain.Base.Extensions;

namespace PersonalFinanceManagement.Domain.Transactions.Dtos
{
    public record TransactionDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public TransactionTypeEnum Type { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }

        public string TypeDescription => Type.GetDescription();
    }
}
