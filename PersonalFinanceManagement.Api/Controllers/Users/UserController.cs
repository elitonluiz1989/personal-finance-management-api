using Microsoft.AspNetCore.Mvc;
using PersonalFinanceManagement.Api.Attributes;
using PersonalFinanceManagement.Domain.Base.Contracts;
using PersonalFinanceManagement.Domain.Users.Contracts;
using PersonalFinanceManagement.Domain.Users.Dtos;
using PersonalFinanceManagement.Domain.Users.Enums;
using PersonalFinanceManagement.Domain.Users.Filters;

namespace PersonalFinanceManagement.Api.Controllers.Users
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        [HttpGet()]
        [Authorized(UserRoleEnum.Administrator)]
        public async Task<IActionResult> Get(
            [FromQuery] UserFilter filter,
            [FromServices] IUserSpecification specification
        )
        {
            var results = await specification.Get(filter);

            return Ok(results);
        }

        [HttpPost]
        [Authorized(UserRoleEnum.Administrator)]
        public async Task<IActionResult> Post(
            [FromBody] UserStoreDto dto,
            [FromServices] IUserStore store,
            [FromServices] IUnitOfWork unitOfWork
        )
        {
            await store.Store(dto);
            unitOfWork.Commit();

            return Ok(true);
        }

        [HttpDelete("{id}")]
        [Authorized(UserRoleEnum.Administrator)]
        public async Task<IActionResult> Patch(
            int id,
            [FromServices] IUserDeleter deleter,
            [FromServices] IUnitOfWork unitOfWork
        )
        {
            await deleter.Delete(id);
            unitOfWork.Commit();

            return Ok(true);
        }
    }
}
