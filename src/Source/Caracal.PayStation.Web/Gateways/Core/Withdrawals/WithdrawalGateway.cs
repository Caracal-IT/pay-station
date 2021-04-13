using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Caracal.Framework.Data;
using Caracal.PayStation.Web.Gateways.Core.Withdrawals.Model;

namespace Caracal.PayStation.Web.Gateways.Core.Withdrawals {
    public interface WithdrawalGateway {
        Task<PagedData<Withdrawal>> GetWithdrawalsAsync(PagedDataFilter request);
    }
    
    public class ApiWithdrawalGateway: WithdrawalGateway {
        private readonly HttpClient _client;

        public ApiWithdrawalGateway(HttpClient client) => _client = client;
        
        public async Task<PagedData<Withdrawal>> GetWithdrawalsAsync(PagedDataFilter request) {
            var response = await _client.PostAsJsonAsync($"{ApiConfig.PayStationApi}Withdrawals/filter", request);

            if (!response.IsSuccessStatusCode) return null;
            
            return await response.Content.ReadFromJsonAsync<PagedData<Withdrawal>>();
        }
    }
}