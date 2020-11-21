# ConsoleApplicationServiceLibrary

![Publish Packages](https://github.com/tommasodotNET/ConsoleApplicationServiceLibrary/workflows/Publish%20Packages/badge.svg)
[![package](https://img.shields.io/nuget/vpre/ServiceLibrary.ConsoleApp.svg?label=ServiceLibrary.ConsoleApp&style=flat-square)](https://www.nuget.org/packages/ServiceLibrary.ConsoleApp)

This class library is meant to ease configuration and registration of services, database contexts and option files in .NET 5.0 projects.

## Installation and configuration

Install package from nuget gallery using either dotnet CLI

```dotnet
dotnet add package ServiceLibrary.ConsoleApp
```

or the package manager

```
Install-Package ServiceLibrary.ConsoleApp
```

All the services will be registered the first time one service is invoked.
Since there's no native support for Dependency Injection in Console Application, I've provided the ```BaseController``` class which contains a ServiceProvider property. The ServiceProvider returns a singleton of a ServiceProvider configured by the [ServiceProviderConfigurator](./ConsoleApplicationServiceLibrary/Providers/ServiceProviderConfiguration.cs).

```csharp

public class SalesTaxesController : BaseController
{
    private readonly IService _service;

    public MyController(IService service = null)
    {
        _service = service ?? ServiceProvider.GetService<IService>();
    }
}
```

## Configuring services

Services registered with DI are usually interface's implementations. Starting froma a generic interface IService, the implementation would be

```csharp
public interface Service : IService
```

To have this automatic registered by this class library, we just add one attribute

```csharp
[Service(typeof(IService), ServiceLifetime.Scoped)]
public interface Service : IService
```

This is the equivalent of adding

```csharp
services.AddScoped<IService, Service>();
```

in the startup.cs.
By changing the values of the second parameter we can select the lifetime of the service (scoped, singleton, ...).
If the Service does not implement any interface, we can simply leave first attribute null.

## Configuring databases

Databases can be easily configured adding the attribute

```csharp
[Database("ConnectionString")]
public class MyDbContext : DbContext
```

This is the equivalent of adding

```csharp
services.AddDbContext<MyDbContext>(options => options.UseSqlite("Data Source=<connection_string>"));
```

in the startup.cs.

The string parameter is the name of the json attribute in the appsettings.json file which holds the value for the connection string.

## Configuring options

Options can be easily configured adding the attribute

```csharp
[Option("JsonSections")]
public class MySettings
```

This is the equivalent of adding

```csharp
// Add functionality to inject IOptions<T>
services.AddOptions();

// Add our Config object so it can be injected
services.Configure<MySettings>(Configuration.GetSection("JsonSections"));
```

in the startup.cs.

The string parameter is the name of the json section in the appsettings.json file which we want to map to our model. Every attribute in the json object needs to have a correspondent property in the model.