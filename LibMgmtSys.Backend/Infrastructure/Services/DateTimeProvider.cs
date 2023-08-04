using LibMgmtSys.Backend.Application.Common.Interfaces.Services;

namespace LibMgmtSys.Backend.Infrastructure.Services
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}

