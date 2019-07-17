using Avalara.Skyscraper.Data;
using Avalara.Skyscraper.Services;
using Avalara.Skyscraper.Web.Common;
using log4net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Avalara.Skyscraper.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static IConfiguration Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(opts =>
            {
                var ai = new AvalaraIdentityHelper(Configuration);
                opts.Filters.Add(typeof(ApiKeyHandler));
                opts.Filters.Add(new AIAuthorizeFilter(ai));
            })
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            //services.UseApiKey();
            AddServicesInScope(services);

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
        }

        private static void AddServicesInScope(IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddScoped<IDbConnectionFactory>(e => new DbConnectionFactory(Configuration.GetSection("DBInfo")["ConnectionString"]));
            services.AddScoped<ISkyscraperDataHelper, SkyscraperDataHelper>();
            services.AddScoped<ISkyscraperService, SkyscraperService>();
            services.AddScoped<ILog>(e => new LoggingService().Initialize("SkyscrapeSvc"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
