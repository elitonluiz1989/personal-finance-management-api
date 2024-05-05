using PersonalFinanceManagement.Domain.Users.Dtos;

namespace PersonalFinanceManagement.Domain.Management.Dtos
{
    public record ManagementDto
    {
        public UserDto? User { get; set; }
        public List<ManagementItemDto> Items { get; set; } = new();
        public ManagementTotalDto Total => new(Items);
    }
}
