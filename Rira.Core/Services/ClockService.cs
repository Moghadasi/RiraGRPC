using Rira.Core.Contracts;

namespace Rira.Core.Services
{
    /// <inheritdoc />
    public class ClockService : IClockService
    {
        /// <inheritdoc />
        [System.Diagnostics.DebuggerNonUserCode]
        public DateTime Now(string? reason = null) => DateTime.UtcNow;
    }
}
