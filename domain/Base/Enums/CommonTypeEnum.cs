using System.ComponentModel;

namespace PersonalFinanceManagement.Domain.Base.Enums
{
    public enum CommonTypeEnum
    {
        [Description("Credit")]
        Credit = 1,
        [Description("Debt")]
        Debt
    }
}
