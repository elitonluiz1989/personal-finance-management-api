using Microsoft.AspNetCore.Mvc;
using PersonalFinanceManagement.Api.Controllers.Base;
using PersonalFinanceManagement.Domain.Base.Contracts;
using PersonalFinanceManagement.Domain.Transactions.Contracts;
using PersonalFinanceManagement.Domain.Transactions.Dtos;

namespace PersonalFinanceManagement.Api.Controllers.Transactions
{
    public class TransactionsController : BaseApiController
    {
        public TransactionsController(
            INotificationService notificationService,
            IUnitOfWork unitOfWork
        )
            : base(notificationService, unitOfWork)
        {
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
    }
}
