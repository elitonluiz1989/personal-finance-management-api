using PersonalFinanceManagement.Domain.Base.Contracts;
using PersonalFinanceManagement.Domain.Base.Services;
using PersonalFinanceManagement.Domain.Transactions.Contracts;
using PersonalFinanceManagement.Domain.Transactions.Entities;

namespace PersonalFinanceManagement.Domain.Transactions.Services
{
    public class TransactionResidueStore : Store<TransactionResidue, int>, ITransactionResidueStore
    {
        public TransactionResidueStore(
            INotificationService notificationService,
            ITransactionResidueRepository repository
        )
            : base(notificationService, repository)
        {
        }

        public TransactionResidue? Store(TransactionItem transactionItemOrigin, TransactionItem transactionItem)
        {
            var transactionResidue = new TransactionResidue
            {
                TransactionItemOrigin = transactionItemOrigin,
                TransactionItem = transactionItem
            };

            if (ValidateEntity(transactionResidue) is false)
                return default;

            return SaveEntity(transactionResidue);
        }
    }
}
