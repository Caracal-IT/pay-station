using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Caracal.EventBus;
using Caracal.Framework.UseCases;
using Caracal.PayStation.EventBus.Events.Withdrawals.Workflow;
using Caracal.PayStation.Workflow;
using Model = Caracal.PayStation.Workflow.Models.Withdrawals;

namespace Caracal.PayStation.Application.UseCases.Withdrawals.ChangeStatus {
    public class ChangeWithdrawalStatusUseCase: UseCase<ChangeWithdrawalStatusResponse, ChangeWithdrawalStatusRequest>  {
        private readonly IMapper _mapper;
        private readonly StateService _service;
        private readonly Caracal.EventBus.EventBus _eventBus;

        public ChangeWithdrawalStatusUseCase(Caracal.EventBus.EventBus eventBus, StateService service) {
            _mapper = Mappings.Create();
            _eventBus = eventBus;
            _service = service;
        }
        
        public override async Task<ChangeWithdrawalStatusResponse> ExecuteAsync(ChangeWithdrawalStatusRequest request, CancellationToken cancellationToken) {
            Request = request;
            var result = await _service.UpdateWithdrawalStatusAsync(_mapper.Map<IEnumerable<Model.WithdrawalStatus>>(request), cancellationToken); 
            Response = _mapper.Map<ChangeWithdrawalStatusResponse>(result.ToList());
            
            var evt = new WithdrawalStatusChangeEvent{Data = _mapper.Map<IEnumerable<Model.WithdrawalStatus>>(request)};
            await _eventBus.PublishAsync(evt, cancellationToken);
            
            return Response;
        }
    }
}