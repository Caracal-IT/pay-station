namespace Caracal.EventBus {
    public abstract class Subscription {
        public SubscriptionToken SubscriptionToken { get; init; }
        public abstract void Publish(Event messageEvent);
    }
}