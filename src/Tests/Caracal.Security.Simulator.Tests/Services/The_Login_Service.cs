// ReSharper disable InconsistentNaming

using System;
using System.Threading;
using System.Threading.Tasks;
using Caracal.Security.Model;
using Caracal.Security.Services;
using Caracal.Security.Simulator.Services;
using FluentAssertions;
using Xunit;

namespace Caracal.Security.Simulator.Tests.Services {
    public class The_Login_Service {
        private readonly LoginService _service;

        public The_Login_Service() => _service = new AuthService();
        
        [Theory]
        [InlineData(3, "JoeS", "Password1", 100)]
        [InlineData(5, "RandyK", "MySecret", 207)]
        [InlineData(17, "KateJ", "Nothing", 207)]
        public async Task Should_Login_Valid_Users(long userId, string username, string password, long tenantId) {
            var result = await _service.LoginAsync(new Login(username, password, tenantId), CancellationToken.None);
            
            result.UserId.Should().Be(userId);
        }

        [Theory]
        [InlineData("Invalid", "Password1", 100)]
        [InlineData("JoeS", "Invalid", 100)]
        [InlineData("JoeS", "Password1", 101)]
        public void Should_Throw_Unauthorized_Access_Exception_If_User_Login_Failed(string username, string password, long tenantId) {
            _service
                .Invoking(async s => await s.LoginAsync(new Login(username, password, tenantId), CancellationToken.None))
                .Should()
                .Throw<UnauthorizedAccessException>()
                .WithMessage("Attempted to perform an unauthorized operation.");
        }
    }
}