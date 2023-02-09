using System.ComponentModel;
using System.Reflection;

namespace PersonalFinanceManagement.Domain.Base.Extensions
{
    public static class EnumDescriptionExtension
    {
        public static string GetDescrition(this Enum value)
        {
            if (value is null)
                return string.Empty;

            FieldInfo? fi = value.GetType().GetField(value.ToString());

            if (fi is null)
                return string.Empty;

            DescriptionAttribute[]? attributes = fi.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

            if (attributes is not null && attributes.Any())
                return attributes.First().Description;

            return value.ToString();
        }
    }
}
