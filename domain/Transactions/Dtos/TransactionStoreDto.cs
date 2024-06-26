﻿using PersonalFinanceManagement.Domain.Base.Dtos;
using PersonalFinanceManagement.Domain.Base.Enums;

namespace PersonalFinanceManagement.Domain.Transactions.Dtos
{
    public record TransactionStoreDto : RecordedDto<int>
    {
        public int UserId { get; set; }
        public CommonTypeEnum Type { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public bool UseBalancesAsValue { get; set; }
        public int[] BalancesIds { get; set; } = Array.Empty<int>();
        public int[] InstallmentsIds { get; set; } = Array.Empty<int>();
    }
}
