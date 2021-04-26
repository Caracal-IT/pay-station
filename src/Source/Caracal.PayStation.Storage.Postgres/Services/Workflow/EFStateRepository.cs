using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Caracal.PayStation.Workflow.Models.Withdrawals;
using Caracal.PayStation.Workflow.Repositories;

namespace Caracal.PayStation.Storage.Postgres.Services.Workflow {
    public class EFStateRepository: StateRepository {
        public Task<IEnumerable<WithdrawalStatusUpdateResult>> UpdateWithdrawalStatusAsync(IEnumerable<WithdrawalStatus> statuses, CancellationToken token) {
            throw new System.NotImplementedException();
        }
    }
}