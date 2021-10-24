using System.Threading;
using EventFlow;
using EventFlow.AspNetCore.Extensions;
using EventFlow.DependencyInjection.Extensions;
using EventFlow.Queries;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Trsys.BackOffice;
using Trsys.BackOffice.Application;
using Trsys.BackOffice.Application.Read.Models;
using Trsys.BackOffice.Application.Write.Commands;
using Trsys.BackOffice.Domain;
using Trsys.CopyTrading;
using Trsys.CopyTrading.Application;
using Trsys.CopyTrading.Domain;
using Trsys.Ea;
using Trsys.Ea.Application;
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
                    config.LoginPath = "/login";
                    config.LogoutPath = "/logout";
                });

            services.AddEventFlow(ef =>
            {
                ef.UseCopyTradeApplication();
                ef.UseEaApplication();
                ef.AddAspNetCore();
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
                var resolver = scope.ServiceProvider.GetRequiredService<BackOfficeEventFlowRootResolver>();
                var queryProcessor = resolver.Resolve<IQueryProcessor>();
                var user = queryProcessor.ProcessAsync(new ReadModelByIdQuery<LoginReadModel>("ADMIN"), CancellationToken.None).Result;
                if (user == null)
                {
                    var commandBus = resolver.Resolve<ICommandBus>();
                    commandBus.PublishAsync(new UserCreateAdministratorCommand(UserId.New, new Username("admin"), new HashedPassword("P@ssw0rd"), new UserNickname("管理者"), new() { DistributionGroupId.New }), CancellationToken.None).Wait();
                }
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
