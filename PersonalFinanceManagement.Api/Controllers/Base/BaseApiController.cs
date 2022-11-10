using Microsoft.AspNetCore.Mvc;
using PersonalFinanceManagement.Domain.Base.Contracts;

namespace PersonalFinanceManagement.Api.Controllers.Base
{
    [ApiController]
    [Route("[controller]")]
    public abstract class BaseApiController : Controller
    {
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
    }
}
