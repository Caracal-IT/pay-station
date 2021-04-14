using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Caracal.EventBus;
using Caracal.Framework.Data;
using Caracal.Framework.UseCases;
using Caracal.PayStation.EventBus.Events.Withdrawals;
using Model = Caracal.PayStation.PaymentModels.Withdrawals;

namespace Caracal.PayStation.Application.UseCases.Withdrawals.FlushWithdrawals {
    public class FlushWithdrawalsUseCase: UseCase<FlushWithdrawalsResponse, FlushWithdrawalsRequest>  {
        private readonly IMapper _mapper;
        //private readonly WithdrawalEngine _withdrawalEngine;
        private readonly Caracal.EventBus.EventBus _eventBus;

        public FlushWithdrawalsUseCase(Caracal.EventBus.EventBus eventBus) {
            _mapper = Mappings.Create();
            _eventBus = eventBus;
        }

        /*
        public FlushWithdrawalsUseCase(WithdrawalEngine withdrawalEngine) {
            _mapper = Mappings.Create();
            _withdrawalEngine = withdrawalEngine;
        }
        */
        
        public override async Task<FlushWithdrawalsResponse> ExecuteAsync(FlushWithdrawalsRequest request) {
            Request = request;
            
            var evt = new RequestFlushEvent{Data = request};
            var result = await _eventBus.SendAndListenAsync<PagedData<Model.Withdrawal>, ResponseFlushEvent>(evt, CancellationToken.None);
            Response = _mapper.Map<FlushWithdrawalsResponse>(result);
            return Response;
        }

        //public override async Task Execute() {
        //    Response = _mapper.Map<FlushWithdrawalsResponse>(await _withdrawalEngine.GetWithdrawals(Request));
        //}

        private Task SetWithdrawals(ResponseFlushEvent evt, CancellationToken token) {
            Response = _mapper.Map<FlushWithdrawalsResponse>(evt.Data);

            return Task.CompletedTask;
        }
    }
}