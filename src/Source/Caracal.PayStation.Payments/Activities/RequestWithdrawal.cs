using System.Dynamic;
using System.Threading;
using System.Threading.Tasks;
using Caracal.PayStation.Payments.Models;
using Elsa.Activities.Http.Models;
using Elsa.Results;
using Elsa.Services;
using Elsa.Services.Models;
using Newtonsoft.Json;

namespace Caracal.PayStation.Payments.Activities {
    public class RequestWithdrawal: Activity {
        private WithdrawalService _service;
        
        public RequestWithdrawal(WithdrawalService service) {
            _service = service;
        }
        
        protected override async Task<ActivityExecutionResult> OnExecuteAsync(WorkflowExecutionContext context, CancellationToken cancellationToken) {
            var request = context.CurrentScope.LastResult.Value as HttpRequestModel;
            dynamic body = request?.Body as ExpandoObject;
            var json = JsonConvert.SerializeObject(body);
            var withdrawal = JsonConvert.DeserializeObject<Withdrawal>(json);

            var result = await _service.AddWithdrawalAsync(withdrawal, cancellationToken);
            
            context.CurrentScope.SetVariable("withdrawalId", result.Id);
            
            return Done();
        }
    }
}