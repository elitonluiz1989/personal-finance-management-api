namespace PersonalFinanceManagement.Domain.Balances.Dtos
{
    public class InstallmentDto
    {
        public int Id { get; set; }
        public int BalanceId { get; set; }
        public BalanceDto? Balance { get; set; }
        public short Reference { get; set; }
        public short Number { get; set; }
        public decimal Value { get; set; }
    }
}
