using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Linq;

namespace Test.IdentityServer4.EFConfigStore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = "IdentityServer4.EntityFramework";

            var host = BuildWebHost(args);

            SeedData.EnsureSeedData(host.Services);

            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
