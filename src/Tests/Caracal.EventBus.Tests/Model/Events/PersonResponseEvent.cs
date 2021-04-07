namespace Caracal.EventBus.Tests.Model.Events {
    public class PersonResponseEvent: Event {
        public Person Person { get; set; }
    }
}