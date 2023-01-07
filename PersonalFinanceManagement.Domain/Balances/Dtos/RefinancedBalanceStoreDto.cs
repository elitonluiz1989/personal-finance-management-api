namespace PersonalFinanceManagement.Domain.Balances.Dtos
{
    public record RefinancedBalanceStoreDto
    {
        public int BalanceId { get; set; }
        public DateTime Date { get; set; }
        public decimal Value { get; set; }
        public bool Financed { get; set; }
        public short InstallmentsNumber { get; set; }
    }
}
