using System.Threading.Tasks;

namespace Caracal.EventBus {
    /*
    public class PubSubAdapter<TReturnValue, TReqEvent, TRespEvent> {
        private TReqEvent _reqEvent;
        
        private PubSubAdapter() {
            
        }
        
        public static async Task<TReturn> SendAndListenAsync<TReturn, TRequestEvent, TResponseEvent>(TRequestEvent requestEvent) 
            where TRequestEvent: Event 
            where TResponseEvent: Event {

            var adapter = new PubSubAdapter<TReturn, TRequestEvent, TResponseEvent>();
            return await adapter.SendAndListenAsync();

            // await _eventBus.PublishAsync(new PersonRequestedEvent{ Id = _personId}, _cancellationToken);
        }

        private Task<TReturnValue> SendAndListenAsync() {
            return Task.FromResult<TReturnValue>(default); 
        }
    }*/
}