using PersonalFinanceManagement.Domain.Transactions.Enums;
using PersonalFinanceManagement.Domain.Users.Dtos;

namespace PersonalFinanceManagement.Domain.Transactions.Dtos
{
    public record TransactionDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public UserDto? User { get; set; }
        public TransactionTypeEnum Type { get; set; }
        public DateTime Date { get; set; }
        public decimal Value { get; set; }
    }
}
