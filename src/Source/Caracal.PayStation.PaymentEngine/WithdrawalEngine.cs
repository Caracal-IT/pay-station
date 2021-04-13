using System.Linq;
using System.Threading.Tasks;
using Caracal.Framework.Data;
using Caracal.PayStation.PaymentModels.Withdrawals;

namespace Caracal.PayStation.PaymentEngine {
    public interface WithdrawalEngine {
        Task<PagedData<Withdrawal>> GetWithdrawals(PagedDataFilter filter);
    }
    
    public class WithdrawalEngineWithEventBus: WithdrawalEngine {
        private Caracal.EventBus.EventBus _eventBus;

        public WithdrawalEngineWithEventBus(Caracal.EventBus.EventBus eventBus) {
            _eventBus = eventBus;
        }
        
        public Task<PagedData<Withdrawal>> GetWithdrawals(PagedDataFilter filter) {
            var response = new PagedData<Withdrawal> {PageNumber = 1, NumberOfResults = 5, NumberOfRows = 5};

            foreach (var i in Enumerable.Range(1, 5))
                response.Items.Add(new Withdrawal(i, $"account {i}", $"R {i}0.44", "Requested"));
            
            return Task.FromResult(response);
        }
    }
}