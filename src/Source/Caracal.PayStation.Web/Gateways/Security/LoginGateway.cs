using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Caracal.PayStation.Web.Gateways.Security.Model.Login;

using static System.Text.Json.JsonSerializer;

namespace Caracal.PayStation.Web.Gateways.Security {
    public interface LoginGateway {
        Task<LoginResponse> LoginAsync(LoginRequest request, CancellationToken cancellationToken);
    }

    public class ApiLoginGateway : LoginGateway {
        private readonly JsonSerializerOptions _options = new() {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };

        private readonly HttpClient _client;

        public ApiLoginGateway(HttpClient client) => _client = client;

        public async Task<LoginResponse> LoginAsync(LoginRequest request, CancellationToken cancellationToken) {
            var responseMessage = await _client.PostAsJsonAsync("users/login", request, cancellationToken);
            var stream = await responseMessage.Content.ReadAsStreamAsync(cancellationToken);
            return await DeserializeAsync<LoginResponse>(stream, _options, cancellationToken);
        }
    }
}