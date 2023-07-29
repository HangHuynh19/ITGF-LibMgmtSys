using Ardalis.SmartEnum;

namespace LibMgmtSys.Backend.Domain.BillAggregate.Enum
{
    public class BillingReason : SmartEnum<BillingReason>
    {
        public static readonly BillingReason LateReturn = new(nameof(LateReturn).ToLower(), 1);
        public static readonly BillingReason Lost = new(nameof(Lost).ToLower(), 2);
        public static readonly BillingReason Damaged = new(nameof(Damaged).ToLower(), 3);

        private BillingReason(string name, int value) : base(name, value)
        {
        }
    }
}