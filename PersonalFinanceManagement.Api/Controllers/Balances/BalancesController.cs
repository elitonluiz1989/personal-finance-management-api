using Microsoft.AspNetCore.Mvc;
using PersonalFinanceManagement.Api.Attributes;
using PersonalFinanceManagement.Api.Controllers.Base;
using PersonalFinanceManagement.Domain.Balances.Contracts.Balances;
using PersonalFinanceManagement.Domain.Balances.Dtos;
using PersonalFinanceManagement.Domain.Balances.Filters;
using PersonalFinanceManagement.Domain.Base.Contracts;
using PersonalFinanceManagement.Domain.Users.Enums;

namespace PersonalFinanceManagement.Api.Controllers.Balances
{
    public class BalancesController : BaseApiController
    {
        public BalancesController(
            INotificationService notificationService,
            IUnitOfWork unitOfWork
        )
            : base(notificationService, unitOfWork)
        {
        }

        [HttpGet()]
        [RolesAuthorized(UserRoleEnum.Administrator, UserRoleEnum.User)]
        public async Task<IActionResult> Get(
            [FromQuery] BalanceFilter filter,
            [FromServices] IBalanceSpecification specification
        )
        {
            var results = await specification.Get(filter);

            return Ok(results);
        }

        [HttpPost]
        [RolesAuthorized(UserRoleEnum.Administrator)]
        public async Task<IActionResult> Post(
            [FromBody] BalanceStoreDto dto,
            [FromServices] IBalanceStore store
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
            [FromServices] IBalanceDeleter deleter
        )
        {
            await deleter.Delete(id);

            if (HasNotifications())
                return ResponseWithNotifications();

            return ResponseWithCommit();
        }
    }
}
