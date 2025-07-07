using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Rira.Core.Utils
{
    /// <summary>
    /// 
    /// </summary>
    public static class TypeUtility
    {
        /// <summary>
        /// Returns all assemblies that has 
        /// </summary>
        /// <typeparam name="TAttribute"></typeparam>
        /// <returns></returns>
        public static IEnumerable<Assembly> Assemblies<TAttribute>()
            where TAttribute : Attribute
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                var assemblyName = assembly.FullName;
                var att = assembly.GetCustomAttribute<TAttribute>();
                if (att == null)
                    continue;
                yield return assembly;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TInterface"></typeparam>
        /// <param name="types"></param>
        /// <returns></returns>
        public static IEnumerable<Type> Implementeds<TInterface>(this IEnumerable<Type> types)
        {
            var interfaceName = typeof(TInterface).Name;
            var implemented = types.Where(t => t.GetInterface(interfaceName) != null);
            return implemented;
        }

        /// <summary>
        /// 
        /// </summary>
        public static IEnumerable<Type> PublicClasses(this IEnumerable<Type> types)
        {
            return types.Where(m => m.IsClass && m.IsPublic && !m.IsAbstract);
        }

        /// <summary>
        /// 
        /// </summary>
        public static void AsSelfService(this IEnumerable<Type> types, IServiceCollection container, ServiceLifetime lifeTime)
        {
            foreach (var type in types)
            {
                try
                {
                    container.Add(new ServiceDescriptor(type, type, lifeTime));
                }
                catch (Exception ex)
                {
                    throw new Exception($"Exception while adding Type {type.Name}. exception: {ex.Message}", ex);
                }
            }
        }


        /// <summary>
        /// 
        /// </summary>
        public static void AsContractService<TContractInterface>(this IEnumerable<Type> types, IServiceCollection container, ServiceLifetime lifeTime)
        {
            foreach (var type in types)
            {
                foreach (var @interface in type.GetInterfaces().Where(m => m != typeof(IDisposable) && m != typeof(TContractInterface)))
                {
                    try
                    {
                        container.Add(new ServiceDescriptor(@interface, type, lifeTime));
                    }
                    catch (Exception ex)
                    {
                        throw new Exception($"Exception while adding Type {type.Name} as implementation of {@interface.Name}. exception: {ex.Message}", ex);
                    }
                }
            }
        }


        /// <summary>
        /// Returns all types implementing interface <typeparamref name="TInterface"/> that are in assemblies marked with attribute <typeparamref name="TAssemblyAttribute"/>
        /// </summary>
        /// <typeparam name="TInterface"></typeparam>
        /// <typeparam name="TAssemblyAttribute"></typeparam>
        /// <returns></returns>
        public static IEnumerable<Type> Implementations<TInterface, TAssemblyAttribute>(bool usables = true, bool defaultConstructorInitializable = true) where TAssemblyAttribute : Attribute
        {
            List<Type> types = new();
            var interfaceName = typeof(TInterface).Name;
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (assembly.GetCustomAttribute<TAssemblyAttribute>() == null)
                    continue;
                types.AddRange(assembly
                    .ExportedTypes
                    .Where(x => x.GetInterface(interfaceName) != null)
                    .Where(x => Allowed(x, usables, defaultConstructorInitializable))
                );
            }

            return types;
        }

        /// <summary>
        /// checks type of conditions
        /// </summary>
        /// <param name="type"></param>
        /// <param name="usable">if type needs to be non-abstract class</param>
        /// <param name="defaultConstructorInitializable">if class must has public parameterless constructor</param>
        /// <returns></returns>
        private static bool Allowed(Type type, bool usable, bool defaultConstructorInitializable)
        {
            // if usable is not true, then every type can pass these conditions
            if (!usable)
                return true;

            // if type must be usable but type is not class or is abstract class, result of evaluation is false
            if (!type.IsClass || type.IsAbstract)
                return false;

            // if class need not to has public parameterless constructor, so every non-abstract class is passed
            if (!defaultConstructorInitializable)
                return true;

            var publicParameterlessConstructor = type.GetConstructor(Array.Empty<Type>());

            return publicParameterlessConstructor != null;
        }

        private static bool isAssembliesLoaded = false;

        /// <summary>
        /// 
        /// </summary>
        public static void LoadAssemblies()
        {
            if (isAssembliesLoaded == true)
                return;
            string searchFolder = AppDomain.CurrentDomain.BaseDirectory ?? throw new Exception("AppDomain.CurrentDomain.BaseDirectory is null");
            foreach (string fileLocation in Directory.GetFiles(searchFolder, "*.dll", SearchOption.TopDirectoryOnly))
            {
                if (fileLocation.EndsWith(".Views.dll"))
                    continue;
                string fileName = Path.GetFileNameWithoutExtension(fileLocation);
                if (IsAssemblyLoaded(fileName))
                {
                    continue;
                }
                else
                {
                    Assembly.Load(fileName);
                }
            }

            isAssembliesLoaded = true;

            static bool IsAssemblyLoaded(string jsutFileName)
            {
                return AppDomain.CurrentDomain.GetAssemblies().Any(a => a.FullName!.StartsWith(jsutFileName, StringComparison.InvariantCultureIgnoreCase));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string FriendlyName(this Type type)
        {
            if (type.IsGenericType)
            {
                var namePrefix = type.Name.Split(new[] { '`' }, StringSplitOptions.RemoveEmptyEntries)[0];
                var genericParameters = type.GetGenericArguments().Select(FriendlyName).ToCsv();
                return namePrefix + "<" + genericParameters + ">";
            }

            return type.Name;
        }

        private static string ToCsv(this IEnumerable<object> collectionToConvert, string separator = ", ")
        {
            return string.Join(separator, collectionToConvert.Select(o => o.ToString()));
        }
    }
}
