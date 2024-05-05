namespace PersonalFinanceManagement.Domain.Base.Extensions
{
    public static class DateTimeExtension
    {
        public static int ToReference(this DateTime dateTime)
        {
            return Convert.ToInt32(dateTime.ToString("yyyyMM"));
        }
    }
}
