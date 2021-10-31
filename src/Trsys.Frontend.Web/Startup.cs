using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading;
using Trsys.BackOffice;
using Trsys.BackOffice.Abstractions;
using Trsys.CopyTrading;
using Trsys.Ea;
using Trsys.Frontend.Web.Formatters;

namespace Trsys.Frontend.Web
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
            services.AddControllersWithViews(config =>
            {
                config.InputFormatters.Add(new TextPlainInputFormatter());
            });
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(config =>
                {
                    config.ReturnUrlParameter = "returnUrl";
                    config.LoginPath = "/login";
                    config.LogoutPath = "/logout";
                });

            services.AddCopyTrading();
            services.AddEa();
            services.AddBackOffice();
            //services.AddOpenTelemetryTracing(builder =>
            //{
            //    builder
            //        .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(Environment.GetEnvironmentVariable("WEBSITE_SITE_NAME") ?? "trsys-server"))
            //        .AddAspNetCoreInstrumentation()
            //        .AddSource("Trsys.CopyTrading")
            //        .AddZipkinExporter(options =>
            //        {
            //            var endpoint = Environment.GetEnvironmentVariable("OTEL_EXPORTER_ZIPKIN_ENDPOINT");
            //            if (!string.IsNullOrEmpty(endpoint))
            //            {
            //                options.Endpoint = new Uri(endpoint);
            //            }
            //        });
            //});
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
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<IUserService>();
                service.CreateAdministratorIfNotExistsAsync("admin", "P@ssw0rd", "管理者", CancellationToken.None).Wait();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
