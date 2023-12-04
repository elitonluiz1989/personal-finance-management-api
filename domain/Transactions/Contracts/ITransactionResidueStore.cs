using PersonalFinanceManagement.Domain.Transactions.Entities;

namespace PersonalFinanceManagement.Domain.Transactions.Contracts
{
    public interface ITransactionResidueStore
    {
        TransactionResidue? Store(TransactionItem transactionItemOrigin, TransactionItem transactionItem);
    }
}
