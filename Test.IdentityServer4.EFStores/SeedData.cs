using AutoMapper;
using IdentityServer4.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using Test.IdentityServer4.EFStores.Persistence;

namespace Test.IdentityServer4.EFStores
{
    public class SeedData
    {
        public static void EnsureSeedData(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = scope.ServiceProvider.GetService<ConfigurationStoreContext>();

                context.Database.Migrate();
                EnsureSeedData(context);
            }
        }

        private static void EnsureSeedData(ConfigurationStoreContext context)
        {
            if (!context.Clients.Any())
            {
                foreach (var client in Config.GetClients().ToList())
                {
                    var customClient = Mapper.Map<Client, Data.Entities.ClientEntity>(client);

                    context.Clients.Add(customClient);
                }
                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("Clients already populated");
            }

            if (!context.IdentityResources.Any())
            {
                foreach (var resource in Config.GetIdentityResources().ToList())
                {
                    var identityResource = Mapper.Map<IdentityResource, Data.Entities.IdentityResourceEntity>(resource);

                    context.IdentityResources.Add(identityResource);
                }
                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("IdentityResources already populated");
            }

            if (!context.ApiResources.Any())
            {
                foreach (var resource in Config.GetApiResources().ToList())
                {
                    var apiResource = Mapper.Map<ApiResource, Data.Entities.ApiResourceEntity>(resource);

                    context.ApiResources.Add(apiResource);
                }
                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("ApiResources already populated");
            }

            Console.WriteLine("Done seeding database.");
            Console.WriteLine();
        }
    }
}
