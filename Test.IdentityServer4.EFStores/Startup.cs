using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.ApplicationInsights.Extensibility;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using IdentityServer4;
using IdentityServer4.Quickstart.UI;
using Test.IdentityServer4.EFStores.Persistence;
using IdentityServer4.Stores;
using Test.IdentityServer4.EFStores.Data;

namespace Test.IdentityServer4.EFStores
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

            //const string connectionString = @"Data Source=(LocalDb)\MSSQLLocalDB;database=IdentityServer4.Quickstart.EntityFramework-2.0.0;trusted_connection=yes;";
            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            services.AddDbContext<ConfigurationStoreContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("AppDB"),
                d => d.MigrationsAssembly(migrationsAssembly)
                )
            );

            services.AddTransient<IClientStore, ClientStore>();
            services.AddTransient<IResourceStore, ResourceStore>();

            services.AddIdentityServer()
               .AddDeveloperSigningCredential()
               .AddTestUsers(TestUsers.Users)
               .AddResourceStore<ResourceStore>()
               .AddClientStore<ClientStore>();
                //.AddProfileService<ProfileService>();

            services.AddAuthentication()
                .AddGoogle("Google", options =>
                {
                    options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                    options.ClientId = "434483408261-55tc8n0cs4ff1fe21ea8df2o443v2iuc.apps.googleusercontent.com";
                    options.ClientSecret = "3gcoTrEDPPJ0ukn_aYYT6PWo";
                });

            services.AddAutoMapper(x => x.AddProfile(new MappingProfile()));
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
    }
}
