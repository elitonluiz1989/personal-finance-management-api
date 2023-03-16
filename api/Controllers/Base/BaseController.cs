using Microsoft.AspNetCore.Mvc;
using PersonalFinanceManagement.Domain.Base.Contracts;

namespace PersonalFinanceManagement.Api.Controllers.Base
{
    public class BaseController : Controller
    {
        protected IHttpContextAccessor _httpContextAccessor;
        private readonly INotificationService _notificationService;
        private readonly IUnitOfWork _unitOfWork;

        protected BaseController(
            IHttpContextAccessor httpContextAccessor,
            INotificationService notificationService,
            IUnitOfWork unitOfWork
        )
        {
            _httpContextAccessor = httpContextAccessor;
            _notificationService = notificationService;
            _unitOfWork = unitOfWork;
        }

        protected bool HasNotifications()
        {
            return _notificationService.HasNotifications();
        }

        protected IActionResult ResponseWithNotifications()
        {
            return BadRequest(_notificationService.GetNotifications());
        }

        protected IActionResult ResponseWithCommit()
        {
            _unitOfWork.Commit();

            return Ok(true);
        }

        protected IActionResult ResponseWithCommit<T>(T result)
        {
            _unitOfWork.Commit();

            return Ok(result);
        }
    }
}
