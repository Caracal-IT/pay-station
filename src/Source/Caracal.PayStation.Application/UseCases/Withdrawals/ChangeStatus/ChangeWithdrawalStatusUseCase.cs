using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Caracal.Framework.UseCases;
using Caracal.PayStation.EventBus.Events.Withdrawals;
using Caracal.PayStation.Payments;
using Caracal.PayStation.Payments.Gateways;

namespace Caracal.PayStation.Application.UseCases.Withdrawals.ChangeStatus {
    public class ChangeWithdrawalStatusUseCase: UseCase<ChangeWithdrawalStatusResponse, ChangeWithdrawalStatusRequest>  {
        private readonly IMapper _mapper;
        private readonly WithdrawalService _service;
        private readonly Caracal.EventBus.EventBus _eventBus;
        private readonly WorkflowGateway _workflow;

        public ChangeWithdrawalStatusUseCase(Caracal.EventBus.EventBus eventBus, WithdrawalService service, WorkflowGateway workflow) {
            _mapper = Mappings.Create();
            _eventBus = eventBus;
            _service = service;
            _workflow = workflow;
        }
        
        public override async Task<ChangeWithdrawalStatusResponse> ExecuteAsync(ChangeWithdrawalStatusRequest request, CancellationToken cancellationToken) {
            Request = request;
            var results = await _service.UpdateWithdrawalStatusAsync(_mapper.Map<IEnumerable<Payments.Models.WithdrawalStatus>>(request), cancellationToken);
            Response = _mapper.Map<ChangeWithdrawalStatusResponse>(results);

            var evt = new WithdrawalStatusChangeEvent{Data = _mapper.Map<IEnumerable<Payments.Models.WithdrawalStatus>>(request)};
            await _eventBus.PublishAsync(evt, cancellationToken);
            
            return Response;
        }
    }
}