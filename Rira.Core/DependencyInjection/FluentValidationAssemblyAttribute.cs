namespace Rira.Core.DependencyInjection
{

    /// <summary>
    /// Assembly that contains services representing configuration sections must be marked with this attribute.
    /// In startup, assemblies with this attribute will be added to dependency injection
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly)]
    public class FluentValidationAssemblyAttribute : Attribute
    {
    }
}
