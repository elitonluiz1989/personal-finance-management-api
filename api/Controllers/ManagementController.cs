using Microsoft.AspNetCore.Mvc;
using PersonalFinanceManagement.Api.Attributes;
using PersonalFinanceManagement.Api.Controllers.Base;
using PersonalFinanceManagement.Application.Contracts;
using PersonalFinanceManagement.Domain.Base.Contracts;
using PersonalFinanceManagement.Domain.Managements.Contracts;
using PersonalFinanceManagement.Domain.Managements.Dtos;
using PersonalFinanceManagement.Domain.Users.Contracts;
using PersonalFinanceManagement.Domain.Users.Enums;

namespace PersonalFinanceManagement.Api.Controllers
{
    [ApiController]
    public class ManagementController : BaseApiController
    {
        public ManagementController(
            IHttpContextAccessor httpContextAccessor,
            INotificationService notificationService,
            IUnitOfWork unitOfWork,
            IUserRepository userRepository
        )
            : base(httpContextAccessor, notificationService, unitOfWork, userRepository)
        {
        }

        [HttpGet("{reference}")]
        [RolesAuthorized(UserRoleEnum.Administrator)]
        public async Task<IActionResult> Manage(
            int reference,
            [FromServices] IManagementService service
        )
        {
            var results = await service.List(reference, AuthenticatedUserId, IsAdmin);

            return Ok(results);
        }

        [HttpPost]
        public async Task<IActionResult> Post(
            ManagementStoreDto dto,
            [FromServices] IManagementStore store
        )
        {
            await store.Store(dto);

            if (HasNotifications())
                return ResponseWithNotifications();

            return ResponseWithCommit();
        }
    }
}
