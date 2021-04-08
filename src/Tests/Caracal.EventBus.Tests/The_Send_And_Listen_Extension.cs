// ReSharper disable InconsistentNaming

using System;
using System.Threading;
using System.Threading.Tasks;
using Caracal.EventBus.Tests.Model;
using Caracal.EventBus.Tests.Model.Events;
using FluentAssertions;
using Xunit;

namespace Caracal.EventBus.Tests {
    public class The_Send_And_Listen_Extension {
        private readonly int _personId;
        private readonly Person _person;
        private readonly EventBus _eventBus;
        private readonly CancellationToken _cancellationToken;
        
        public The_Send_And_Listen_Extension() {
            _cancellationToken = CancellationToken.None;
            _personId = 77;
            _person = new Person(77, "Joe", "Soap");
            _eventBus = new MemoryEventBus();
        }
        
        [Fact]
        public async Task Should_Send_And_Listen_To_Events() {
            var reqSubToken = await _eventBus.SubscribeAsync<PersonRequestedEvent>(OnRequested, _cancellationToken);
            var request = new PersonRequestedEvent{ Id = _personId };
            var result = await _eventBus.SendAndListenAsync<Person, Event<Person>>(request, _cancellationToken);
            
            await _eventBus.UnsubscribeAsync(reqSubToken, _cancellationToken);

            result.Id.Should().Be(_personId);
        }

        [Fact]
        public void Should_Throw_Exception_On_Timeout() {
            const int _timeout = 10;
            var request = new PersonRequestedEvent{ Id = _personId };
            
            _eventBus.Invoking(async eBus => await eBus.SendAndListenAsync<Person, Event<Person>>(request, _cancellationToken, _timeout))
                     .Should()
                     .Throw<TimeoutException>()
                     .WithMessage("The operation has timed out.");
        }

        private async Task OnRequested(PersonRequestedEvent request, CancellationToken token) {
            if(request.Id != _personId) return;
            
            await _eventBus.PublishAsync(new Event<Person>(request.CorrelationId) {Data = _person}, _cancellationToken);
        }
    }
}