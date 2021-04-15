using System.Threading;
using System.Threading.Tasks;
using Caracal.Security.Model;

namespace Caracal.Security.Services {
    public interface LoginService {
        Task<User> LoginAsync(Login request, CancellationToken cancellationToken);
    }
}