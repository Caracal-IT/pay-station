using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Caracal.PayStation.Workflow.Models.Withdrawals;
using Caracal.PayStation.Workflow.Repositories;
using Microsoft.Extensions.Caching.Distributed;
using Base = Caracal.PayStation.Workflow;

namespace Caracal.PayStation.Workflow.Services {
    public class StateService: Base.StateService {
        private readonly StateRepository _repository;
        private IDistributedCache _cache;
        
        public StateService(StateRepository repository, IDistributedCache cache) {
            _repository = repository;
            _cache = cache;
        }

        public async Task<IEnumerable<WithdrawalStatusUpdateResult>> UpdateWithdrawalStatusAsync(IEnumerable<WithdrawalStatus> statuses, CancellationToken token) => 
            await _repository.UpdateWithdrawalStatusAsync(statuses, token);
    }
}