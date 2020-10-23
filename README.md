# ConsoleApplicationServiceLibrary

![Publish Packages](https://github.com/tommasodotNET/ConsoleApplicationServiceLibrary/workflows/Publish%20Packages/badge.svg)
[![package](https://img.shields.io/nuget/vpre/ConsoleApplicationServiceLibrary.svg?label=WebAppServiceLibrary&style=flat-square)](https://www.nuget.org/packages/ConsoleApplicationServiceLibrary)

This class library is meant to ease configuration and registration of services, database contexts and option files in .NET Core 3.1 projects.

## Installation and configuration

Install package from nuget gallery using either dotnet CLI

```dotnet
dotnet add package ConsoleApplicationServiceLibrary
```

or the package manager

```
Install-Package ConsoleApplicationServiceLibrary
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
