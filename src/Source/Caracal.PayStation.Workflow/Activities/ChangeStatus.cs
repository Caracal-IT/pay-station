using System;
using System.Threading;
using System.Threading.Tasks;
using Caracal.PayStation.Workflow.Models.Withdrawals;
using Elsa.Attributes;
using Elsa.Results;
using Elsa.Services;
using Elsa.Services.Models;

namespace Caracal.PayStation.Workflow.Activities {
    public class ChangeState: Activity {
        private StateService _service;
        
        public ChangeState(StateService service) {
            _service = service;
        }
        
        [ActivityProperty(Hint = "The next status of the withdrawal")]
        public string WorkflowStatus { 
            get => GetState<string>();
            set => SetState(value); 
        }
        
        protected override async Task<ActivityExecutionResult> OnExecuteAsync(WorkflowExecutionContext context, CancellationToken cancellationToken) {
            var id = Convert.ToInt64(context.CurrentScope.GetVariable("withdrawalId"));
            await _service.UpdateWithdrawalStatusAsync(new[] {
                new WithdrawalStatus(id, WorkflowStatus)
            }, cancellationToken);
            
            return Done();
        }
    }
}