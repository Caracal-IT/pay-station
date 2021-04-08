using System;
using System.Threading;
using System.Threading.Tasks;

namespace Caracal.EventBus {
    internal class PubSubAdapter<TReturn, TResponseEvent> where TResponseEvent : Event<TReturn> {
        private readonly int _timeout;
        
        private readonly EventBus _eventBus;
        private readonly Event _requestEvent;
        private readonly CancellationToken _cancellationToken;

        private PubSubAdapter(EventBus eventBus, Event requestEvent, CancellationToken cancellationToken, int timeout) {
            _eventBus = eventBus;
            _requestEvent = requestEvent;
            _cancellationToken = cancellationToken;
            _timeout = timeout;
        }

        public static Task<TReturn> SendAndListenAsync(EventBus eventBus, Event requestEvent, CancellationToken cancellationToken, int timeout = 3000) {
            var adapter = new PubSubAdapter<TReturn, TResponseEvent>(eventBus, requestEvent, cancellationToken, timeout);
            return adapter.SendAndListenAsync();
        }

        private Task<TReturn> SendAndListenAsync() =>
            Task.Run(CreateTask, _cancellationToken);

        private async Task<TReturn> CreateTask() {
            var t = new TaskCompletionSource<TReturn>();

            var token = await _eventBus.SubscribeAsync<TResponseEvent>(evt => {
                if (evt.CorrelationId == _requestEvent.CorrelationId)
                    t.SetResult(evt.Data);
            }, CancellationToken.None);

            await _eventBus.PublishAsync(_requestEvent, _cancellationToken);
            await _eventBus.UnsubscribeAsync(token, _cancellationToken);

            var completedTask = await Task.WhenAny(t.Task, Task.Delay(_timeout, _cancellationToken));
            if (completedTask == t.Task)
                return t.Task.Result;

            throw new TimeoutException();
        }
    }
}