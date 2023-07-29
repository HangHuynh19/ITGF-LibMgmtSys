using LibMgmtSys.Backend.Domain.Common.Models;

namespace LibMgmtSys.Backend.Domain.LoanAggregate.ValueObjects
{
    public class LoanId : ValueObject
    {
        public Guid Value { get; }

        private LoanId(Guid value)
        {
            Value = value;
        }

        public static LoanId CreateUnique()
        {
            return new LoanId(Guid.NewGuid());
        }

        public static LoanId Create(Guid loanId)
        {
            return new LoanId(loanId);
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}