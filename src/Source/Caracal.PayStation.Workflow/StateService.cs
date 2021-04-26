using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Caracal.PayStation.Workflow.Models.Withdrawals;

namespace Caracal.PayStation.Workflow {
    public interface StateService {
        Task<IEnumerable<WithdrawalStatusUpdateResult>> UpdateWithdrawalStatusAsync(IEnumerable<WithdrawalStatus> statuses, CancellationToken token);
    }
}