using PersonalFinanceManagement.Domain.Users.Dtos;

namespace PersonalFinanceManagement.Domain.Transactions.Dtos
{
    public record TransactionForListingDto : TransactionDto
    {
        public UserDto? User { get; set; }
        public List<TransactionItemWithInstallmentDto> Items { get; set; } = new();
    }
}
