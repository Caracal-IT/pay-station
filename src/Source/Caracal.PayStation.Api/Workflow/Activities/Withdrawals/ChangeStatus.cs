using System;
using System.Threading;
using System.Threading.Tasks;
using Caracal.PayStation.Application.UseCases.Withdrawals.ChangeStatus;
using Elsa.Attributes;
using Elsa.Results;
using Elsa.Services;
using Elsa.Services.Models;

namespace Caracal.PayStation.Api.Workflow.Activities.Withdrawals {
    public class ChangeStatus: Activity {
        private ChangeWithdrawalStatusUseCase _useCase;
        
        public ChangeStatus(ChangeWithdrawalStatusUseCase useCase) => 
            _useCase = useCase;

        [ActivityProperty(Hint = "The next status of the withdrawal")]
        public string WorkflowStatus { 
            get => GetState<string>();
            set => SetState(value); 
        }
        
        protected override async Task<ActivityExecutionResult> OnExecuteAsync(WorkflowExecutionContext context, CancellationToken cancellationToken) {
            var id = Convert.ToInt64(context.CurrentScope.GetVariable("withdrawalId"));
            var request = new ChangeWithdrawalStatusRequest {
                new (id, WorkflowStatus)
            };
            
            var result = await _useCase.ExecuteAsync(request, cancellationToken);
            context.CurrentScope.LastResult.Value = result;
            
            return Done();
        }
    }
}