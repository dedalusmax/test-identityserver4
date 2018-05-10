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

            var seed = args.Contains("/seed");
            if (seed)
            {
                args = args.Except(new[] { "/seed" }).ToArray();
            }

            var host = BuildWebHost(args);

            if (seed)
            {
                SeedData.EnsureSeedData(host.Services);
                return;
            }

            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
