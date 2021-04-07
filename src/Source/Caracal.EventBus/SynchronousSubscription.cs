using System;
using System.Threading;
using System.Threading.Tasks;

namespace Caracal.EventBus {
    internal class SynchronousSubscription<TEvent>: Subscription<TEvent> where TEvent: Event {
        private readonly Action<TEvent> _action;
        
        public SynchronousSubscription(Action<TEvent> action, SubscriptionToken token): base(token) => 
            _action = action;

        internal override Task PublishAsync(Event @eventItem, CancellationToken token) {
            if (eventItem is not TEvent evt)
                throw new ArgumentException("The item is of the wrong type");
            
            _action(evt);

            return Task.CompletedTask;
        }
    }
}