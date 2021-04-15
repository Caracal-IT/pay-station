using Caracal.Framework.UseCases;
using Caracal.PayStation.Application.UseCases.Withdrawals.ChangeStatus;
using Caracal.PayStation.Application.UseCases.Withdrawals.GetWithdrawals;

namespace Caracal.PayStation.Application.Builders.Core {
    public class WithdrawalsUseCaseBuilder {
        private readonly Caracal.EventBus.EventBus _eventBus;
        
        public WithdrawalsUseCaseBuilder(Caracal.EventBus.EventBus eventBus) => _eventBus = eventBus;

        public TUseCase Build<TUseCase>() where TUseCase : UseCase => typeof(TUseCase).Name switch {
            nameof(ChangeWithdrawalStatusUseCase) => new ChangeWithdrawalStatusUseCase(_eventBus) as TUseCase,
            nameof(GetWithdrawalsUseCase) => new GetWithdrawalsUseCase(_eventBus) as TUseCase,
            //nameof(ChangeWithdrawalStatusUseCase) => new ChangeWithdrawalStatusUseCase(_withdrawalEngine, _eventBus) as TUseCase,
            _ => default
        };
    }
}