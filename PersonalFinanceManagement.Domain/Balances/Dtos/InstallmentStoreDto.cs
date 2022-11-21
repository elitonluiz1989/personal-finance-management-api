using PersonalFinanceManagement.Domain.Base.Dtos;

namespace PersonalFinanceManagement.Domain.Balances.Dtos
{
    public record InstallmentStoreDto : RecordedDto<int>
    {
        public int BalanceId { get; set; }
        public int Reference { get; set; }
        public short Number { get; set; }
        public decimal Value { get; set; }
    }
}
