using System;
using System.Threading;
using System.Threading.Tasks;

namespace Caracal.EventBus {
    public interface EventBus {
        Task<SubscriptionToken> SubscribeAsync<TEvent>(Action<TEvent> action, CancellationToken cancellationToken) where TEvent : Event;
        Task UnsubscribeAsync(SubscriptionToken token, CancellationToken cancellationToken);
        Task PublishAsync<TEvent>(TEvent eventItem, CancellationToken cancellationToken) where TEvent : Event;
    }
}