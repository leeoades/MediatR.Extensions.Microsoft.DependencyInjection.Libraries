using System;
using MediatR.Extensions.Microsoft.DependencyInjection.Libraries.Ext;
using Microsoft.Extensions.DependencyInjection;

namespace MediatR.TestLibrary.Ext
{
    public static class ServiceCollectionEx
    {
        public static IServiceCollection AddTestLibraryByMarkerType(this IServiceCollection serviceCollection)
        {
            return serviceCollection
                .AddMeditatRLibrary(typeof(FooRequestHandler));
        }
        
        public static IServiceCollection AddTestLibraryByAssembly(this IServiceCollection serviceCollection)
        {
            return serviceCollection
                .AddMeditatRLibrary(typeof(FooRequestHandler).Assembly);
        }
        
        public static IServiceCollection AddTestLibraryByAssemblyWithConfig(this IServiceCollection serviceCollection, Action<MediatRServiceConfiguration> configuration)
        {
            return serviceCollection
                .AddMeditatRLibrary(configuration, typeof(FooRequestHandler).Assembly);
        }
    }
}