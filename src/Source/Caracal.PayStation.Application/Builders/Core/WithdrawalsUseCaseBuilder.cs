using Caracal.Framework.UseCases;
using Caracal.PayStation.Application.UseCases.Infrastructure.LoginUser;
using Caracal.PayStation.Application.UseCases.Withdrawals.GetWithdrawals;
using Caracal.PayStation.PaymentEngine;

namespace Caracal.PayStation.Application.Builders.Core {
    public class WithdrawalsUseCaseBuilder {
        private readonly WithdrawalEngine _withdrawalEngine;
        private readonly Caracal.EventBus.EventBus _eventBus;
        
        public WithdrawalsUseCaseBuilder(WithdrawalEngine withdrawalEngine, Caracal.EventBus.EventBus eventBus) {
            _withdrawalEngine = withdrawalEngine;
            _eventBus = eventBus;
        }
        
        public TUseCase Build<TUseCase>() where TUseCase : UseCase => typeof(TUseCase).Name switch {
            nameof(GetWithdrawalsUseCase) => new GetWithdrawalsUseCase(_withdrawalEngine, _eventBus) as TUseCase,
            _ => default
        };
    }
}