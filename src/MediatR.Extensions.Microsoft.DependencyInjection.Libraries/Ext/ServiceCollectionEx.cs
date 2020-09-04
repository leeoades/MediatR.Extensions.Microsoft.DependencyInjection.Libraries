using System;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace MediatR.Extensions.Microsoft.DependencyInjection.Libraries.Ext
{
    public static class ServiceCollectionEx
    {
        public static IServiceCollection AddMeditatRLibrary(this IServiceCollection serviceCollection, params Assembly[] assemblies)
        {
            MediatRLibraryRegistrar.AddAssemblies(assemblies);
            return serviceCollection;
        }
        
        public static IServiceCollection AddMeditatRLibrary(this IServiceCollection serviceCollection, params Type[] assemblyMarkerTypes) 
            => serviceCollection.AddMeditatRLibrary(assemblyMarkerTypes.Select(t => t.GetTypeInfo().Assembly).ToArray());

        public static IServiceCollection AddMediatRIncludingLibraries(this IServiceCollection serviceCollection, params Type[] assemblyMarkerTypes)
        {
            return serviceCollection
                .AddMeditatRLibrary(assemblyMarkerTypes)
                .AddMediatRIncludingLibraries();
        }

        public static IServiceCollection AddMediatRIncludingLibraries(this IServiceCollection serviceCollection, params Assembly[] assemblies)
        {
            return serviceCollection
                .AddMeditatRLibrary(assemblies)
                .AddMediatRIncludingLibraries();
        }
        public static IServiceCollection AddMediatRIncludingLibraries(this IServiceCollection serviceCollection)
        {
            var isMediatRAlreadyRegistered = serviceCollection.Any(c => c.ServiceType == typeof(IMediator));
            if (isMediatRAlreadyRegistered) throw new Exception($"MediatR is already registered in the container. {nameof(AddMediatRIncludingLibraries)} can not run.");

            return serviceCollection.AddMediatR(MediatRLibraryRegistrar.GetAssemblies().ToArray());
        }
    }
}