using Caracal.Framework.UseCases;
using Caracal.PayStation.Application.UseCases.Infrastructure.LoginUser;
using Caracal.PayStation.Application.UseCases.Withdrawals.GetWithdrawals;

namespace Caracal.PayStation.Application.Builders.Core {
    public class WithdrawalsUseCaseBuilder {
        public TUseCase Build<TUseCase>() where TUseCase : UseCase => typeof(TUseCase).Name switch {
            nameof(GetWithdrawalsUseCase) => new GetWithdrawalsUseCase() as TUseCase,
            _ => default
        };
    }
}