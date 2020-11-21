using ServiceLibrary.Model.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ConsoleApplicationServiceLibrary.Providers
{
   public class ServiceProviderConfiguration
   {
      private static readonly Lazy<ServiceProviderConfiguration> _instance =
        new Lazy<ServiceProviderConfiguration>(() => new ServiceProviderConfiguration());

      public static ServiceProviderConfiguration Instance
      {
         get => _instance.Value;
      }

      public ServiceProviderConfiguration()
      {
         Console.WriteLine("Configuring services...");

         var builder = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile("appsettings.json");

         var configurationOptions = builder.Build();

         ServiceProvider = new ServiceCollection()
             .AddServicesWithReflection()
             .ConfigureOptions(configurationOptions)
             .AddDbContextWithReflection(configurationOptions)
             .BuildServiceProvider();

         Console.WriteLine("Done.");
      }

      public ServiceProvider ServiceProvider { get; }
   }
}
