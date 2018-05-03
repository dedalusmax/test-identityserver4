using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

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
                .UseKestrel(options =>
                {
                    options.Listen(System.Net.IPAddress.Loopback, 5000);  // http:localhost:5000
                    options.Listen(System.Net.IPAddress.Loopback, 5001, listenOptions =>
                    {
                        try
                        {
                            listenOptions.UseHttps(Certificates.Certificate.Get());
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    });
                })
                .Build();
    }
}
