using Microsoft.AspNetCore.Mvc;
using PersonalFinanceManagement.Api.Attributes;
using PersonalFinanceManagement.Api.Controllers.Base;
using PersonalFinanceManagement.Domain.Balances.Contracts.Installments;
using PersonalFinanceManagement.Domain.Balances.Filters;
using PersonalFinanceManagement.Domain.Base.Contracts;
using PersonalFinanceManagement.Domain.Users.Contracts;
using PersonalFinanceManagement.Domain.Users.Enums;

namespace PersonalFinanceManagement.Api.Controllers.Balances
{
    public class InstallmentsController : BaseApiController
    {
        public InstallmentsController(
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
            [FromQuery] InstallmentFilter filters,
            [FromServices] IInstallmentSpecification specification
        )
        {
            var results = await specification
                .WithFilter(filters, AuthenticatedUserId, IsAdmin)
                .WithPagination(filters)
                .PagedList();

            return Ok(results);
        }
    }
}
