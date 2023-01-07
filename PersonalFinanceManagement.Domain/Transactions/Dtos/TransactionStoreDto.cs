using PersonalFinanceManagement.Domain.Base.Dtos;
using PersonalFinanceManagement.Domain.Transactions.Enums;

namespace PersonalFinanceManagement.Domain.Transactions.Dtos
{
    public record TransactionStoreDto : RecordedDto<int>
    {
        public int UserId { get; set; }
        public TransactionTypeEnum Type { get; set; }
        public DateTime Date { get; set; }
        public decimal Value { get; set; }
        public List<short> InstallmentsIds { get; set; } = new();
    }
}
