namespace Rira.Core.DependencyInjection
{
    /// <summary>
    /// Every contract (that a class must implement it) must extend from this interface so
    /// DependencyResolver will find and register it &amp; its implemented class automatically
    /// </summary>
    public interface IScopedContractService
    {
    }

    /// <summary>
    /// Assembly that contains services implementing contracts must be marked with this attribute.
    /// In startup, assemblies with this attribute will be added to dependency injection
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly)]
    public class ScopedContractServicesAssemblyAttribute : Attribute
    {
    }
}
