// ReSharper disable InconsistentNaming
// ReSharper disable ClassNeverInstantiated.Local

using Caracal.Framework.UseCases;
using Caracal.PayStation.Application.Builders.Infrastructure;
using Caracal.PayStation.Application.UseCases.Infrastructure.LoginUser;
using Caracal.Security.Services;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Caracal.PayStation.Application.Tests.Builders.Infrastructure {
    public class The_Infrastructure_Use_Case_Builder {
        private readonly InfrastructureUseCaseBuilder _builder;
        
        public The_Infrastructure_Use_Case_Builder() {
            var loginService = Substitute.For<LoginService>();
            _builder = new InfrastructureUseCaseBuilder(loginService);
        }

        [Fact]
        public void Should_Not_Create_Use_Case_If_Not_Mapped() {
            var useCase = _builder.Build<MockUseCase>();
            useCase.Should().BeNull();
        }
        
        [Fact]
        public void Should_Create_Login_Use_Case() {
            var useCase = _builder.Build<LoginUseCase>();
            useCase.Should().BeOfType<LoginUseCase>().And.NotBeNull();
        }

        private class MockUseCase : UseCase { }
    }
}