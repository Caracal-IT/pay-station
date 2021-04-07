using System;

namespace Caracal.EventBus {
    public sealed class SubscriptionToken {
        public Type EventItemType { get; }
        public Guid Key { get; }
        
        internal SubscriptionToken(Type eventItemType) {
            Key = Guid.NewGuid();
            EventItemType = eventItemType;
        }
    }
}