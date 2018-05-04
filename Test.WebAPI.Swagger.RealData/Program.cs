using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System;

namespace Test.WebAPI.Swagger.RealData
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = "API";
            BuildWebHost(args)
                .SeedDatabase()
                .Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}