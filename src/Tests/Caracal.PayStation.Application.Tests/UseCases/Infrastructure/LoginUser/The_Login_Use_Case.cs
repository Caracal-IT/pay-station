// ReSharper disable InconsistentNaming

using System.Threading.Tasks;
using Caracal.Framework.UseCases;
using Caracal.PayStation.Application.UseCases.Infrastructure.LoginUser;
using Caracal.Security.Model;
using Caracal.Security.Services;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Caracal.PayStation.Application.Tests.UseCases.Infrastructure.LoginUser {
    public class The_Login_Use_Case {
        private readonly LoginResponse _response = new (777);
        private readonly LoginRequest _request = new ("JoeSoap", "Password1", 1234);
        
        private readonly UseCase<LoginResponse, LoginRequest> _useCase;
        
        public The_Login_Use_Case() =>
            _useCase = new LoginUseCase(CreateAuthService());
        
        [Fact]
        public async Task Should_Login_User_When_Setting_Request() {
            _useCase.Request = _request;
            await _useCase.Execute();

            _useCase.Response.Should().Be(_response);
        }

        [Fact]
        public async Task Should_Login_With() {
            var result = await _useCase.Execute(_request);
            
            result.Should().Be(_response);
        }

        private AuthService CreateAuthService() {
            var authService = Substitute.For<AuthService>();
            
            authService.Login(new Login(_request.Username, _request.Password, _request.TenantId))
                       .Returns(new User(_response.UserId));

            return authService;
        }
    }
}