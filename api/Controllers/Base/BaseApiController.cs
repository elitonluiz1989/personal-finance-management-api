using Microsoft.AspNetCore.Mvc;
using PersonalFinanceManagement.Domain.Base.Contracts;

namespace PersonalFinanceManagement.Api.Controllers.Base
{
    [ApiController]
    [Route("[controller]")]
    public abstract class BaseApiController : Controller
    {
        protected int AuthUserId => GetAuthUserId();
        private readonly INotificationService _notificationService;
        private readonly IUnitOfWork _unitOfWork;

        protected BaseApiController(
            INotificationService notificationService,
            IUnitOfWork unitOfWork
        )
        {
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

        private int GetAuthUserId()
        {
            if (HttpContext is null)
                return default;

            var userIdValue = HttpContext.Items.FirstOrDefault(i => i.Key.ToString() == "UserId").Value;

            if (userIdValue is null)
                return default;

            var success = int.TryParse(userIdValue.ToString(), out int userId);

            if (success is false)
                return default;

            return userId;
        }
    }
}
