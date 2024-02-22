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
            //dotnet run --project DotNet8.csproj --Environment=AT
            Console.WriteLine($"Environment: '{config["Environment"]}'");
            var environment = config["Environment"];

            //var environment = "";
            //var environmentUnparsed = args.FirstOrDefault(arg => arg.Split(':')[0] == "env") ?? "";
            //var environmentParsed = environmentUnparsed.Split(':');
            //if (environmentParsed.Length == 2)
            //    environment = environmentParsed[1];
            ////dotnet run --property:Configuration=AT
            //Console.WriteLine(environment);

            HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

            builder.Configuration.Sources.Clear();

            //IHostEnvironment env = builder.Environment;
            //var dotnetEnv = env.EnvironmentName;

            //        builder.Configuration
            //.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)

            builder.Configuration
                //.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
                //.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                ;

           var test = builder.Configuration.GetSection("settings").GetSection("whoami");

            var runOnPipeline = builder.Configuration.GetValue<string>("settings:bool");
            Console.WriteLine($"runOnPipeline: {runOnPipeline}");
            var whoami = builder.Configuration.GetValue<string>("settings:whoami");
            Console.WriteLine($"whoami: {whoami}");

        }
    }
}
