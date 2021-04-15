using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Caracal.EventBus;
using Caracal.PayStation.EventBus.Events.Withdrawals.Workflow.WithdrawalStatusChange;
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
            
            _subscription = await _eventBus.SubscribeAsync<WithdrawalStatusChangeEvent>(UpdateWithdrawalStatus, CancellationToken.None);
        }

        private async Task UpdateWithdrawalStatus(WithdrawalStatusChangeEvent evt, CancellationToken token) {
            evt.Data
               .ToList()
               .ForEach(ChangeStatus);

            var response = evt
                .Data
                .Select(UpdateStatus);
                
            await _eventBus.PublishAsync(new WithdrawalStatusChangedEvent{Data = response, CorrelationId = evt.CorrelationId}, token);
        }

        private static void ChangeStatus(WithdrawalStatus status) {
            if (!Store.Withdrawals.Keys.Contains(status.WithdrawalId))
                return;
                
            var item = Store.Withdrawals[status.WithdrawalId];
            Store.Withdrawals[status.WithdrawalId] = new Withdrawal(item.Id, item.Account, item.Amount, status.Status);
        }

        private static WithdrawalStatusUpdateResult UpdateStatus(WithdrawalStatus status) =>
            new (status, string.Empty, Succeeded: true);
    }
}