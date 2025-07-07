using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rira.Core.Utils;

namespace Rira.Core.DependencyInjection
{
    /// <summary>
    /// 
    /// </summary>
    public static class DependencyInjectionConfiguration
    {
        /// <summary>
        /// 
        /// </summary>
        public static void Setup(IServiceCollection container, IConfiguration configuration)
        {
            TypeUtility.LoadAssemblies();
            TypeUtility
                .Assemblies<ScopedContractServicesAssemblyAttribute>()
                .SelectMany(m => m.ExportedTypes)
                .PublicClasses()
                .Implementeds<IScopedContractService>()
                .AsContractService<IScopedContractService>(container, ServiceLifetime.Scoped);

            TypeUtility
                .Assemblies<ScopedSelfServicesAssemblyAttribute>()
                .SelectMany(m => m.ExportedTypes)
                .PublicClasses()
                .Implementeds<IScopedSelfService>()
                .AsSelfService(container, ServiceLifetime.Scoped);

            TypeUtility
                .Assemblies<SingletonContractServicesAssemblyAttribute>()
                .SelectMany(m => m.ExportedTypes)
                .PublicClasses()
                .Implementeds<ISingletonContractService>()
                .AsContractService<ISingletonContractService>(container, ServiceLifetime.Singleton);

            TypeUtility
                .Assemblies<SingletonSelfServicesAssemblyAttribute>()
                .SelectMany(m => m.ExportedTypes)
                .PublicClasses()
                .Implementeds<ISingletonSelfService>()
                .AsSelfService(container, ServiceLifetime.Singleton);

            // Mediator
            var mediatorAssemblies = TypeUtility.Assemblies<MediatorAssemblyAttribute>().ToArray();
            container.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(mediatorAssemblies));
        }
    }
}