using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Caracal.Framework.Data;
using Caracal.PayStation.Web.Gateways.Core.Withdrawals.Model;
using static System.Text.Json.JsonSerializer;

namespace Caracal.PayStation.Web.Gateways.Core.Withdrawals {
    public interface WithdrawalGateway {
        Task<PagedData<Withdrawal>?> GetWithdrawalsAsync(PagedDataFilter request, CancellationToken cancellationToken);
        Task<List<WorkflowAction>?> ProcessClientActionAsync(List<WorkflowAction> request, CancellationToken cancellationToken);

        Task<string> ExportAsync(CancellationToken cancellationToken);
    }
    
    public class ApiWithdrawalGateway: WithdrawalGateway {
        private readonly JsonSerializerOptions _options = new() {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };

        private readonly HttpClient _client;

        public ApiWithdrawalGateway(HttpClient client) => _client = client;
        
        public async Task<PagedData<Withdrawal>?> GetWithdrawalsAsync(PagedDataFilter request, CancellationToken cancellationToken) {
            var responseMessage = await _client.PostAsJsonAsync("withdrawal/filter", request, cancellationToken);
            var stream = await responseMessage.Content.ReadAsStreamAsync(cancellationToken);
            return await DeserializeAsync<PagedData<Withdrawal>>(stream, _options, cancellationToken);
        }

        public async Task<List<WorkflowAction>?> ProcessClientActionAsync(
            List<WorkflowAction> request,
            CancellationToken cancellationToken) {
            
            var responseMessage = await _client.PostAsJsonAsync("withdrawal/client/action", request, cancellationToken);
            var stream = await responseMessage.Content.ReadAsStreamAsync(cancellationToken);
            return await DeserializeAsync<List<WorkflowAction>>(stream, _options, cancellationToken);
        }

        public async Task<string> ExportAsync(CancellationToken cancellationToken) {
            var responseMessage = await _client.GetAsync("withdrawal/export", cancellationToken);
            return await responseMessage.Content.ReadAsStringAsync(cancellationToken);
        }
    }
}