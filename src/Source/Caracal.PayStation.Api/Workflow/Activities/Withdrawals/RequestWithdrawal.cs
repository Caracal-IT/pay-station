using System.Dynamic;
using System.Threading;
using System.Threading.Tasks;
using Caracal.PayStation.Application.UseCases.Withdrawals.RequestWithdrawal;

using Elsa.Activities.Http.Models;
using Elsa.Results;
using Elsa.Services;
using Elsa.Services.Models;
using Newtonsoft.Json;

namespace Caracal.PayStation.Api.Workflow.Activities.Withdrawals {
    public class RequestWithdrawal: Activity {
        private RequestWithdrawalUseCase _useCase;
        
        public RequestWithdrawal(RequestWithdrawalUseCase useCase) => 
            _useCase = useCase;

        protected override async Task<ActivityExecutionResult> OnExecuteAsync(WorkflowExecutionContext context, CancellationToken cancellationToken) {
            var requestModel = context.CurrentScope.LastResult.Value as HttpRequestModel;
            dynamic? body = requestModel?.Body as ExpandoObject;
            var json = JsonConvert.SerializeObject(body);
            var request = JsonConvert.DeserializeObject<RequestWithdrawalRequest>(json);

            var result = await _useCase.ExecuteAsync(request, cancellationToken);
            
            context.CurrentScope.SetVariable("withdrawalId", result.Id);
            context.CurrentScope.LastResult.Value = result;
            
            return Done();
        }
    }
}