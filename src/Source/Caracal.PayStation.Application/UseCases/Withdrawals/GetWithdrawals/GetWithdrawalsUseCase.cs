using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Caracal.EventBus;
using Caracal.Framework.Data;
using Caracal.Framework.UseCases;
using Caracal.PayStation.EventBus.Events.Withdrawals.Withdrawals;
using Caracal.PayStation.Payments.Repositories;

namespace Caracal.PayStation.Application.UseCases.Withdrawals.GetWithdrawals {
    public class GetWithdrawalsUseCase: UseCase<GetWithdrawalsResponse, GetWithdrawalsRequest>  {
        private readonly IMapper _mapper;
        private readonly Caracal.EventBus.EventBus _eventBus;
        private readonly WithdrawalsRepository _repository;

        public GetWithdrawalsUseCase(Caracal.EventBus.EventBus eventBus, WithdrawalsRepository repository) {
            _mapper = Mappings.Create();
            _eventBus = eventBus;
            _repository = repository;
        }
        
        public override async Task<GetWithdrawalsResponse> ExecuteAsync(GetWithdrawalsRequest request, CancellationToken cancellationToken) {
            Request = request;
            var evt = new RequestWithdrawalsEvent{Data = request};
            var result = await _repository.GetWithdrawalsAsync(request, cancellationToken);
            // _eventBus.SendAndListenAsync<PagedData<Payments.Models.Withdrawal>, ResponseWithdrawalsEvent>(evt, cancellationToken);
            Response = _mapper.Map<GetWithdrawalsResponse>(result);
            
            return Response;
        }
    }
}