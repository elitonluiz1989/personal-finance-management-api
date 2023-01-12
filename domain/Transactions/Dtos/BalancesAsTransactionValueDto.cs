namespace PersonalFinanceManagement.Domain.Transactions.Dtos
{
    public class BalancesAsTransactionValueDto
    {
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public int[] BalancesIds { get; set; } = Array.Empty<int>();
    }
}
