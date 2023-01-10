using PersonalFinanceManagement.Domain.Base.Dtos;
using PersonalFinanceManagement.Domain.Transactions.Enums;

namespace PersonalFinanceManagement.Domain.Transactions.Dtos
{
    public record TransactionItemStoreDto : RecordedDto<int>
    {
        public int InstallmentId { get; set; }
        public TransactionItemTypeEnum Type { get; set; }
        public bool PartiallyPaid { get; set; }
        public decimal AmountPaid { get; set; }
    }
}
