// ReSharper disable InconsistentNaming

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
            var result = await _service.Login(new Login(username, password, tenantId));
            
            result.UserId.Should().Be(userId);
        }
    }
}