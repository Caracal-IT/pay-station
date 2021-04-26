using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Caracal.PayStation.Storage.Simulator.Model.Withdrawals;
using Caracal.PayStation.Workflow.Repositories;

using WithdrawalStatus = Caracal.PayStation.Workflow.Models.Withdrawals.WithdrawalStatus;
using WithdrawalStatusUpdateResult = Caracal.PayStation.Workflow.Models.Withdrawals.WithdrawalStatusUpdateResult;

namespace Caracal.PayStation.Storage.Simulator.Workflow {
    public class MemoryStateRepository : StateRepository {
        public Task<IEnumerable<WithdrawalStatusUpdateResult>> UpdateWithdrawalStatusAsync(IEnumerable<WithdrawalStatus> statuses,
            CancellationToken token) {
            var data = statuses.ToList();
            data.ForEach(ChangeStatus);

            return Task.FromResult<IEnumerable<WithdrawalStatusUpdateResult>>(data.Select(UpdateStatus).ToList());
        }

        private static void ChangeStatus(WithdrawalStatus status) {
            if (!Store.Withdrawals.Keys.Contains(status.WithdrawalId) || string.IsNullOrWhiteSpace(status.Status))
                return;

            var item = Store.Withdrawals[status.WithdrawalId];
            Store.Withdrawals[status.WithdrawalId] = new Withdrawal(item.Id, item.Account, item.Amount, status.Status);
        }

        private static WithdrawalStatusUpdateResult UpdateStatus(WithdrawalStatus status) =>
            new(status, string.Empty, Succeeded: true);
    }
}