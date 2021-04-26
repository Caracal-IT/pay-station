using System.Threading;
using System.Threading.Tasks;
using Caracal.Framework.Data;
using Caracal.PayStation.Payments.Models;

namespace Caracal.PayStation.Payments.Repositories {
    public interface WithdrawalsRepository {
        Task<PagedData<Withdrawal>> GetWithdrawalsAsync(PagedDataFilter filter, CancellationToken cancellationToken);
    }
}