using System;
using System.Linq;
using System.Threading.Tasks;
using Caracal.Security.Model;
using Caracal.Security.Services;
using Caracal.Security.Simulator.Stores;

namespace Caracal.Security.Simulator.Services {
    public class AuthService: LoginService {
        private readonly UserStore _userStore;

        public AuthService() => _userStore = UserStore.Create();

        Task<User> LoginService.Login(Login request) {
            var user = _userStore.FirstOrDefault(l => 
                l.UserName == request.Username 
                &&  l.Password == request.Password 
                && l.TenantId == request.TenantId);
            
            if (user == null || !user.IsAuthorized)
                throw new UnauthorizedAccessException();

            return Task.FromResult<User>(user);
        }
    }
}