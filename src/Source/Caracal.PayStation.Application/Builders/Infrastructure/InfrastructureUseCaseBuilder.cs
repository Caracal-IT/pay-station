using System;
using Caracal.Framework.UseCases;
using Caracal.PayStation.Application.UseCases.Infrastructure.LoginUser;
using Caracal.Security.Services;

namespace Caracal.PayStation.Application.Builders.Infrastructure {
    /*
    public class InfrastructureUseCaseBuilder {
        private readonly LoginService _loginService;
        
        public InfrastructureUseCaseBuilder(LoginService loginService) => _loginService = loginService;

        public TUseCase Build<TUseCase>() where TUseCase: UseCase => typeof(TUseCase).Name switch {
                nameof(LoginUseCase) => new LoginUseCase(_loginService) as TUseCase,
                _ => default
            };
    }
    */
}