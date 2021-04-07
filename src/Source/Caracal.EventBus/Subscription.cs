using System;
using System.Threading;
using System.Threading.Tasks;

namespace Caracal.EventBus {
    public abstract class Subscription {
        public SubscriptionToken SubscriptionToken { get; init; }
        internal abstract Task PublishAsync(Event eventItem, CancellationToken token);
    }
    
    internal class Subscription<TEvent>: Subscription where TEvent: Event {
        private readonly Func<TEvent, CancellationToken, Task> _actionAsync;

        public Subscription(Func<TEvent, CancellationToken, Task> actionAsync, SubscriptionToken token): this(token) => 
            _actionAsync = actionAsync;

        internal Subscription(SubscriptionToken token) =>
            SubscriptionToken = token;
        
        internal override async Task PublishAsync(Event eventItem, CancellationToken token) {
            if (eventItem is not TEvent evt)
                throw new ArgumentException("The item is of the wrong type");
            
            await _actionAsync(evt, token);
        }
    }
}