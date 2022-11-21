using PersonalFinanceManagement.Domain.Balances.Enums;
using PersonalFinanceManagement.Domain.Users.Dtos;
using System.Text.Json.Serialization;

namespace PersonalFinanceManagement.Domain.Balances.Dtos
{
    public record TransactionDto
    {
        public int Id { get; set; }
        [JsonIgnore]
        public int UserId { get; set; }
        public UserDto? User { get; set; }
        public TransactionTypeEnum Type { get; set; }
        public DateTime Date { get; set; }
        public decimal Value { get; set; }
    }
}
