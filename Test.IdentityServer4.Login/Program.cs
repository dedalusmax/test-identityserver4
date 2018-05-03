using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System;

namespace Test.IdentityServer4.Login
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = "IdentityServerWithAspNetIdentity";

            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
