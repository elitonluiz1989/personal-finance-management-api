using Microsoft.AspNetCore.Mvc;
using PersonalFinanceManagement.Api.Attributes;
using PersonalFinanceManagement.Api.Controllers.Base;
using PersonalFinanceManagement.Domain.Base.Contracts;
using PersonalFinanceManagement.Domain.Users.Contracts;
using PersonalFinanceManagement.Domain.Users.Dtos;
using PersonalFinanceManagement.Domain.Users.Enums;
using PersonalFinanceManagement.Domain.Users.Filters;

namespace PersonalFinanceManagement.Api.Controllers.Users
{
    public class UsersController : BaseApiController
    {
        public UsersController(
            INotificationService notificationService,
            IUnitOfWork unitOfWork
        )
            : base(notificationService, unitOfWork)
        {
        }

        [HttpGet()]
        [RolesAuthorized(UserRoleEnum.Administrator)]
        public async Task<IActionResult> Get(
            [FromQuery] UserFilter filter,
            [FromServices] IUserSpecification specification
        )
        {
            var results = await specification.Get(filter);

            return Ok(results);
        }

        [HttpPost]
        [RolesAuthorized(UserRoleEnum.Administrator)]
        public async Task<IActionResult> Post(
            [FromBody] UserStoreDto dto,
            [FromServices] IUserStore store,
            [FromServices] IUnitOfWork unitOfWork
        )
        {
            await store.Store(dto);

            if (HasNotifications())
                return ResponseWithNotifications();
            
            return ResponseWithCommit();
        }

        [HttpDelete("{id}")]
        [RolesAuthorized(UserRoleEnum.Administrator)]
        public async Task<IActionResult> Patch(
            int id,
            [FromServices] IUserDeleter deleter,
            [FromServices] IUnitOfWork unitOfWork
        )
        {
            await deleter.Delete(id);

            if (HasNotifications())
                return ResponseWithNotifications();

            return ResponseWithCommit();
        }
    }
}
