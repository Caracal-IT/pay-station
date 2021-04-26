using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Caracal.Security.Services;
using Caracal.Security.Simulator.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

using Caracal.EventBus;
using Caracal.PayStation.Application;
using Caracal.PayStation.Application.UseCases.Infrastructure.LoginUser;
using Caracal.PayStation.Application.UseCases.Withdrawals.ChangeStatus;
using Caracal.PayStation.Application.UseCases.Withdrawals.GetWithdrawals;
using Caracal.PayStation.Payments.Engines;
using Caracal.PayStation.Payments;
using Caracal.PayStation.Payments.Repositories;
using Caracal.PayStation.Storage.Postgres.DbContexts;
using Caracal.PayStation.Storage.Postgres.Services.Payments;
using Caracal.PayStation.Storage.Postgres.Services.Workflow;
using Caracal.PayStation.Workflow;
using Caracal.PayStation.Workflow.Repositories;
using Caracal.PayStation.WorkflowEngine;
using Microsoft.EntityFrameworkCore;
using PaymentsImpl = Caracal.PayStation.Payments.Services;
using WfImpl = Caracal.PayStation.Workflow.Services;

namespace Caracal.PayStation.Api {
    public class Startup {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.AddAutoMapper(typeof(Startup));
            services.AddControllers();
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo {
                    Title = "Caracal PayStation Api", 
                    Description = "Facilitate in the processing of payments.",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Ettiene Mare",
                        Email = string.Empty,
                        Url = new Uri("https://twitter.com/test"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "MIT",
                        Url = new Uri("https://example.com/license"),
                    },
                    Version = "v1"
                });
                
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
            
            services.AddDbContextPool<WithdrawalDbContext>(options => options.UseNpgsql(Configuration.GetConnectionString("PayStation"))); 
            
            services.AddTransient<LoginService, AuthService>();
           // services.AddSingleton<InfrastructureUseCaseBuilder>();
            //services.AddSingleton<WithdrawalsUseCaseBuilder>();
            
            //nameof(ChangeWithdrawalStatusUseCase) => new ChangeWithdrawalStatusUseCase(_eventBus) as TUseCase,
            //nameof(GetWithdrawalsUseCase) => new GetWithdrawalsUseCase(_eventBus) as TUseCase,
            
            //nameof(LoginUseCase) => new LoginUseCase(_loginService) as TUseCase,

            services.AddTransient<ChangeWithdrawalStatusUseCase>();
            services.AddTransient<GetWithdrawalsUseCase>();
            services.AddTransient<LoginUseCase>();

            services.AddTransient<Caracal.EventBus.EventBus, MemoryEventBus>();
            services.AddTransient<WithdrawalEngine, WithdrawalEngineWithEventBus>();
            services.AddTransient<ChangeStateEngine, ChangeStateEngineWithEventBus>();

            services.AddTransient<WithdrawalService, PaymentsImpl.WithdrawalService>();
            services.AddTransient<StateService, WfImpl.StateService>();

           // services.AddSingleton<WithdrawalsRepository, MemoryWithdrawalsRepository>();
           // services.AddSingleton<StateRepository, MemoryStateRepository>();
            
            services.AddTransient<WithdrawalsRepository, EFWithdrawalsRepository>();
            services.AddTransient<StateRepository, EFStateRepository>();
            
            services.AddTransient<Start>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, Start appStart) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
                app.UseStaticFiles();
                app.UseSwagger();
                app.UseSwaggerUI(c => {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Caracal.PayStation.Api v1");
                    c.InjectStylesheet("/swagger-ui/custom.css");
                    c.InjectJavascript("/swagger-ui/custom.js");
                    c.DocumentTitle = "Caracal Pay Station";
                });
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            var t = Task.Run(async() => await appStart.Initialize());
            Task.WhenAny(t, Task.Delay(10_000));
        }
    }
}

/*
 nameof(ChangeWithdrawalStatusUseCase) => new ChangeWithdrawalStatusUseCase(_eventBus) as TUseCase,
            nameof(GetWithdrawalsUseCase) => new GetWithdrawalsUseCase(_eventBus) as TUseCase,
            
            nameof(LoginUseCase) => new LoginUseCase(_loginService) as TUseCase,
 */