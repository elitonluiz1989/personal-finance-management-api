using PersonalFinanceManagement.Domain.Balances.Enums;

namespace PersonalFinanceManagement.Domain.Balances.Dtos
{
    public record BalanceDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public BalanceTypeEnum Type { get; set; }
        public string TypeDescription { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public bool Financed { get; set; }
        public short InstallmentsNumber { get; set; }
        public bool Closed { get; set; }
        public List<InstallmentDto> Installments { get; set; } = new();
    }
}
