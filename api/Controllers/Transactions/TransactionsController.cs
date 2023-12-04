using Microsoft.AspNetCore.Mvc;
using PersonalFinanceManagement.Api.Controllers.Base;
using PersonalFinanceManagement.Domain.Base.Contracts;
using PersonalFinanceManagement.Domain.Transactions.Contracts;
using PersonalFinanceManagement.Domain.Transactions.Dtos;
using PersonalFinanceManagement.Domain.Transactions.Filters;
using PersonalFinanceManagement.Domain.Users.Contracts;

namespace PersonalFinanceManagement.Api.Controllers.Transactions
{
    public class TransactionsController : BaseApiController
    {
        public TransactionsController(
            IHttpContextAccessor httpContextAccessor,
            INotificationService notificationService,
            IUnitOfWork unitOfWork,
            IUserRepository userRepository
        )
            : base(httpContextAccessor, notificationService, unitOfWork, userRepository)
        {
        }

        [HttpGet]
        public async Task<IActionResult> Get(
            [FromQuery] TransactionFilter filter,
            [FromServices] ITransactionWithTransactionItemsSpecification specification
        )
        {
            var results = await specification
                .WithPagination(filter)
                .PagedList();

            return Ok(results);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(
            int id,
            [FromServices] ITransactionWithTransactionItemsSpecification specification
        )
        {
            var result = await specification
                .WithFilter(new TransactionFilter() { Id = id }, AuthenticatedUserId, IsAdmin)
                .First();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(
            TransactionStoreDto dto,
            [FromServices] ITransactionStore store
        )
        {
            await store.Store(dto);

            if (HasNotifications())
                return ResponseWithNotifications();

            return ResponseWithCommit();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(
            int id,
            [FromServices] ITransactionDeleter deleter
        )
        {
            await deleter.Delete(id);

            if (HasNotifications())
                return ResponseWithNotifications();

            return ResponseWithCommit();
        }
    }
}
