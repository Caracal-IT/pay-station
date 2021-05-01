using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using Caracal.PayStation.Payments.Gateways;

namespace Caracal.PayStation.Api.Gateways {
    public class ApiWorkflowGateway: WorkflowGateway {
        private readonly HttpClient _client;
        public ApiWorkflowGateway(HttpClient client) => _client = client;
        
        public async Task SendClientEventAsync(string url, object request, CancellationToken cancellationToken) {
            await _client.PostAsJsonAsync(url, request, cancellationToken);
        }
    }
}