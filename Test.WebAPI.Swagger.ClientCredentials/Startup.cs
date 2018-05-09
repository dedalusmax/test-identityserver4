using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Swagger;
using System.Collections.Generic;

namespace Test.WebAPI.Swagger.ClientCredentials
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllHeaders",
                      builder =>
                      {
                          builder.AllowAnyOrigin()
                                 .AllowAnyHeader()
                                 .AllowAnyMethod();
                      });
            });

            services.AddMvc()
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
                         Flow = "application",
                         AuthorizationUrl = identityUrl + "/connect/authorize",
                         TokenUrl = identityUrl + "/connect/token",
                         Scopes = new Dictionary<string, string>()
                         {
                             { "api:system", "Admin API" }
                         }
                     });

                c.OperationFilter<AuthorizeCheckOperationFilter>();
                c.DescribeAllEnumsAsStrings();

                c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
                {
                    { "oauth2", new[] { "api:system" } }
                });
            });

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseAuthentication();

            app.UseSwagger();


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

            //app.UseCors("AllowAllHeaders");

            app.UseCors(options =>
            {
                options.AllowAnyOrigin();
                options.AllowAnyMethod();
                options.AllowAnyHeader();
            });


            app.UseMvc();
        }
    }
}
