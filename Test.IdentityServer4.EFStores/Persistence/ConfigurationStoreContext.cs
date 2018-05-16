using Microsoft.EntityFrameworkCore;
using Test.IdentityServer4.EFStores.Data.Entities;

namespace Test.IdentityServer4.EFStores.Persistence
{
    public class ConfigurationStoreContext : DbContext
    {
        public ConfigurationStoreContext(DbContextOptions<ConfigurationStoreContext> options) : base(options)
        { }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Claim> Claims { get; set; }
        public DbSet<Grant> Grants { get; set; }


        public DbSet<ClientEntity> Clients { get; set; }
        public DbSet<ApiResourceEntity> ApiResources { get; set; }
        public DbSet<IdentityResourceEntity> IdentityResources { get; set; }
        public DbSet<Secret> Secrets { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ClientEntity>()
                .HasKey(m => m.ClientId);

            builder.Entity<ApiResourceEntity>()
                .HasKey(m => m.Name);

            builder.Entity<IdentityResourceEntity>()
                .HasKey(i => i.Name);

            builder.Entity<ApiResourceEntity>()
                .Ignore(a => a.UserClaims)
                .Ignore(a => a.Scopes)
                .Ignore(a => a.ApiSecrets);

            builder.Entity<ClientEntity>()
                .HasMany(t => t.ClientSecrets);

            builder.Entity<ClientEntity>()
                .HasMany(t => t.AllowedGrantTypes);

            builder.Entity<ClientEntity>()
                .Ignore(c => c.PostLogoutRedirectUris)
                .Ignore(c => c.RedirectUris)
                .Ignore(c => c.AllowedCorsOrigins)
                .Ignore(c => c.AllowedScopes);

            builder.Entity<IdentityResourceEntity>()
                .Ignore(i => i.UserClaims);

            #region Defaults

            builder.Entity<IdentityResourceEntity>()
                .Property(u => u.Required)
                .HasDefaultValue(false);

            builder.Entity<IdentityResourceEntity>()
                .Property(u => u.Emphasize)
                .HasDefaultValue(false);

            builder.Entity<IdentityResourceEntity>()
                .Property(u => u.ShowInDiscoveryDocument)
                .HasDefaultValue(true);

            builder.Entity<IdentityResourceEntity>()
                .Property(u => u.Enabled)
                .HasDefaultValue(true);

            builder.Entity<ApiResourceEntity>()
                .Property(u => u.Enabled)
                .HasDefaultValue(true);

            builder.Entity<ClientEntity>()
                .Property(u => u.Enabled)
                .HasDefaultValue(true);

            builder.Entity<ClientEntity>()
                .Property(u => u.RequireClientSecret)
                .HasDefaultValue(true);

            builder.Entity<ClientEntity>()
                .Property(u => u.RequireConsent)
                .HasDefaultValue(true);

            builder.Entity<ClientEntity>()
                .Property(u => u.AllowRememberConsent)
                .HasDefaultValue(true);

            builder.Entity<ClientEntity>()
                .Property(u => u.RequirePkce)
                .HasDefaultValue(false);

            builder.Entity<ClientEntity>()
                .Property(u => u.AllowPlainTextPkce)
                .HasDefaultValue(false);

            builder.Entity<ClientEntity>()
                .Property(u => u.AllowAccessTokensViaBrowser)
                .HasDefaultValue(false);

            builder.Entity<ClientEntity>()
                .Property(u => u.AllowOfflineAccess)
                .HasDefaultValue(false);

            builder.Entity<ClientEntity>()
                .Property(u => u.AlwaysIncludeUserClaimsInIdToken)
                .HasDefaultValue(false);

            builder.Entity<ClientEntity>()
                .Property(u => u.AlwaysSendClientClaims)
                .HasDefaultValue(false);

            #endregion

            builder.Entity<Role>()
                .Property(r => r._Permissions).HasColumnName("Permissions");

            builder.Entity<User>()
                .HasMany(c => c.Claims);

            #region Defaults

            builder.Entity<User>()
                .Property(u => u.IsActive)
                .HasDefaultValue(true);

            builder.Entity<User>()
                .Property(u => u.Language)
                .HasDefaultValue(Language.English);

            #endregion

            #region Alternate keys

            //builder.Entity<Role>()
            //    .HasAlternateKey(_ => _.Name);

            #endregion

            #region Indexes

            builder.Entity<User>()
                .HasIndex(_ => _.UserName)
                .IsUnique(true);

            builder.Entity<Role>()
                .HasIndex(_ => _.Name)
                .IsUnique(true);

            #endregion

            #region Composite Keys

            //builder.Entity<OrganisationMembership>()
            //    .HasOne<Organisation>(o => o.Organisation)
            //    .WithMany(u => u.OrganisationMembership)
            //    .HasForeignKey(o => o.OrganisationId);

            #endregion

            #region CascadeActions

            //builder.Entity<User>()
            //    .HasOne(_ => _.Role)
            //    .WithMany()
            //    .OnDelete(DeleteBehavior.SetNull);

            #endregion

        }
    }
}
