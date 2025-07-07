namespace Rira.Core.DependencyInjection
{
    /// <summary>
    /// </summary>
    public interface ISingletonContractService
    {
    }
    /// <summary>
    /// Assembly that contains singleton services must be marked with this attribute.
    /// In startup, assemblies with this attribute will be added to dependency injection
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly)]
    public class SingletonContractServicesAssemblyAttribute : Attribute
    {
    }
}
