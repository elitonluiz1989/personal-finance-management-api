using System.ComponentModel;

namespace PersonalFinanceManagement.Domain.Balances.Enums
{
    public enum InstallmentStatusEnum
    {
        [Description("Created")]
        Created = 1,
        [Description("Paid")]
        Paid,
        [Description("Partially Paid")]
        PartiallyPaid
    }
}
