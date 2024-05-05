namespace PersonalFinanceManagement.Domain.Balances.Extensions
{
    public static class BalanceReferenceToMonthYearFormatExtension
    {
        public static string ToMonthYear(this int reference)
        {
            var referenceStr = reference.ToString();

            return $@"{referenceStr[4..6]}/{referenceStr[..4]}";
        }

        public static DateTime ToDateTime(this int reference)
        {
            var referenceStr = reference.ToString();

            int year = Convert.ToInt32(referenceStr[..4]);
            int month = Convert.ToInt32(referenceStr[4..6]);

            return new DateOnly(year, month, 1).ToDateTime(TimeOnly.MinValue);
        }
    }
}
