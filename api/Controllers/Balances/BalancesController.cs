using Microsoft.AspNetCore.Mvc;
using PersonalFinanceManagement.Api.Attributes;
using PersonalFinanceManagement.Api.Controllers.Base;
using PersonalFinanceManagement.Domain.Balances.Contracts.Balances;
using PersonalFinanceManagement.Domain.Balances.Dtos;
using PersonalFinanceManagement.Domain.Balances.Filters;
using PersonalFinanceManagement.Domain.Base.Contracts;
using PersonalFinanceManagement.Domain.Users.Contracts;
using PersonalFinanceManagement.Domain.Users.Enums;

namespace PersonalFinanceManagement.Api.Controllers.Balances
{
    public class BalancesController : BaseApiController
    {
        public BalancesController(
            IHttpContextAccessor httpContextAccessor,
            INotificationService notificationService,
            IUnitOfWork unitOfWork,
            IUserRepository userRepository
        )
            : base(httpContextAccessor, notificationService, unitOfWork, userRepository)
        {
        }

        [HttpGet()]
        [RolesAuthorized(UserRoleEnum.Administrator, UserRoleEnum.User)]
        public async Task<IActionResult> Get(
            [FromQuery] BalanceFilter filters,
            [FromServices] IBalanceSpecification specification
        )
        {
            var results = await specification
                .WithFilter(filters, AuthenticatedUserId, IsAdmin)
                .List();

            return Ok(results);
        }

        [HttpGet("{id}")]
        [RolesAuthorized(UserRoleEnum.Administrator, UserRoleEnum.User)]
        public async Task<IActionResult> Get(
             int id,
            [FromServices] IBalanceSpecification specification
        )
        {
            var result = await specification
                .WithFilter(new BalanceFilter() { Id = id }, AuthenticatedUserId, IsAdmin)
                .First();

            return Ok(result);
        }

        [HttpPost]
        [RolesAuthorized(UserRoleEnum.Administrator)]
        public async Task<IActionResult> Post(
            [FromBody] BalanceStoreDto dto,
            [FromServices] IBalanceStore store
        )
        {
            var balance = await store.Store(dto);

            if (HasNotifications())
                return ResponseWithNotifications();

            return ResponseWithCommit(balance);
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
