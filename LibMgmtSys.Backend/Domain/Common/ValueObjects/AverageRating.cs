using LibMgmtSys.Backend.Domain.Common.Models;
using LibMgmtSys.Backend.Domain.Common.Models.Identities;

namespace LibMgmtSys.Backend.Domain.Common.ValueObjects
{
    public sealed class AverageRating : ValueObject
    {
        private AverageRating(double value, int numberOfRatings)
        {
            Value = value;
            NumberOfRatings = numberOfRatings;
        }
        public double Value { get; private set; }
        public int NumberOfRatings { get; private set; }

        public static AverageRating CreateNew(double rating = 0, int numberOfRatings = 0)
        {
            return new AverageRating(rating, numberOfRatings);
        }

        public void AddNewRating(Rating rating)
        {
            Value = ((Value * NumberOfRatings) + rating.Value) / ++NumberOfRatings;
        }

        public void RemoveRating(Rating rating)
        {
            Value = ((Value * NumberOfRatings) - rating.Value) / --NumberOfRatings;
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}