using System.Threading;
using System.Threading.Tasks;
using Caracal.EventBus;
using Caracal.PayStation.EventBus.Events.Withdrawals.Workflow.WithdrawalStatusChange;
using Caracal.PayStation.Workflow;

namespace Caracal.PayStation.WorkflowEngine {
    public class ChangeStateEngineWithEventBus : ChangeStateEngine {
        private readonly Caracal.EventBus.EventBus _eventBus;
        private readonly StateService _stateService; 
            
        private SubscriptionToken _subscription;
        
        public ChangeStateEngineWithEventBus(Caracal.EventBus.EventBus eventBus, StateService stateService) {
            _eventBus = eventBus;
            _stateService = stateService;
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
            var response = await _stateService.UpdateWithdrawalStatusAsync(evt.Data, token);
            await _eventBus.PublishAsync(new WithdrawalStatusChangedEvent{Data = response, CorrelationId = evt.CorrelationId}, token);
        }
    }
}