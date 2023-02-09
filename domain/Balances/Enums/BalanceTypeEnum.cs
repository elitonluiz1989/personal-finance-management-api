using System.ComponentModel;

namespace PersonalFinanceManagement.Domain.Balances.Enums
{
    public enum BalanceTypeEnum
    {
        [Description("Credit")]
        Credit = 1,
        [Description("Debt")]
        Debt
    }
}
