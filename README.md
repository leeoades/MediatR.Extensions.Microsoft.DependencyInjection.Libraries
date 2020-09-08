MediatR Libraries
=================
We **love** Mediatr! 

However, when adding Mediatr to the container (i.e. *IServiceCollection*), a complete list of assemblies to scan must be provided. This might not be known, especially when consuming a library which is consuming other libraries.

The convention in **.net** is for a library to provide an extension method for setting up its container registrations. These abstract the library's dependencies, and it can, in turn, call the extension methods of its dependencies.

This package provides a mechanism for following this convention when using Mediatr.

Library Mediatr Registrations
-----------------------------
Within a library, MediatR registrations are added using *.AddMeditatRLibrary(...)*.

    // Add an assembly to be scanned by MediatR
    serviceCollection.AddMeditatRLibrary(typeof(MyRequestHandler).Assembly);

    // Add an assembly using a marker type
    serviceCollection.AddMeditatRLibrary(typeof(MyRequestHandler));

    // Add multiple assemlies
    serviceCollection.AddMeditatRLibrary(typeof(MyRequestHandler), typeof(AnotherRequestHandler));

Main Program Mediatr Registration
---------------------------------
Within the main program, replace the call to *.AddMediator()* with *.AddMediatRIncludingLibraries(...)*

    // Register MediatR
    serviceCollection.AddMediatRIncludingLibraries();

    // Assemblies and Marker Types can still be added at this point
    serviceCollection.AddMediatRIncludingLibraries(typeof(Startup));

