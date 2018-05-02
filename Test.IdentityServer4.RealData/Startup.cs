using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Test.IdentityServer4.Data;
using Test.IdentityServer4.Data.Entities;
using Test.IdentityServer4.RealData.Services;

namespace Test.IdentityServer4.RealData
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }

        public IHostingEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DatabaseContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // work on User
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<DatabaseContext>()
                .AddDefaultTokenProviders();

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddMvc();

            // don't know if it's needed
            //services.Configure<IISOptions>(iis =>
            //{
            //    iis.AuthenticationDisplayName = "Windows";
            //    iis.AutomaticAuthentication = false;
            //});

            // what's that?
            //services.AddTransient<ILoginService<User>, EFLoginService>();

            var builder = services.AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;
            })
                .AddDeveloperSigningCredential()
                .AddInMemoryPersistedGrants()
                .AddInMemoryIdentityResources(Config.GetIdentityResources())
                .AddInMemoryApiResources(Config.GetApiResources())
                .AddInMemoryClients(Config.GetClients())
                .AddAspNetIdentity<ApplicationUser>();

            // adds "serious" certificate. See Dink's Certificate.cs
            //builder.AddSigningCredential(Certificate.Get());

            //builder.Services.AddTransient<IProfileService, ProfileService>();

            //if (this.Environment.IsDevelopment())
            //{
            //    builder.AddDeveloperSigningCredential();
            //}
            //else
            //{
            //    throw new Exception("need to configure key material");
            //}

            // add third-party providers
            //services.AddAuthentication()
            //  .AddGoogle(options =>
            //  {
            //      var config = Configuration.GetSection("GoogleAuth");
            //      options.ClientId = config["GoogleClientId"];
            //      options.ClientSecret = config["GoogleClientSecret"];
            //      options.ClaimActions.MapJsonSubKey(IdentityModel.JwtClaimTypes.GivenName, "name", "givenName");
            //      options.ClaimActions.MapJsonSubKey(IdentityModel.JwtClaimTypes.FamilyName, "name", "familyName");
            //  })
            //  .AddSaml2("dink-okta", "Okta", options =>
            //  {
            //      // options.SignInScheme = IdentityServer4.IdentityServerConstants.ExternalCookieAuthenticationScheme;
            //      // Okta is the default SAML2 provider for now, so no need to set the ModulePath. That default path is /Saml2
            //      options.SPOptions.EntityId = new Saml2NameIdentifier("http://localhost:5000/Saml2");
            //      var idp = GetOktaIdentityProvider(options.SPOptions, string.Empty, string.Empty);
            //      options.IdentityProviders.Add(idp);
            //  });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                //app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseIdentityServer();

            app.UseMvcWithDefaultRoute();
            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute(
            //        name: "default",
            //        template: "{controller=Home}/{action=Index}/{id?}");
            //});
        }
    }
}
