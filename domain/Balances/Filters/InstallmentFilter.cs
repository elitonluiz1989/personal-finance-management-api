﻿using PersonalFinanceManagement.Domain.Balances.Enums;
using PersonalFinanceManagement.Domain.Base.Filters;

namespace PersonalFinanceManagement.Domain.Balances.Filters
{
    public record InstallmentFilter : Filter
    {
        public int Id { get; set; }
        public int BalanceId { get; set; }
        public int UserId { get; set; }
        public int TransactionId { get; set; }
        public BalanceTypeEnum? BalanceType { get; set; }
        public int Reference { get; set; }
        public short Number { get; set; }
        public InstallmentStatusEnum Status { get; set; }
        public decimal Amount { get; set; }
        public bool InstallmentToAddAtTransaction { get; set; }
    }
}
