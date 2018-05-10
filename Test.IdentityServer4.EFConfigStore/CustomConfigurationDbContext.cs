using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;

namespace Test.IdentityServer4.EFConfigStore
{
    public class CustomConfigurationDbContext : ConfigurationDbContext
    {
        public CustomConfigurationDbContext(DbContextOptions<ConfigurationDbContext> options, ConfigurationStoreOptions storeOptions) 
            : base(options, storeOptions)
        {

        }
    }
}
