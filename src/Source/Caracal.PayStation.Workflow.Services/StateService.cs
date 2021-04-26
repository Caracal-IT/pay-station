using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Caracal.PayStation.Workflow.Models.Withdrawals;
using Caracal.PayStation.Workflow.Repositories;
using Base = Caracal.PayStation.Workflow;

namespace Caracal.PayStation.Workflow.Services {
    public class StateService: Base.StateService {
        private readonly StateRepository _repository;
        
        public StateService(StateRepository repository) => _repository = repository;

        public async Task<IEnumerable<WithdrawalStatusUpdateResult>> UpdateWithdrawalStatusAsync(IEnumerable<WithdrawalStatus> statuses, CancellationToken token) => 
            await _repository.UpdateWithdrawalStatusAsync(statuses, token);
    }
}