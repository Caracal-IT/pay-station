using System;

namespace Caracal.EventBus {
    public class Event {
        public Guid CorrelationId { get; set; }

        public Event() : this(Guid.NewGuid()){}

        public Event(Guid correlationId) => CorrelationId = correlationId;
    }
    
    public class Event<TData>: Event {
        public Event(){ }

        public Event(Guid correlationId): base(correlationId) {}
        
        public TData Data { get; set; } 
    }
}