using PersonalFinanceManagement.Application.Dtos.Authentication;

namespace PersonalFinanceManagement.Application.Contracts
{
    public interface IAuthenticationService
    {
        Task<AuthenticatedDto?> Authenticate(AuthenticationDto dto);
    }
}
