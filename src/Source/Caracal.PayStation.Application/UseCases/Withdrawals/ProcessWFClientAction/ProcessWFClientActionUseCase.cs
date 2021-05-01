using System.Collections.Generic;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Caracal.Framework.UseCases;
using Caracal.PayStation.Payments;
using Caracal.PayStation.Payments.Gateways;

namespace Caracal.PayStation.Application.UseCases.Withdrawals.ProcessWFClientAction {
    public class ProcessWFClientActionUseCase: UseCase<ProcessWFClientActionResponse, ProcessWFClientActionRequest> {
        private readonly WorkflowGateway _workflow;
        private readonly WithdrawalService _service;
        
        public ProcessWFClientActionUseCase(WorkflowGateway workflow, WithdrawalService service) {
            _workflow = workflow;
            _service = service;
        }

        public override async Task<ProcessWFClientActionResponse> ExecuteAsync(ProcessWFClientActionRequest request, CancellationToken cancellationToken) {
            Request = request;
            var wfUpdates = new List<Task>();

            foreach (var action in request.Items) {
                var withdrawal = await _service.GetWithdrawal(action.WithdrawalId, cancellationToken);
                
                if(string.IsNullOrWhiteSpace(withdrawal.WorkflowUrl))
                    continue;
                
                wfUpdates.Add(SendWfRequestAsync(withdrawal.WorkflowUrl, action.Payload, cancellationToken));
                await _service.UpdateWfUrlAsync(withdrawal.Id, string.Empty, cancellationToken);
                action.Succeeded = true;
            }
            
            await Task.WhenAll(wfUpdates);

            return new ProcessWFClientActionResponse {Items = request.Items};
        }
        
        private async Task SendWfRequestAsync(string url, object body, CancellationToken cancellationToken) => 
            await _workflow.SendClientEventAsync(url, body, cancellationToken);
    }
}