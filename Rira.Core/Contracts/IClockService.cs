using Rira.Core.DependencyInjection;

namespace Rira.Core.Contracts;

/// <summary>
/// 
/// </summary>
public interface IClockService : ISingletonContractService
{
    /// <summary>
    /// Regardless of what provided for reason, always returns value of DateTime.UtcNow.
    /// But in tests, you can customize time to return based on reason parameter
    /// </summary>
    DateTime Now(string? reason = null);
}