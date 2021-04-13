using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Caracal.Framework.UseCases;
using Caracal.PayStation.PaymentEngine;

namespace Caracal.PayStation.Application.UseCases.Withdrawals.GetWithdrawals {
    public class GetWithdrawalsUseCase: UseCase<GetWithdrawalsResponse, GetWithdrawalsRequest>  {
        private readonly IMapper _mapper;
        private readonly WithdrawalEngine _withdrawalEngine;
        private readonly Caracal.EventBus.EventBus _eventBus;

        public GetWithdrawalsUseCase(WithdrawalEngine withdrawalEngine, Caracal.EventBus.EventBus eventBus) {
            _mapper = Mappings.Create();
            _withdrawalEngine = withdrawalEngine;
            _eventBus = eventBus;
        }

        public override async Task Execute() => 
            Response = _mapper.Map<GetWithdrawalsResponse>(await _withdrawalEngine.GetWithdrawals(Request));
    }
}