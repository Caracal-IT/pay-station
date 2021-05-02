using System.Threading;
using System.Threading.Tasks;
using Caracal.Framework.UseCases;
using Caracal.PayStation.Payments;

namespace Caracal.PayStation.Application.UseCases.Withdrawals.UpdateClientEvent {
    public class UpdateClientEventUseCase: UseCase<UpdateClientEventResponse, UpdateClientEventRequest> {
        private readonly WithdrawalService _service;

        public UpdateClientEventUseCase(WithdrawalService service) {
            _service = service;
        }
        public override async Task<UpdateClientEventResponse> ExecuteAsync(UpdateClientEventRequest request, CancellationToken cancellationToken) {
            Request = request;
            var result = await _service.UpdateWfUrlAsync(request.WithdrawalId, request.WorkflowUrl, cancellationToken);
            Response = new UpdateClientEventResponse { IsSuccessful = result };

            return Response;
        }
    }
}