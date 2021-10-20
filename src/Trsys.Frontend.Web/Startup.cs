using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
            // services.AddCopyTrading();
            services.AddEa();
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

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
