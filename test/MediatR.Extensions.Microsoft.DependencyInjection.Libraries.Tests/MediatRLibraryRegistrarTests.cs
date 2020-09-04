using System.Collections.Generic;
using MediatR.Extensions.Microsoft.DependencyInjection.Libraries.Ext;
using MediatR.TestLibrary;
using MediatR.TestLibrary.Ext;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace MediatR.Extensions.Microsoft.DependencyInjection.Libraries.Tests
{
    public class MediatRLibraryRegistrarTests
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
                .AddTestLibraryByMarkerType()
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
        public void Supplied_config_action_should_be_executed_on_registration()
        {
            // ARRANGE
            var configCalls = new List<MediatRServiceConfiguration>();
            var serviceCollection = new ServiceCollection()
                .AddTestLibraryByAssemblyWithConfig(config =>
                {
                    configCalls.Add(config);
                });

            // ACT
            serviceCollection.AddMediatRIncludingLibraries();

            // ASSERT
            var actual = Assert.Single(configCalls);
            Assert.NotNull(actual);
        }
        
        [Fact]
        public void Supplied_config_action_should_be_executed_on_registration_of_assembly()
        {
            // ARRANGE
            var configCalls = new List<MediatRServiceConfiguration>();
            var serviceCollection = new ServiceCollection()
                .AddMeditatRLibrary(config =>
                {
                    configCalls.Add(config);
                }, typeof(MediatRLibraryRegistrarTests).Assembly);

            // ACT
            serviceCollection.AddMediatRIncludingLibraries();

            // ASSERT
            var actual = Assert.Single(configCalls);
            Assert.NotNull(actual);
        }
        
        [Fact]
        public void All_supplied_config_actions_should_be_executed_on_registration_of_assemblies()
        {
            // ARRANGE
            var configCalls = new List<MediatRServiceConfiguration>();
            var serviceCollection = new ServiceCollection()
                .AddMeditatRLibrary(config =>
                {
                    configCalls.Add(config);
                }, typeof(MediatRLibraryRegistrarTests))
                .AddTestLibraryByAssemblyWithConfig(config =>
                {
                    configCalls.Add(config);
                });

            // ACT
            serviceCollection.AddMediatRIncludingLibraries();

            // ASSERT
            Assert.Equal(2, configCalls.Count);
            Assert.NotNull(configCalls[0]);
            Assert.Equal(configCalls[0], configCalls[1]);
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