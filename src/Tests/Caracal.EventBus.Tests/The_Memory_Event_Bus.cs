// ReSharper disable InconsistentNaming

using System;
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
        private int _requestedId;
        
        public The_Event_Bus() {
            _cancellationToken = CancellationToken.None;
            _person = new Person(77, "Joe", "Soap");
            _eventBus = new MemoryEventBus();
            _requestedId = -1;
        }
        
        [Fact]
        public async Task Should_Subscribe_And_Publish_Events() {
            var subscription = await _eventBus.SubscribeAsync<PersonRequestedEvent>(OnRequested, _cancellationToken);
            await _eventBus.PublishAsync(new PersonRequestedEvent {Id = _person.Id}, _cancellationToken);
            await _eventBus.UnsubscribeAsync(subscription, _cancellationToken);

            _requestedId.Should().Be(_person.Id);
        }

        [Fact]
        public async Task Should_Not_Listen_To_Unsubscribed_Events() {
            var subscription = await _eventBus.SubscribeAsync<PersonRequestedEvent>(OnRequested, _cancellationToken);
            await _eventBus.UnsubscribeAsync(subscription, _cancellationToken);
            await _eventBus.PublishAsync(new PersonRequestedEvent {Id = _person.Id}, _cancellationToken);
            
            _requestedId.Should().NotBe(_person.Id);
        }
        
        [Fact]
        public async Task Should_Listen_To_Only_Subscribed_Events() {
            var subscription = await _eventBus.SubscribeAsync<PersonRequestedEvent>(OnRequested, _cancellationToken);
            await _eventBus.PublishAsync(new Event<Person> {Data = _person}, _cancellationToken);
            await _eventBus.UnsubscribeAsync(subscription, _cancellationToken);
            
            _requestedId.Should().NotBe(_person.Id);
        }

        [Fact]
        public void Should_Not_Allow_Null_Callbacks_When_Subscribing() {
            _eventBus.Invoking(async eBus => await eBus.SubscribeAsync<PersonRequestedEvent>(null, _cancellationToken))
                     .Should()
                     .Throw<ArgumentException>()
                     .WithMessage("Value cannot be null. (Parameter 'actionAsync')");
        }
        
        [Fact]
        public void Should_Not_Allow_Null_SynchronousCallbacks_When_Subscribing() {
            Action<PersonRequestedEvent> action = null;
            
            // ReSharper disable once ExpressionIsAlwaysNull
            _eventBus.Invoking(async eBus => await eBus.SubscribeAsync<PersonRequestedEvent>(action, _cancellationToken))
                     .Should()
                     .Throw<ArgumentException>()
                     .WithMessage("Value cannot be null. (Parameter 'action')");
        }
        
        [Fact]
        public void Should_Not_Allow_Unsubscription_With_Null_Token() {
            _eventBus.Invoking(async eBus => await eBus.UnsubscribeAsync(null, _cancellationToken))
                     .Should()
                     .Throw<ArgumentException>()
                     .WithMessage("Value cannot be null. (Parameter 'token')");
        }

        [Fact]
        public void Should_Not_Throw_Exception_When_Unsubscribing_With_Old_Token() {
            var subscription = new SubscriptionToken(GetType());
           
            _eventBus.Invoking(async eBus => await eBus.UnsubscribeAsync(subscription, _cancellationToken))
                     .Should()
                     .NotThrow(because: "The token was already canceled");
        }
        
        [Fact]
        public void Should_Not_Allow_To_Publish_Null_Messages() {
            _eventBus.Invoking(async eBus => await eBus.PublishAsync<Event>(null, _cancellationToken))
                .Should()
                .Throw<ArgumentException>()
                .WithMessage("Value cannot be null. (Parameter 'eventItem')");
        }
        
        private void OnRequested(PersonRequestedEvent evt) => _requestedId = evt.Id;
    }
}