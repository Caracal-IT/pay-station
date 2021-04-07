using System;

namespace Caracal.EventBus {
    internal class EventSubscription<TEvent>: Subscription where TEvent : Event {
        private readonly Action<TEvent> _action;

        public EventSubscription(Action<TEvent> action, SubscriptionToken token) {
            _action = action;
            SubscriptionToken = token;
        }

        public override void Publish(Event eventItem) {
            if (eventItem is not TEvent evt)
                throw new ArgumentException("The item is of the wrong type");
            
            _action(evt);
        }
    }
}