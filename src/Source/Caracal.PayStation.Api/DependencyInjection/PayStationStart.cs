using Caracal.EventBus;
using Caracal.PayStation.Api.Filters;
using Caracal.PayStation.Storage.Postgres.DbContexts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Caracal.PayStation.Api.DependencyInjection {
    public static class PayStationStart {
        public static void AddPayStation(this IServiceCollection services, IConfiguration configuration) {
            services.AddDbContextPool<WithdrawalDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("PayStation"))); 
            
            services.AddTransient<IStartupFilter, MigrationStartupFilter<WithdrawalDbContext>>();
            services.AddTransient<Caracal.EventBus.EventBus, MemoryEventBus>();
        }
    }
}