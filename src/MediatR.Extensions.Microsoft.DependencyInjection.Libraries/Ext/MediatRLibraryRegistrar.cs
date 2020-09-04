using System;
using System.Collections.Generic;
using System.Reflection;

namespace MediatR.Extensions.Microsoft.DependencyInjection.Libraries.Ext
{
    public static class MediatRLibraryRegistrar
    {
        private static readonly List<Assembly> Assemblies = new List<Assembly>();
        private static readonly List<Action<MediatRServiceConfiguration>> ConfigActions = new List<Action<MediatRServiceConfiguration>>();

        public static void AddAssemblies(IEnumerable<Assembly> assemblies)
        {
            Assemblies.AddRange(assemblies);
        }

        public static void AddAssemblies(IEnumerable<Assembly> assemblies, Action<MediatRServiceConfiguration> configuration)
        {
            AddAssemblies(assemblies);
            if (configuration != null)
                ConfigActions.Add(configuration);
        }
        public static IEnumerable<Assembly> GetAssemblies() => Assemblies;

        public static Action<MediatRServiceConfiguration> GetConfigurationActions() =>
            config =>
            {
                foreach (var action in ConfigActions)
                    action(config);
            };
    }
}