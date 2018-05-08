using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Swagger;
using System.Collections.Generic;
using Test.Data;
using Test.Data.Entities;

namespace Test.WebAPI.Swagger.RealData
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
                .AddMvc()
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                });

            services.AddAuthorization();

            services.AddAuthentication(o =>
            {
                o.DefaultScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
                o.DefaultAuthenticateScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
            })
                .AddIdentityServerAuthentication(options =>
                {
                    var config = Configuration.GetSection("IdentityServer");
                    var identityUrl = config["Url"];
                    options.Authority = identityUrl;
                    options.ApiName = "api:system";
                    options.RequireHttpsMetadata = false;
                    options.SupportedTokens = SupportedTokens.Both;
                });

            ConfigureDataRepositories(services);
            ConfigureBusinessServices(services);

            // Register the Swagger generator, defining one or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                var config = Configuration.GetSection("IdentityServer");
                var identityUrl = config["Url"];

                //c.OperationFilter<FileUploadOperationFilter>();
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
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
                             { "api:system", "Admin API" }
                         }
                     });

                c.OperationFilter<AuthorizeCheckOperationFilter>();
                c.DescribeAllEnumsAsStrings();
            });

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, AppDbContext dbContext)
        {
            app.UseAuthentication();

            // automatic migration on the target database
            dbContext.Database.Migrate();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.DocExpansion(DocExpansion.None);
                c.OAuthClientId("adminswaggerui");
                c.OAuthAppName("Admin Swagger UI");
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API");
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

            app.UseMvc();
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

        private void ConfigureBusinessServices(IServiceCollection services)
        {
            //services.AddSingleton<CryptographyService>();

            //services.AddScoped<AuthenticationService>();
            //services.AddScoped<UserService>();
            //services.AddScoped<RoleService>();
            //services.AddScoped<AuditService>();
            //services.AddScoped<EmailService>();
            //services.AddScoped<SmsService>();
            //services.AddScoped<OrganisationService>();
        }
    }
}
