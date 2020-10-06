using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Zetes.WebGui.Consumers;

namespace Zetes.WebGui
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(config =>
            {
                config.DefaultScheme = "ZetesWeb";
                config.DefaultAuthenticateScheme = "oidc";
            }).AddCookie("ZetesWeb")
            .AddOpenIdConnect("oidc", config =>
            {
                config.Authority = "https://localhost:44319/";
                config.ClientId = "zetes_web_gui";
                config.ClientSecret = "fhfhdifdieyf9fadpfaosdf89d9sfd";
                config.SaveTokens = true;
                config.ResponseType = "code";
                config.SignedOutCallbackPath = "/Home/Index";
                
                config.ClaimActions.DeleteClaim("amr");
                config.ClaimActions.DeleteClaim("s_hash");
                config.ClaimActions.DeleteClaim("amr");
                
                config.GetClaimsFromUserInfoEndpoint = true;
                
                config.Scope.Clear();
                config.Scope.Add("openId");
                config.Scope.Add("ApiRestService");
            });
            services.AddHttpClient();

            services.AddScoped<IRemoteConsumer, RemoteConsumer>();

            services.AddControllersWithViews();

            services.AddKendo();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler();
            }

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseStatusCodePages(async context =>
            {
                context.HttpContext.Response.ContentType = "text/plain";

                await context.HttpContext.Response.WriteAsync(
                    "Status code page, status code: " +
                    context.HttpContext.Response.StatusCode);
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
