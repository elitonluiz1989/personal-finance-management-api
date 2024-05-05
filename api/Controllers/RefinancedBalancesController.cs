using Microsoft.AspNetCore.Mvc;
using PersonalFinanceManagement.Api.Attributes;
using PersonalFinanceManagement.Api.Controllers.Base;
using PersonalFinanceManagement.Domain.Balances.Contracts.RefinanceBalances;
using PersonalFinanceManagement.Domain.Balances.Dtos;
using PersonalFinanceManagement.Domain.Base.Contracts;
using PersonalFinanceManagement.Domain.Users.Contracts;
using PersonalFinanceManagement.Domain.Users.Enums;

namespace PersonalFinanceManagement.Api.Controllers
{
    public class RefinancedBalancesController : BaseApiController
    {
        public RefinancedBalancesController(
            IHttpContextAccessor httpContextAccessor,
            INotificationService notificationService,
            IUnitOfWork unitOfWork,
            IUserRepository userRepository
        )
            : base(httpContextAccessor, notificationService, unitOfWork, userRepository)
        {
        }

        [HttpPost]
        [RolesAuthorized(UserRoleEnum.Administrator)]
        public async Task<IActionResult> Post(
            [FromBody] RefinancedBalanceStoreDto dto,
            [FromServices] IRefinancedBalanceStore store
        )
        {
            await store.Store(dto, AuthenticatedUserId);

            if (HasNotifications())
                return ResponseWithNotifications();

            return ResponseWithCommit();
        }
    }
}
