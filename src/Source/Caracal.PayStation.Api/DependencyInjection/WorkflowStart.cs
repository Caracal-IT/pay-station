using Caracal.PayStation.Application.UseCases.Withdrawals.GetWithdrawals;
using Caracal.PayStation.Storage.Postgres.Services.Workflow;
using Caracal.PayStation.Workflow;
using Caracal.PayStation.Workflow.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Caracal.PayStation.Api.DependencyInjection {
    public static class WorkflowStart {
        public static void AddWorkflow(this IServiceCollection services) {
            services.AddTransient<GetWithdrawalsUseCase>();
            services.AddTransient<StateService, Workflow.Services.StateService>();
            services.AddTransient<StateRepository, EFStateRepository>();
            // services.AddSingleton<StateRepository, MemoryStateRepository>();
        } 
    }
}