using System.Threading.Tasks;
using AutoMapper;
using Caracal.Framework.UseCases;
using Caracal.Security.Model;
using Caracal.Security.Services;

namespace Caracal.PayStation.Application.UseCases.Infrastructure.LoginUser {
    public class LoginUseCase: UseCase<LoginResponse, LoginRequest> {
        private readonly IMapper _mapper;
        private readonly AuthService _authService;

        public LoginUseCase(AuthService authService) {
            _mapper = Mappings.Create();
            _authService = authService;
        }

        public override async Task Execute() {
            var result = await _authService.Login(_mapper.Map<Login>(Request));
            Response = _mapper.Map<LoginResponse>(result);
        }
    }
}