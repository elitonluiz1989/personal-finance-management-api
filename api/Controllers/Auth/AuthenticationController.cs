using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalFinanceManagement.Api.Controllers.Base;
using PersonalFinanceManagement.Application.Contracts;
using PersonalFinanceManagement.Application.Dtos.Authentication;
using PersonalFinanceManagement.Domain.Base.Contracts;

namespace PersonalFinanceManagement.Api.Controllers.Auth
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : BaseController
    {
        public AuthenticationController(
            IHttpContextAccessor httpContextAccessor,
            INotificationService notificationService,
            IUnitOfWork unitOfWork
        )
            : base(httpContextAccessor, notificationService, unitOfWork)
        {
        }

        [HttpPost("Authenticate")]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate(
            [FromBody] AuthenticationDto dto,
            [FromServices] IConfiguration configuration,
            [FromServices] IAuthenticationService authenticationService
        )
        {
            var result = await authenticationService.Authenticate(dto, configuration);

            if (HasNotifications())
                return ResponseWithNotifications();

            return ResponseWithCommit(result);
        }

        [HttpPost("Refresh")]
        [AllowAnonymous]
        public async Task<IActionResult> Refresh(
            [FromBody] AuthenticationRefreshDto dto,
            [FromServices] IConfiguration configuration,
            [FromServices] IAuthenticationService authenticationService
        )
        {
            var result = await authenticationService.Refresh(dto, configuration);

            if (HasNotifications())
                return ResponseWithNotifications();

            return ResponseWithCommit(result);
        }
    }
}
