// ReSharper disable InconsistentNaming

using System.Threading;
using System.Threading.Tasks;
using Caracal.EventBus.Tests.Model;
using Caracal.EventBus.Tests.Model.Events;
using FluentAssertions;
using Xunit;

namespace Caracal.EventBus.Tests {
    public class The_Event_Bus {
        private readonly Person _person;
        private readonly EventBus _eventBus;
        private readonly CancellationToken _cancellationToken;
        
        public The_Event_Bus() {
            _cancellationToken = CancellationToken.None;
            _person = new Person(77, "Joe", "Soap");
            _eventBus = new MemoryEventBus();
        }
        
        [Fact]
        public async Task Should_Subscribe_And_Publish_Events() {
            var id = -1;
            var subscription = await _eventBus.SubscribeAsync<PersonRequestedEvent>(OnRequested, _cancellationToken);
            await _eventBus.PublishAsync(new PersonRequestedEvent {Id = _person.Id}, _cancellationToken);
            await _eventBus.UnsubscribeAsync(subscription, _cancellationToken);

            id.Should().Be(_person.Id);

           void OnRequested(PersonRequestedEvent evt) => id = evt.Id;
        }

        [Fact]
        public async Task Should_Not_Listen_To_Unsubscribed_Events() {
            var id = -1;
            var subscription = await _eventBus.SubscribeAsync<PersonRequestedEvent>(OnRequested, _cancellationToken);
            await _eventBus.UnsubscribeAsync(subscription, _cancellationToken);
            await _eventBus.PublishAsync(new PersonRequestedEvent {Id = _person.Id}, _cancellationToken);
            
            id.Should().NotBe(_person.Id);

            void OnRequested(PersonRequestedEvent evt) => id = evt.Id;
        }
        
        [Fact]
        public async Task Should_Listen_To_Only_Subscribed_Events() {
            var id = -1;
            var subscription = await _eventBus.SubscribeAsync<PersonRequestedEvent>(OnRequested, _cancellationToken);
            await _eventBus.PublishAsync(new PersonResponseEvent {Data = _person}, _cancellationToken);
            await _eventBus.UnsubscribeAsync(subscription, _cancellationToken);
            
            id.Should().NotBe(_person.Id);

            void OnRequested(PersonRequestedEvent evt) => id = evt.Id;
        }
    }
}