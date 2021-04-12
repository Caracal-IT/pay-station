using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Caracal.PayStation.Web.Gateways.Security.Model.Login;

namespace Caracal.PayStation.Web.Gateways.Security {
    public interface LoginGateway {
        Task<LoginResponse> Login(LoginRequest request);
    }

    public class ApiLoginGateway : LoginGateway {
        private readonly HttpClient _client;

        public ApiLoginGateway(HttpClient client) => _client = client;

        public async Task<LoginResponse> Login(LoginRequest request) {
            var response = await _client.PostAsJsonAsync($"{ApiConfig.PayStationApi}users/Login", request);

            if (!response.IsSuccessStatusCode) return null;
            
            return await response.Content.ReadFromJsonAsync<LoginResponse>();
        }
    }
}