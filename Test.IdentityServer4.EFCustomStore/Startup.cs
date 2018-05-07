using IdentityServer4;
using IdentityServer4.Stores;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Test.Data;
using Test.Data.Entities;
using Test.IdentityServer4.EFCustomStore.Persistence;

namespace Test.IdentityServer4.EFCustomStore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            ConfigureDataRepositories(services);

            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddInMemoryIdentityResources(Config.GetIdentityResources())
                .AddInMemoryApiResources(Config.GetApiResources())
                .AddInMemoryClients(Config.GetClients())
                .AddProfileService<ProfileService>();

            services.AddSingleton<IUserStore, UserStore>();
            services.AddTransient<IPersistedGrantStore, PersistedGrantStore>();

            services.AddAuthentication()
                .AddGoogle("Google", options =>
                {
                    options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                    options.ClientId = "434483408261-55tc8n0cs4ff1fe21ea8df2o443v2iuc.apps.googleusercontent.com";
                    options.ClientSecret = "3gcoTrEDPPJ0ukn_aYYT6PWo";
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                try
                {
                    var configuration = app.ApplicationServices.GetService<TelemetryConfiguration>();
                    configuration.DisableTelemetry = true;
                }
                catch { }
            }

            app.UseIdentityServer(); // includes a call to UseAuthentication

            // That’s it - if you run the server and navigate the browser to:
            // http://localhost:5000/.well-known/openid-configuration
            // you should see the so-called discovery document. This will be used by your clients and APIs to download the necessary configuration data.

            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
        }

        private void ConfigureDataRepositories(IServiceCollection services)
        {
            // Register DB contexts
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("AppDB")));

            // Register database scope
            services.AddScoped<IDatabaseScope, AppDatabaseScope>();

            // Register repositories
            services.AddScoped<IGenericRepository<User>, GenericRepository<User>>();
            services.AddScoped<IGenericRepository<Role>, GenericRepository<Role>>();
        }
    }
}
