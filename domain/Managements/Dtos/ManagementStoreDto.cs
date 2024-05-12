using PersonalFinanceManagement.Domain.Base.Dtos;

namespace PersonalFinanceManagement.Domain.Managements.Dtos
{
    public record ManagementStoreDto : RecordedDto<int>
    {
        public int UserId { get; set; }
        public int Reference { get; private set; }
        public decimal InitialAmount { get; set; }
        public decimal Amount { get; set; }
        public int[] InstallmentsIds { get; set; } = Array.Empty<int>();
        public int[] TransactionsIds { get; set; } = Array.Empty<int>();
    }
}
