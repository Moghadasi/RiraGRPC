namespace Rira.Core.DependencyInjection
{
    /// <summary>
    /// Every service class that must be added to DependencyResolver as itself
    /// (for example class A as class A, not its interfaces) must implement this empty interface
    /// </summary>
    public interface IScopedSelfService
    {
    }

    /// <summary>
    /// Assembly that contains services representing themselves must be marked with this attribute.
    /// In startup, assemblies with this attribute will be added for dependency injection
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly)]
    public class ScopedSelfServicesAssemblyAttribute : Attribute
    {
    }
}
