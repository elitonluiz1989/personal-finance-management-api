using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalFinanceManagement.Api.Controllers.Base;
using PersonalFinanceManagement.Application.Contracts;
using PersonalFinanceManagement.Application.Dtos.Authentication;
using PersonalFinanceManagement.Domain.Base.Contracts;

namespace PersonalFinanceManagement.Api.Controllers.Auth
{
    public class AuthenticationController : BaseApiController
    {
        public AuthenticationController(
            INotificationService notificationService,
            IUnitOfWork unitOfWork
        )
            : base(notificationService, unitOfWork)
        {
        }

        [HttpPost("Authenticate")]
        [AllowAnonymous]
        public async Task<ActionResult<AuthenticatedDto>> Authenticate(
            [FromBody] AuthenticationDto dto,
            [FromServices] IConfiguration configuration,
            [FromServices] IAuthenticationService authenticationService
        )
        {
            if (dto == null)
                return BadRequest();

            dto.AppSecret = configuration.GetValue<string>("Jwt:Secret");
            dto.ExpirationDelay = configuration.GetValue<uint>("Jwt:ExprirationDelay");

            var result = await authenticationService.Authenticate(dto);

            return Ok(result);
        }
    }
}
