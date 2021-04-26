using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Caracal.PayStation.Workflow.Models.Withdrawals;

namespace Caracal.PayStation.Workflow.Repositories {
    public interface StateRepository {
        Task<IEnumerable<WithdrawalStatusUpdateResult>> UpdateWithdrawalStatusAsync(IEnumerable<WithdrawalStatus> statuses, CancellationToken token);
    }
}