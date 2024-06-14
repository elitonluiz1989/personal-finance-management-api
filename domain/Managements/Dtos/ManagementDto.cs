using PersonalFinanceManagement.Domain.Base.Dtos;
using PersonalFinanceManagement.Domain.Users.Dtos;

namespace PersonalFinanceManagement.Domain.Managements.Dtos
{
    public record ManagementDto : RecordedDto<int>
    {
        public UserDto? User { get; set; }
        public List<ManagementItemDto> Items { get; set; } = new();
        public ManagementTotalDto? Total { get; set; }
    }
}
