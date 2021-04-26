using Caracal.PayStation.Application.UseCases.Infrastructure.LoginUser;
using Caracal.Security.Services;
using Caracal.Security.Simulator.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Caracal.PayStation.Api.DependencyInjection {
    public static class InfrastructureStart {
        public static void AddLogin(this IServiceCollection services) {
            services.AddTransient<LoginService, AuthService>();
            services.AddTransient<LoginUseCase>();
        }
    }
}