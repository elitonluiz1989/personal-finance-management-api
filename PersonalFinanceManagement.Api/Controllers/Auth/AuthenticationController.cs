using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalFinanceManagement.Application.Contracts;
using PersonalFinanceManagement.Application.Dtos.Authentication;

namespace PersonalFinanceManagement.Api.Controllers.Auth
{
    public class AuthenticationController : Controller
    {

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
