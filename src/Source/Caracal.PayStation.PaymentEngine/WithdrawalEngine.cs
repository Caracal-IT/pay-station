using System.Threading;
using System.Threading.Tasks;
using Caracal.EventBus;
using Caracal.Framework.Data;
using Caracal.PayStation.EventBus.Events.Withdrawals.Withdrawals;
using Caracal.PayStation.PaymentModels.Withdrawals;
using Caracal.PayStation.Storage.Simulator;
using Caracal.PayStation.Withdrawals;

namespace Caracal.PayStation.PaymentEngine {
    public class WithdrawalEngineWithEventBus: WithdrawalEngine {
        private readonly Caracal.EventBus.EventBus _eventBus;
        private SubscriptionToken _subscription;

        public WithdrawalEngineWithEventBus(Caracal.EventBus.EventBus eventBus) {
            _eventBus = eventBus;
        }

        ~WithdrawalEngineWithEventBus() {
            if (_subscription == null) return;
            
            var task = Task.Run(() => _eventBus.UnsubscribeAsync(_subscription, CancellationToken.None));
            Task.WaitAny(task, Task.Delay(4000));
        }

        public async Task RegisterEventListener() {
            if (_subscription != null) return;
            
            _subscription = await _eventBus.SubscribeAsync<RequestWithdrawalsEvent>(ReturnWithdrawals, CancellationToken.None);
        }

        private async Task ReturnWithdrawals(RequestWithdrawalsEvent evt, CancellationToken token) {
            var response = await GetWithdrawals(evt.Data);
            await _eventBus.PublishAsync(new ResponseWithdrawalsEvent{Data = response, CorrelationId = evt.CorrelationId}, token);
        }


        private Task<PagedData<Withdrawal>> GetWithdrawals(PagedDataFilter filter) {
            var response = new PagedData<Withdrawal> {PageNumber = 1, NumberOfResults = 5, NumberOfRows = 5};
            response.Items.AddRange(Store.Withdrawals.Values);
            
            return Task.FromResult(response);
        }
    }
}