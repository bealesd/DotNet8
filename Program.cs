using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace DotNet8
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World from explicit DotNet8 namespace!");

            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddCommandLine(args);
            var config = configurationBuilder.Build();
            var environment = config["Environment"];
            Console.WriteLine($"Environment: '{environment}'");

            HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
            builder.Configuration.Sources.Clear();
            builder.Configuration
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true);

            var runOnPipeline = builder.Configuration.GetValue<bool>("settings:runOnPipeline");
            Console.WriteLine($"runOnPipeline: {runOnPipeline}");

            var whoami = builder.Configuration.GetValue<string>("settings:whoami");
            Console.WriteLine($"whoami: {whoami}");
        }
    }
}
