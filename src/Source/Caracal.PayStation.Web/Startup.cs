using System;
using Caracal.PayStation.Web.Gateways.Core.Withdrawals;
using Caracal.PayStation.Web.Gateways.Security;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Caracal.PayStation.Web {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.AddAutoMapper(typeof(Startup));
            services.AddControllersWithViews();

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration => { configuration.RootPath = "ClientApp/www"; });

            var payStationApi = new Uri(Configuration["Services:PayStationApi"]);
            services.AddHttpClient<LoginGateway, ApiLoginGateway>(client => client.BaseAddress = payStationApi);
            services.AddHttpClient<WithdrawalGateway, ApiWithdrawalGateway>(client => client.BaseAddress = payStationApi);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) 
                app.UseDeveloperExceptionPage();
            else {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });
            
            app.Use((context, next) =>
            {
                context.Response.Headers["X-Version"] = Configuration["Application:Version"];
                return next.Invoke();
            });

            app.UseSpa(spa => {
                spa.Options.SourcePath = "ClientApp";
                spa.Options.StartupTimeout = TimeSpan.FromSeconds(60);
                
                if (env.IsDevelopment()) 
                    spa.UseProxyToSpaDevelopmentServer(Configuration["Services:ClientApp"]);
            });
        }
    }
}