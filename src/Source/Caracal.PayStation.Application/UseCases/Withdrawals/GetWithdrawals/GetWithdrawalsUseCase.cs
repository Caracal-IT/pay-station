using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Caracal.EventBus;
using Caracal.Framework.Data;
using Caracal.Framework.UseCases;
using Caracal.PayStation.EventBus.Events.Withdrawals;
using Caracal.PayStation.EventBus.Events.Withdrawals.Withdrawals;
using Caracal.PayStation.PaymentEngine;
using Model = Caracal.PayStation.PaymentModels.Withdrawals;

namespace Caracal.PayStation.Application.UseCases.Withdrawals.GetWithdrawals {
    public class GetWithdrawalsUseCase: UseCase<GetWithdrawalsResponse, GetWithdrawalsRequest>  {
        private readonly IMapper _mapper;
        //private readonly WithdrawalEngine _withdrawalEngine;
        private readonly Caracal.EventBus.EventBus _eventBus;

        public GetWithdrawalsUseCase(Caracal.EventBus.EventBus eventBus) {
            _mapper = Mappings.Create();
            _eventBus = eventBus;
        }

        /*
        public ChangeWithdrawalStatusUseCase(WithdrawalEngine withdrawalEngine) {
            _mapper = Mappings.Create();
            _withdrawalEngine = withdrawalEngine;
        }
        */
        
        public override async Task<GetWithdrawalsResponse> ExecuteAsync(GetWithdrawalsRequest request) {
            Request = request;
            
            var evt = new RequestWithdrawalsEvent{Data = request};
            var result = await _eventBus.SendAndListenAsync<PagedData<Model.Withdrawal>, ResponseWithdrawalsEvent>(evt, CancellationToken.None);
            Response = _mapper.Map<GetWithdrawalsResponse>(result);
            return Response;
        }

        //public override async Task Execute() {
        //    Response = _mapper.Map<ChangeWithdrawalStatusResponse>(await _withdrawalEngine.GetWithdrawals(Request));
        //}

        private Task SetWithdrawals(ResponseWithdrawalsEvent evt, CancellationToken token) {
            Response = _mapper.Map<GetWithdrawalsResponse>(evt.Data);

            return Task.CompletedTask;
        }
    }
}