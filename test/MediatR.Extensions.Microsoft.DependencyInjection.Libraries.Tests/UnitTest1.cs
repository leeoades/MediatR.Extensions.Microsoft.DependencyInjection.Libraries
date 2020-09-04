using MediatR.Extensions.Microsoft.DependencyInjection.Registrar.Ext;
using MediatR.TestLibrary;
using MediatR.TestLibrary.Ext;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace MediatR.Extensions.Microsoft.DependencyInjection.Libraries.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Registering_FooRequestHandler_by_assembly_the_normal_way_should_resolve()
        {
            // ARRANGE
            var serviceProvider = new ServiceCollection()
                .AddMediatR(typeof(FooRequestHandler).Assembly)
                .BuildServiceProvider();
            
            // ACT
            var actual = serviceProvider.GetService<IRequestHandler<FooRequest, FooResponse>>();
            
            // ASSERT
            Assert.NotNull(actual);
            Assert.IsType<FooRequestHandler>(actual);
        }
        
        [Fact]
        public void Registering_FooRequestHandler_by_marker_type_the_normal_way_should_resolve()
        {
            // ARRANGE
            var serviceProvider = new ServiceCollection()
                .AddMediatR(typeof(FooRequestHandler))
                .BuildServiceProvider();

            // ACT
            var actual = serviceProvider.GetService<IRequestHandler<FooRequest, FooResponse>>();
            
            // ASSERT
            Assert.NotNull(actual);
            Assert.IsType<FooRequestHandler>(actual);
        }
        
        [Fact]
        public void Library_registering_itself_by_marker_type_should_resolve()
        {
            // ARRANGE
            var serviceProvider = new ServiceCollection()
                .AddTestLibraryByType()
                .AddMediatRIncludingLibraries()
                .BuildServiceProvider();
            
            // ACT
            var actual = serviceProvider.GetService<IRequestHandler<FooRequest, FooResponse>>();
            
            // ASSERT
            Assert.NotNull(actual);
            Assert.IsType<FooRequestHandler>(actual);
        }
        
        [Fact]
        public void Library_registering_itself_by_assembly_should_resolve()
        {
            // ARRANGE
            var serviceProvider = new ServiceCollection()
                .AddTestLibraryByAssembly()
                .AddMediatRIncludingLibraries()
                .BuildServiceProvider();
            
            // ACT
            var actual = serviceProvider.GetService<IRequestHandler<FooRequest, FooResponse>>();
            
            // ASSERT
            Assert.NotNull(actual);
            Assert.IsType<FooRequestHandler>(actual);
        }
        
        [Fact]
        public void AddMediatRIncludingLibraries_when_Mediatr_already_registered_should_throw()
        {
            // ARRANGE
            var serviceCollection = new ServiceCollection()
                .AddMediatR(typeof(FooRequestHandler));

            // ACT
            var exception = Record.Exception(() => serviceCollection.AddMediatRIncludingLibraries());

            // ASSERT
            Assert.NotNull(exception);
            Assert.Contains("already registered", exception.Message);
        }
        
        
    }
}