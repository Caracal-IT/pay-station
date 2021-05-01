using System.Threading;
using System.Threading.Tasks;

namespace Caracal.PayStation.Payments.Gateways {
    public interface WorkflowGateway {
        Task SendClientEventAsync(string url, object body, CancellationToken cancellationToken);
    }
}