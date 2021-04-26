using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Caracal.Framework.Data;
using Caracal.PayStation.Payments.Models;
using Caracal.PayStation.Payments.Repositories;

namespace Caracal.PayStation.Storage.Simulator.Payments {
    public class MemoryWithdrawalsRepository : WithdrawalsRepository {
        public Task<PagedData<Withdrawal>> GetWithdrawalsAsync(PagedDataFilter filter, CancellationToken cancellationToken) {
            var response = new PagedData<Withdrawal> {PageNumber = 1, NumberOfResults = 5, NumberOfRows = 5};
            response.Items.AddRange(Store.Withdrawals.Values.Select(i => new Withdrawal(i.Id, i.Account, i.Amount, i.Status)));
            
            return Task.FromResult(response);
        }
    }
}