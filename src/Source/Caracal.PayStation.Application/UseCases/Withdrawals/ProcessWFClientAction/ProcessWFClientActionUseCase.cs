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

            foreach(var i in request.Items)
                await ProcessAsync(i);

            return new ProcessWFClientActionResponse {Items = request.Items};

            async Task ProcessAsync(WorkflowAction action) {
                var withdrawal = await _service.GetWithdrawal(action.WithdrawalId, cancellationToken);

                if (string.IsNullOrWhiteSpace(withdrawal.WorkflowUrl))
                    return;
                
                await _service.UpdateWfUrlAsync(withdrawal.Id, string.Empty, cancellationToken);
                await _workflow.SendClientEventAsync(withdrawal.WorkflowUrl, action.Payload, cancellationToken);
                action.Succeeded = true;
            }
        }
    }
}