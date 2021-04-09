using System.Threading.Tasks;
using AutoMapper;
using Caracal.Framework.UseCases;
using Caracal.Security.Model;
using Caracal.Security.Services;

namespace Caracal.PayStation.Application.UseCases.Infrastructure.LoginUser {
    public class LoginUseCase: UseCase<LoginResponse, LoginRequest> {
        private readonly IMapper _mapper;
        private readonly LoginService _loginService;

        public LoginUseCase(LoginService loginService) {
            _mapper = Mappings.Create();
            _loginService = loginService;
        }

        public override async Task Execute() {
            var result = await _loginService.Login(_mapper.Map<Login>(Request));
            Response = _mapper.Map<LoginResponse>(result);
        }
    }
}