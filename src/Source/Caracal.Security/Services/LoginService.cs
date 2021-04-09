using System.Threading.Tasks;
using Caracal.Security.Model;

namespace Caracal.Security.Services {
    public interface LoginService {
        Task<User> Login(Login request);
    }
}