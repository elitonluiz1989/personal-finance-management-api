using System.Text.Json.Serialization;

namespace PersonalFinanceManagement.Domain.Balances.Dtos
{
    public record RefinancedBalanceStoreDto
    {
        [JsonIgnore]
        public int UserId { get; set; }
        public int BalanceId { get; set; }
        public DateTime Date { get; set; }
        public decimal Value { get; set; }
        public bool Financed { get; set; }
        public short InstallmentsNumber { get; set; }
    }
}
