using Caracal.PayStation.Application.UseCases.Withdrawals.ChangeStatus;
using Caracal.PayStation.Payments;
using Caracal.PayStation.Payments.Repositories;
using Caracal.PayStation.Storage.Postgres.Services.Payments;
using Microsoft.Extensions.DependencyInjection;

namespace Caracal.PayStation.Api.DependencyInjection {
    public static class PaymentsStart {
        public static void AddPayments(this IServiceCollection services) {
            services.AddTransient<ChangeWithdrawalStatusUseCase>();
            services.AddTransient<WithdrawalService, Payments.Services.WithdrawalService>();
            services.AddTransient<WithdrawalsRepository, EFWithdrawalsRepository>();
            // services.AddSingleton<WithdrawalsRepository, MemoryWithdrawalsRepository>(); 
        }
    }
}