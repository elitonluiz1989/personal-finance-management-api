using Microsoft.Extensions.Configuration;
using PersonalFinanceManagement.Application.Dtos.Authentication;

namespace PersonalFinanceManagement.Application.Contracts
{
    public interface IAuthenticationService
    {
        Task<AuthenticatedDto?> Authenticate(AuthenticationDto dto, IConfiguration configuration);
        Task<AuthenticationRefreshedDto?> Refresh(AuthenticationRefreshDto dto, IConfiguration configuration);
    }
}
