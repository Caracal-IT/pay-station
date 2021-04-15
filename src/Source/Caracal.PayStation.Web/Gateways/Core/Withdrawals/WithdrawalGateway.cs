using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using Caracal.Framework.Data;
using Caracal.PayStation.Web.Gateways.Core.Withdrawals.Model;

namespace Caracal.PayStation.Web.Gateways.Core.Withdrawals {
    public interface WithdrawalGateway {
        Task<PagedData<Withdrawal>> GetWithdrawalsAsync(PagedDataFilter request, CancellationToken cancellationToken);
        Task<IEnumerable<WithdrawalStatusUpdateResult>> UpdateStatusAsync(IEnumerable<WithdrawalStatus> request, CancellationToken cancellationToken);
    }
    
    public class ApiWithdrawalGateway: WithdrawalGateway {
        private readonly HttpClient _client;

        public ApiWithdrawalGateway(HttpClient client) => _client = client;
        
        public async Task<PagedData<Withdrawal>> GetWithdrawalsAsync(PagedDataFilter request, CancellationToken cancellationToken) {
            var response = await _client.PostAsJsonAsync($"{ApiConfig.PayStationApi}Withdrawal/filter", request, cancellationToken);

            if (!response.IsSuccessStatusCode) return null;
            
            return await response.Content.ReadFromJsonAsync<PagedData<Withdrawal>>(cancellationToken: cancellationToken);
        }
        
        public async Task<IEnumerable<WithdrawalStatusUpdateResult>> UpdateStatusAsync(IEnumerable<WithdrawalStatus> request, CancellationToken cancellationToken) {
            var response = await _client.PostAsJsonAsync($"{ApiConfig.PayStationApi}Withdrawal/status/update", request, cancellationToken);

            if (!response.IsSuccessStatusCode) return null;
            
            return await response.Content.ReadFromJsonAsync<IEnumerable<WithdrawalStatusUpdateResult>>(cancellationToken: cancellationToken);
        }
    }
}