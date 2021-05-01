using Caracal.PayStation.Application.UseCases.Withdrawals.ChangeStatus;
using Caracal.PayStation.Application.UseCases.Withdrawals.GetWithdrawals;
using Caracal.PayStation.Application.UseCases.Withdrawals.ProcessWFClientAction;
using Caracal.PayStation.Application.UseCases.Withdrawals.RequestWithdrawal;
using Caracal.PayStation.Application.UseCases.Withdrawals.UpdateClientEvent;
using Caracal.PayStation.Payments;
using Caracal.PayStation.Payments.Repositories;
using Caracal.PayStation.Storage.Postgres.Services.Payments;
using Microsoft.Extensions.DependencyInjection;

namespace Caracal.PayStation.Api.DependencyInjection {
    public static class PaymentsStart {
        public static void AddPayments(this IServiceCollection services) {
            services.AddTransient<GetWithdrawalsUseCase>();
            services.AddTransient<ChangeWithdrawalStatusUseCase>();
            services.AddTransient<RequestWithdrawalUseCase>();
            services.AddTransient<UpdateClientEventUseCase>();
            services.AddTransient<ProcessWFClientActionUseCase>();
            
            services.AddTransient<WithdrawalService, Payments.Services.WithdrawalService>();
            services.AddTransient<WithdrawalsRepository, EFWithdrawalsRepository>();
        }
    }
}