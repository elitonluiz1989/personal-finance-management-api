using PersonalFinanceManagement.Domain.Base.Dtos;

namespace PersonalFinanceManagement.Domain.Transactions.Dtos
{
    public record TransactionItemStoreDto : RecordedDto<int>
    {
        public int InstallmentId { get; set; }
        public bool PartiallyPaid { get; set; }
        public decimal AmountPaid { get; set; }
    }
}
