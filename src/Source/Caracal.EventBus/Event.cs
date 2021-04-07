namespace Caracal.EventBus {
    public class Event { }
    
    public class Event<TData>: Event {
        public TData Data { get; set; } 
    }
}