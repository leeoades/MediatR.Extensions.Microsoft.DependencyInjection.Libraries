using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace MediatR.Extensions.Microsoft.DependencyInjection.Libraries.Ext
{
    public static partial class ServiceCollectionEx
    {
        public static IServiceCollection AddMeditatRLibrary(this IServiceCollection serviceCollection, params Assembly[] assemblies)
            => serviceCollection.AddMeditatRLibrary(configuration: null, assemblies);

        public static IServiceCollection AddMeditatRLibrary(this IServiceCollection serviceCollection, params Type[] handlerAssemblyMarkerTypes)
            => serviceCollection.AddMeditatRLibrary(configuration: null, handlerAssemblyMarkerTypes);

        public static IServiceCollection AddMeditatRLibrary(this IServiceCollection serviceCollection, Action<MediatRServiceConfiguration> configuration, params Type[] handlerAssemblyMarkerTypes)
            => serviceCollection.AddMeditatRLibrary(configuration, handlerAssemblyMarkerTypes.Select(t => t.GetTypeInfo().Assembly).ToArray());


        public static IServiceCollection AddMeditatRLibrary(this IServiceCollection serviceCollection, Action<MediatRServiceConfiguration> configuration, params Assembly[] assemblies)
        {
            MediatRLibraryRegistrar.AddAssemblies(assemblies, configuration);
            return serviceCollection;
        }
    }
    
    public static partial class ServiceCollectionEx
    {
        public static IServiceCollection AddMediatRIncludingLibraries(this IServiceCollection serviceCollection, params Type[] assemblyMarkerTypes)
            => serviceCollection.AddMediatRIncludingLibraries(configuration: null, assemblyMarkerTypes);
        
        public static IServiceCollection AddMediatRIncludingLibraries(this IServiceCollection serviceCollection, Action<MediatRServiceConfiguration> configuration, params Type[] assemblyMarkerTypes)
        {
            return serviceCollection
                .AddMeditatRLibrary(configuration, assemblyMarkerTypes)
                .AddMediatRIncludingLibraries();
        }

        public static IServiceCollection AddMediatRIncludingLibraries(this IServiceCollection serviceCollection, params Assembly[] assemblies)
            => serviceCollection.AddMediatRIncludingLibraries(configuration: null, assemblies);
        
        public static IServiceCollection AddMediatRIncludingLibraries(this IServiceCollection serviceCollection, Action<MediatRServiceConfiguration> configuration, params Assembly[] assemblies)
        {
            return serviceCollection
                .AddMeditatRLibrary(configuration, assemblies)
                .AddMediatRIncludingLibraries();
        }

        public static IServiceCollection AddMediatRIncludingLibraries(this IServiceCollection serviceCollection, IEnumerable<Assembly> assemblies, Action<MediatRServiceConfiguration> configuration)
        {
            return serviceCollection
                .AddMeditatRLibrary(configuration, assemblies.ToArray())
                .AddMediatRIncludingLibraries();
        }

        public static IServiceCollection AddMediatRIncludingLibraries(this IServiceCollection serviceCollection)
        {
            var isMediatRAlreadyRegistered = serviceCollection.Any(c => c.ServiceType == typeof(IMediator));
            if (isMediatRAlreadyRegistered) throw new Exception($"MediatR is already registered in the container. {nameof(AddMediatRIncludingLibraries)} can not run.");

            return serviceCollection.AddMediatR(MediatRLibraryRegistrar.GetAssemblies().ToArray(), MediatRLibraryRegistrar.GetConfigurationActions());
        }
    }
}