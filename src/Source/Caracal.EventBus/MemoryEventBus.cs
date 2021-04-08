using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Caracal.EventBus {
    public sealed class MemoryEventBus: EventBus {
        private readonly object _subscriptionLock = new();
        private readonly Dictionary<Type, IList<Subscription>> _subscriptions = new();
        
        public Task<SubscriptionToken> SubscribeAsync<TEvent>(Action<TEvent> action, CancellationToken cancellationToken) where TEvent: Event {
            if (action == null) throw new ArgumentNullException(nameof(action));

            lock (_subscriptionLock) {
                if (!_subscriptions.ContainsKey(typeof(TEvent)))
                    _subscriptions.Add(typeof(TEvent), new List<Subscription>());

                var token = new SubscriptionToken(typeof(TEvent));
                _subscriptions[typeof(TEvent)].Add(new SynchronousSubscription<TEvent>(action, token));

                return Task.FromResult(token);
            }
        }

        public Task<SubscriptionToken> SubscribeAsync<TEvent>(Func<TEvent, CancellationToken, Task> actionAsync, CancellationToken cancellationToken) 
            where TEvent: Event 
        {
            if (actionAsync == null) throw new ArgumentNullException(nameof(actionAsync));

            lock (_subscriptionLock) {
                if (!_subscriptions.ContainsKey(typeof(TEvent)))
                    _subscriptions.Add(typeof(TEvent), new List<Subscription>());

                var token = new SubscriptionToken(typeof(TEvent));
                _subscriptions[typeof(TEvent)].Add(new Subscription<TEvent>(actionAsync, token));

                return Task.FromResult(token);
            }
        }

        public Task UnsubscribeAsync(SubscriptionToken token, CancellationToken cancellationToken) {
            if (token == null) throw new ArgumentNullException(nameof(token));

            lock (_subscriptionLock) {
                if (!_subscriptions.ContainsKey(token.EventItemType)) return Task.CompletedTask;

                var subscriptionsToRemove = _subscriptions[token.EventItemType].FirstOrDefault(e => e.SubscriptionToken.Key == token.Key);

                if (subscriptionsToRemove != null)
                    _subscriptions[token.EventItemType].Remove(subscriptionsToRemove);
            }

            return Task.CompletedTask;
        }

        public async Task PublishAsync<TEvent>(TEvent eventItem, CancellationToken cancellationToken) where TEvent: Event {
            if (eventItem == null) throw new ArgumentNullException(nameof(eventItem));

            IList<Subscription> subscriptions = new List<Subscription>();

            lock (_subscriptionLock) {
                if (_subscriptions.ContainsKey(typeof(TEvent)))
                    subscriptions = _subscriptions[typeof(TEvent)];
                else if (_subscriptions.ContainsKey(eventItem.GetType()))
                    subscriptions = _subscriptions[eventItem.GetType()];
            }

            foreach (var subscription in subscriptions)
                await subscription.PublishAsync(eventItem, cancellationToken);
        }
    }
}