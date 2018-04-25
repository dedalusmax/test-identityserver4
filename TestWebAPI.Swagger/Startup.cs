using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using IdentityServer4.AccessTokenValidation;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Test.WebAPI.Swagger
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
            services.AddCors();

            services
                .AddMvcCore()
                .AddAuthorization()
                .AddJsonFormatters();

            services.AddAuthorization(options =>
            {
                DefineAuthorizationPolicies(options);
            });

            //services.AddAutoMapper();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // ASP identity thingy
            //services.AddIdentity<User, IdentityRole>()
            //    .AddEntityFrameworkStores<DatabaseContext>()
            //    .AddDefaultTokenProviders();

            //services.AddAuthentication("Bearer")
            //    .AddIdentityServerAuthentication(options =>
            //    {
            //        options.Authority = "http://localhost:5000";
            //        options.RequireHttpsMetadata = false;

            //        options.ApiName = "api1";
            //    });

            ConfigureAuthService(services);

            // Register the Swagger generator, defining one or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                //c.OperationFilter<FileUploadOperationFilter>();
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });

                var config = Configuration.GetSection("IdentityServer");
                var identityUrl = config["Url"];

                c.AddSecurityDefinition(
                    "oauth2",
                     new OAuth2Scheme
                     {
                         Type = "oauth2",
                         Flow = "implicit",
                         AuthorizationUrl = identityUrl + "/connect/authorize",
                         TokenUrl = identityUrl + "/connect/token",
                         Scopes = new Dictionary<string, string>()
                         {
                             { "api1", "My API" }
                         }
                     });

                c.OperationFilter<AuthorizeCheckOperationFilter>();
                c.DescribeAllEnumsAsStrings();
            });

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseAuthentication();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(options =>
            {
                options.AllowAnyOrigin();
                options.AllowAnyMethod();
                options.AllowAnyHeader();
            });

            //app.UseIdentityServerAuthentication(new IdentityServerAuthenticationOptions
            //{
            //    Authority = "http://localhost:5000",
            //    RequireHttpsMetadata = false,

            //    ApiName = "api1",
            //});

            //app.UseAuthentication();

            app.UseMvc();
        }

        private static void DefineAuthorizationPolicies(AuthorizationOptions options)
        {
            //options.AddPolicy(SecurityConsts.POLICY_REQUIRE_SUPER_ADMIN_ROLE, policy => policy.RequireRole(SecurityConsts.ROLE_SUPER_ADMIN));
            //options.AddPolicy(SecurityConsts.POLICY_REQUIRE_ENTERPRISE_ADMIN_ROLE, policy => policy.RequireRole(SecurityConsts.ROLE_ENTERPRISE_ADMIN));
            //options.AddPolicy(SecurityConsts.POLICY_REQUIRE_USER_ROLE, policy => policy.RequireRole(SecurityConsts.ROLE_USER));
        }

        private void ConfigureAuthService(IServiceCollection services)
        {
            services.AddAuthentication(o =>
            {
                o.DefaultScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
                o.DefaultAuthenticateScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
            })
                .AddIdentityServerAuthentication(o =>
                {
                    var config = Configuration.GetSection("IdentityServer");
                    var identityUrl = config["Url"];
                    o.Authority = identityUrl;
                    o.ApiName = "api1";
                    o.RequireHttpsMetadata = false;
                    o.SupportedTokens = SupportedTokens.Both;

                    // o.EnableCaching = true;
                });
        }
    }
}
