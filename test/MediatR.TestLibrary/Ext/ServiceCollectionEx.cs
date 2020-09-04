using MediatR.Extensions.Microsoft.DependencyInjection.Libraries.Ext;
using Microsoft.Extensions.DependencyInjection;

namespace MediatR.TestLibrary.Ext
{
    public static class ServiceCollectionEx
    {
        public static IServiceCollection AddTestLibraryByType(this IServiceCollection serviceCollection)
        {
            return serviceCollection
                .AddMeditatRLibrary(typeof(FooRequestHandler));
        }
        
        public static IServiceCollection AddTestLibraryByAssembly(this IServiceCollection serviceCollection)
        {
            return serviceCollection
                .AddMeditatRLibrary(typeof(FooRequestHandler).Assembly);
        }
    }
}