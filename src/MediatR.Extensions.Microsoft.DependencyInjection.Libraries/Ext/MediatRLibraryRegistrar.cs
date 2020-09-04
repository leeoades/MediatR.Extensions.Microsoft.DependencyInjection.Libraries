using System.Collections.Generic;
using System.Reflection;

namespace MediatR.Extensions.Microsoft.DependencyInjection.Libraries.Ext
{
    public static class MediatRLibraryRegistrar
    {
        private static readonly List<Assembly> Assemblies = new List<Assembly>();

        public static void AddAssemblies(Assembly[] assemblies)
        {
            Assemblies.AddRange(assemblies);
        }

        public static IEnumerable<Assembly> GetAssemblies() => Assemblies;
    }
}