using PersonalFinanceManagement.Domain.Base.Filters;

namespace PersonalFinanceManagement.Domain.Transactions.Filters
{
    public record TransactionFilter : Filter
    {
        public int Id { get; set; }
    }
}
