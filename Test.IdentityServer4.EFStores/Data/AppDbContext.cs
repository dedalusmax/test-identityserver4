using Microsoft.EntityFrameworkCore;
using Test.IdentityServer4.EFStores.Data.Entities;

namespace Test.IdentityServer4.EFStores.Data
{
    public class AppDbContext: DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Claim> Claims { get; set; }
        public DbSet<Grant> Grants { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Role>()
                .Property(r => r._Permissions).HasColumnName("Permissions");

            modelBuilder.Entity<User>()
                .HasMany(c => c.Claims);

            #region Defaults

            modelBuilder.Entity<User>()
                .Property(u => u.IsActive)
                .HasDefaultValue(true);

            modelBuilder.Entity<User>()
                .Property(u => u.Language)
                .HasDefaultValue(Language.English);

            #endregion

            #region Alternate keys

            //modelBuilder.Entity<Role>()
            //    .HasAlternateKey(_ => _.Name);

            #endregion

            #region Indexes

            modelBuilder.Entity<User>()
                .HasIndex(_ => _.UserName)
                .IsUnique(true);

            modelBuilder.Entity<Role>()
                .HasIndex(_ => _.Name)
                .IsUnique(true);

            #endregion

            #region Composite Keys
            
            //modelBuilder.Entity<OrganisationMembership>()
            //    .HasOne<Organisation>(o => o.Organisation)
            //    .WithMany(u => u.OrganisationMembership)
            //    .HasForeignKey(o => o.OrganisationId);

            #endregion

            #region CascadeActions

            //modelBuilder.Entity<User>()
            //    .HasOne(_ => _.Role)
            //    .WithMany()
            //    .OnDelete(DeleteBehavior.SetNull);

            #endregion
        }
    }
}
