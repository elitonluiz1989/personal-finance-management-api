using Microsoft.AspNetCore.Mvc;
using PersonalFinanceManagement.Domain.Base.Contracts;
using PersonalFinanceManagement.Domain.Users.Contracts;
using PersonalFinanceManagement.Domain.Users.Enums;

namespace PersonalFinanceManagement.Api.Controllers.Base
{
    [ApiController]
    [Route("[controller]")]
    public abstract class BaseApiController : BaseController
    {
        protected int AuthenticatedUserId { get; private set; }
        protected bool IsAdmin { get; private set; }
        private readonly IUserRepository _userRepository;

        protected BaseApiController(
            IHttpContextAccessor httpContextAccessor,
            INotificationService notificationService,
            IUnitOfWork unitOfWork,
            IUserRepository userRepository
        )
            : base(httpContextAccessor, notificationService, unitOfWork)
        {
            _userRepository = userRepository;

            DefineVariables();
        }

        private void SetAuthenticatedUserId()
        {
            if (_httpContextAccessor?.HttpContext is null)
                return;

            var userIdValue = _httpContextAccessor.HttpContext.Items.FirstOrDefault(i => i.Key.ToString() == "UserId").Value;

            if (userIdValue is null)
                return;

            var success = int.TryParse(userIdValue.ToString(), out int userId);

            if (success is false)
                return;

            AuthenticatedUserId = userId;
        }

        private void SetIfIsAdmin()
        {
            if (AuthenticatedUserId == 0)
                return;

            var userRole = _userRepository.GetUserRole(AuthenticatedUserId);

            IsAdmin = userRole is UserRoleEnum.Administrator;
        }

        private void DefineVariables()
        {
            SetAuthenticatedUserId();
            SetIfIsAdmin();
        }
    }
}
