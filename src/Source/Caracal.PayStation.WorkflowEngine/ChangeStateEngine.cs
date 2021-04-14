using System;
using System.Threading;
using System.Threading.Tasks;
using Caracal.EventBus;
using Caracal.Framework.Data;
using Caracal.PayStation.EventBus.Events.Withdrawals;
using Caracal.PayStation.PaymentModels.Withdrawals;
using Caracal.PayStation.Storage.Simulator;

namespace Caracal.PayStation.WorkflowEngine {
    public interface ChangeStateEngine {
        Task RegisterEventListener();
    }

    public class ChangeStateEngineWithEventBus : ChangeStateEngine {
        private readonly Caracal.EventBus.EventBus _eventBus;
        private SubscriptionToken _subscription;
        
        public ChangeStateEngineWithEventBus(Caracal.EventBus.EventBus eventBus) {
            _eventBus = eventBus;
        }

        ~ChangeStateEngineWithEventBus() {
            if (_subscription == null) return;
            
            var task = Task.Run(() => _eventBus.UnsubscribeAsync(_subscription, CancellationToken.None));
            Task.WaitAny(task);
        }

        public async Task RegisterEventListener() {
            if (_subscription != null) return;
            
            _subscription = await _eventBus.SubscribeAsync<RequestFlushEvent>(ReturnWithdrawals, CancellationToken.None);
        }

        private async Task ReturnWithdrawals(RequestFlushEvent evt, CancellationToken token) {
            var response = await GetWithdrawals(evt.Data);
            await _eventBus.PublishAsync(new ResponseFlushEvent{Data = response, CorrelationId = evt.CorrelationId}, token);
        }


        private Task<PagedData<Withdrawal>> GetWithdrawals(PagedDataFilter filter) {
            var response = new PagedData<Withdrawal> {PageNumber = 1, NumberOfResults = 5, NumberOfRows = 5};
            response.Items.AddRange(Store.Withdrawals);

            var item = response.Items[2];
            response.Items[2] = new Withdrawal(item.Id, item.Account, item.Amount, "Flushed");
            
            return Task.FromResult(response);
        }
    }
}