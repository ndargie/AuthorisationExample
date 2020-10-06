using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;
using TestFrontEnd.Consumer;

namespace TestFrontEnd
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(config =>
            {
                config.DefaultScheme = "Cookie";
                config.DefaultChallengeScheme = "oidc";                
            }).AddCookie("Cookie")
            .AddOpenIdConnect("oidc", config =>
            {
                config.Authority = "https://localhost:44349/";
                config.ClientId = "client_id_mvc";
                config.ClientSecret = "client_secret_mvc";
                config.SaveTokens = true;
                config.ResponseType = "code";
                config.NonceCookie.SameSite = SameSiteMode.Lax;
                config.CorrelationCookie.SameSite = SameSiteMode.Lax;
                config.SignedOutCallbackPath = "/Home/Index";

                //configure cookies claim mapping
                config.ClaimActions.DeleteClaim("amr");
                config.ClaimActions.DeleteClaim("s_hash");
                config.ClaimActions.DeleteClaim("amr");

                //two trips to load claim into cookie but id token is smaller
                config.GetClaimsFromUserInfoEndpoint = true;
                //configure scope
                config.Scope.Clear();
                config.Scope.Add("openid");
                config.Scope.Add("profile");
                config.Scope.Add("ApiOne");
                config.Scope.Add("offline_access");
                config.TokenValidationParameters.NameClaimType = "name";                
            });

            services.AddHttpClient();
            services.AddControllersWithViews().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();               
            });

            services.AddScoped<IRemoteConsumer, RemoteConsumer>();
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddKendo();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
