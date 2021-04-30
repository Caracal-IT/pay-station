using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Caracal.PayStation.Api.DependencyInjection;
using Caracal.PayStation.Api.Workflow.Activities;
using Caracal.PayStation.Api.Workflow.Activities.Withdrawals;
using Elsa.Activities.Email.Extensions;
using Elsa.Activities.Http.Extensions;
using Elsa.Activities.Timers.Extensions;
using Elsa.Dashboard.Extensions;
using Elsa.Persistence.EntityFrameworkCore.DbContexts;
using Elsa.Persistence.EntityFrameworkCore.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Caracal.PayStation.Api {
    public class Startup {
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.AddSingleton(System.Console.Out);
            services.AddActivity<ChangeStatus>();
            services.AddActivity<RequestWithdrawal>();
            services.AddActivity<ClientEvent>();
            
            services // Required services for Elsa to work. Registers things like `IWorkflowInvoker`.
                .AddElsa( elsa => elsa
                    .AddEntityFrameworkStores<PostgreSqlContext>(options => options
                        .UseNpgsql(Configuration.GetConnectionString("Workflow"))
                    ));
            
            //services.AddTransient<IStartupFilter, MigrationStartupFilter<PostgreSqlContext>>();

            services.AddHttpActivities(options => options.Bind(Configuration.GetSection("Elsa:Http")))
                .AddEmailActivities(options => options.Bind(Configuration.GetSection("Elsa:Smtp")))
                .AddTimerActivities(options => options.Bind(Configuration.GetSection("Elsa:Timers")));
            
            services.AddAutoMapper(typeof(Startup));
            services.AddControllers();
            services.AddSwagger();

            services.AddDistributedMemoryCache();
            
            services.AddLogin();
            
            services.AddPayStation(Configuration);
            services.AddPayments();

            services.AddElsaDashboard();
//https://localhost:5001/withdrawal/request
            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                    builder =>
                    {
                        builder.WithOrigins("https://localhost:7001",
                            "http://localhost:7000")
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
                app.UseStaticFiles();
                app.UseSwaggerDocs();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            app.UseCors(MyAllowSpecificOrigins);
            
            app.UseHttpActivities();
        }
    }
}