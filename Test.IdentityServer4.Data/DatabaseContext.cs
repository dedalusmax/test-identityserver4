using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Test.IdentityServer4.Data.Entities;
using Test.IdentityServer4.Data.Interfaces;

namespace Test.IdentityServer4.Data
{
    public class DatabaseContext : IdentityDbContext, IReadOnlyDatabaseContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        public DbSet<Organisation> Organisations { get; set; }

        // See Dink's DatabaseContext.cs as a further reference
        public DbSet<TEntity> DataSet<TEntity>()
            where TEntity : class
        {
            return Set<TEntity>();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            modelBuilder.Entity<Organisation>()
                .Property(o => o.IsActive)
                .HasDefaultValue(true);

            modelBuilder.Entity<Organisation>()
                .Property(o => o.DeploymentStatus)
                .HasDefaultValue(AzureDeploymentStatus.NotStarted);
        }
    }
}
