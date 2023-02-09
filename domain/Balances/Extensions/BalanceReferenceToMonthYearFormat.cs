namespace PersonalFinanceManagement.Domain.Balances.Extensions
{
    public static class BalanceReferenceToMonthYearFormat
    {
        public static string ToMonthYear(this int reference)
        {
            var referenceStr = reference.ToString();

            return $@"{referenceStr[4..6]}/{referenceStr[..4]}";
        }
    }
}
