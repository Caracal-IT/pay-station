using System.Threading;
using System.Threading.Tasks;

namespace Caracal.EventBus {
    public static class EventBusExtensions {
        public static async Task<TReturn> SendAndListenAsync<TReturn, TResponseEvent>(
            this EventBus eventBus, 
            Event requestEvent, 
            CancellationToken cancellationToken, 
            int timeout = 3000) where TResponseEvent: Event<TReturn>
        {
            return await PubSubAdapter<TReturn, TResponseEvent>.SendAndListenAsync(eventBus, requestEvent, cancellationToken, timeout);
        }
    }
}