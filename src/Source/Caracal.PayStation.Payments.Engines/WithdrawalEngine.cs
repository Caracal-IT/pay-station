using System.Threading;
using System.Threading.Tasks;
using Caracal.EventBus;
using Caracal.PayStation.EventBus.Events.Withdrawals.Withdrawals;

namespace Caracal.PayStation.Payments.Engines {
    public class WithdrawalEngineWithEventBus: WithdrawalEngine {
        private readonly Caracal.EventBus.EventBus _eventBus;
        private readonly WithdrawalService _withdrawalService;
        
        private SubscriptionToken _subscription;

        public WithdrawalEngineWithEventBus(Caracal.EventBus.EventBus eventBus, WithdrawalService withdrawalService) {
            _eventBus = eventBus;
            _withdrawalService = withdrawalService;
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
            var response = await _withdrawalService.GetWithdrawalsAsync(evt.Data, token);
            await _eventBus.PublishAsync(new ResponseWithdrawalsEvent{Data = response, CorrelationId = evt.CorrelationId}, token);
        }
    }
}