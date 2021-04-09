using System.Threading.Tasks;
using Caracal.Security.Model;

namespace Caracal.Security.Services {
    public interface AuthService {
        Task<User> Login(Login request);
    }
}