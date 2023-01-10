namespace PersonalFinanceManagement.Domain.Transactions.Dtos
{
    public class TransactionItemStorageDto
    {
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public bool CanGenerateCredit { get; set; } = true;
        public bool InstallmentAsTransactionAmount { get; set; }
    }
}
