using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace RestApiCRUDDemo
{
    public class Program
    {

        //A host is an object that encapsulates an app's resources, such as:
        //Dependency injection(DI)
        //Logging
        //Configuration
        //IHostedService implementations
        public static void Main(string[] args)
        {
            //Creates a .NET Generic Host
            CreateHostBuilder(args).Build().Run();

            //The host is typically configured, built, and run by code in the Program class. The Main method:
            //Calls a CreateHostBuilder method to create and configure a builder object.
            //Calls Build and Run methods on the builder object.
        }

        /*
         * The CreateDefaultBuilder and ConfigureWebHostDefaults methods configure a host with a set of default options, such as:
         *  Use Kestrel as the web server and enable IIS integration.
         *  Load configuration from appsettings.json, appsettings.{Environment Name}.json, environment variables, command line arguments, and other configuration sources.
         *  Send logging output to the console and debug providers.
         */

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>()
                              .UseUrls("https://myapi:54691");
                });
    }
}
