using System.Threading;
using System.Threading.Tasks;
using Caracal.Framework.Data;
using Caracal.PayStation.Payments.Models;
using Caracal.PayStation.Payments.Repositories;

namespace Caracal.PayStation.Storage.Postgres.Services.Payments {
    public class EFWithdrawalsRepository: WithdrawalsRepository {
        public Task<PagedData<Withdrawal>> GetWithdrawalsAsync(PagedDataFilter filter, CancellationToken cancellationToken) {
            throw new System.NotImplementedException();
        }
    }
}