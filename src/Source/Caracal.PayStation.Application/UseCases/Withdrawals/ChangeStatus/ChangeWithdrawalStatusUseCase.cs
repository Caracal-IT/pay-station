using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Caracal.EventBus;
using Caracal.Framework.UseCases;
using Caracal.PayStation.EventBus.Events.Withdrawals.Workflow.WithdrawalStatusChange;
using Model = Caracal.PayStation.Workflow.Models.Withdrawals;

namespace Caracal.PayStation.Application.UseCases.Withdrawals.ChangeStatus {
    public class ChangeWithdrawalStatusUseCase: UseCase<ChangeWithdrawalStatusResponse, ChangeWithdrawalStatusRequest>  {
        private readonly IMapper _mapper;
        private readonly Caracal.EventBus.EventBus _eventBus;

        public ChangeWithdrawalStatusUseCase(Caracal.EventBus.EventBus eventBus) {
            _mapper = Mappings.Create();
            _eventBus = eventBus;
        }
        
        public override async Task<ChangeWithdrawalStatusResponse> ExecuteAsync(ChangeWithdrawalStatusRequest request, CancellationToken cancellationToken) {
            Request = request;
            var evt = new WithdrawalStatusChangeEvent{Data = _mapper.Map<IEnumerable<Model.WithdrawalStatus>>(request)};
            var result = await _eventBus.SendAndListenAsync<IEnumerable<Model.WithdrawalStatusUpdateResult>, WithdrawalStatusChangedEvent>(evt, cancellationToken);
            Response = _mapper.Map<ChangeWithdrawalStatusResponse>(result);
            
            return Response;
        }
    }
}