namespace Rira.Core.DependencyInjection
{
    /// <summary>
    /// Mediator handlers must be in assemblies attributed with this attribute,
    /// because asp.net wap is configured to scan these assemblies for controllers
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly)]
    public class MediatorAssemblyAttribute : Attribute
    {
    }
}
