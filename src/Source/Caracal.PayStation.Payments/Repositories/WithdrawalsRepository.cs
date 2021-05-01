using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Caracal.Framework.Data;
using Caracal.PayStation.Payments.Models;

namespace Caracal.PayStation.Payments.Repositories {
    public interface WithdrawalsRepository {
        Task<PagedData<Withdrawal>> GetWithdrawalsAsync(PagedDataFilter filter, CancellationToken cancellationToken);
        Task<Withdrawal> AddWithdrawalAsync(Withdrawal withdrawal, CancellationToken cancellationToken);
        Task<IEnumerable<WithdrawalStatusUpdateResult>> UpdateWithdrawalStatusAsync(IEnumerable<WithdrawalStatus> statuses, CancellationToken token);
        Task<bool> UpdateWfUrlAsync(long withdrawalId, string url, CancellationToken token);
        Task<Withdrawal> GetWithdrawal(long id, CancellationToken token);
        Task<bool> UpdateAmountAsync(long id, string amount, CancellationToken token);
    }
}