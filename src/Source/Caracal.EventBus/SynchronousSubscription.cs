using System;
using System.Threading;
using System.Threading.Tasks;

namespace Caracal.EventBus {
    internal class SynchronousSubscription<TEvent>: Subscription<TEvent> where TEvent: Event {
        private readonly Action<TEvent> _action;
        
        public SynchronousSubscription(Action<TEvent> action, SubscriptionToken token): base(token) => 
            _action = action;

        internal override Task PublishAsync(Event eventItem, CancellationToken token) {
            _action((TEvent) eventItem);

            return Task.CompletedTask;
        }
    }
}