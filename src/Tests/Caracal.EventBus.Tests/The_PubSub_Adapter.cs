// ReSharper disable InconsistentNaming
/*
using System.Threading;
using System.Threading.Tasks;
using Caracal.EventBus.Tests.Model;
using Caracal.EventBus.Tests.Model.Events;
using FluentAssertions;
using Xunit;

namespace Caracal.EventBus.Tests {
    public class The_PubSub_Adapter {
        private readonly int _personId;
        private readonly Person _person;
        private readonly EventBus _eventBus;
        private readonly CancellationToken _cancellationToken;
        
        private Person _testPerson;
        
        public The_PubSub_Adapter() {
            _cancellationToken = CancellationToken.None;
            _personId = 77;
            _person = new Person(77, "Joe", "Soap");
            _eventBus = new MemoryEventBus();
        }
        
        [Fact]
        public async Task Should_Send_And_Listen_To_Events() {
            var reqSubToken = await _eventBus.SubscribeAsync<PersonRequestedEvent>(OnRequested, _cancellationToken);
            var respSubToken = await _eventBus.SubscribeAsync<PersonResponseEvent>(OnResponded, _cancellationToken);

            await _eventBus.PublishAsync(new PersonRequestedEvent{ Id = _personId}, _cancellationToken);
            
            await _eventBus.UnsubscribeAsync(reqSubToken, _cancellationToken);
            await _eventBus.UnsubscribeAsync(respSubToken, _cancellationToken);

            var a = await PubSubAdapter<Person, PersonRequestedEvent, PersonResponseEvent>
                .SendAndListenAsync<Person, PersonRequestedEvent, PersonResponseEvent>(new PersonRequestedEvent{ Id = _personId});
            
            _testPerson.Id.Should().Be(_personId);
        }

        private async Task OnRequested(PersonRequestedEvent request, CancellationToken token) {
            if (request.Id != _personId) return;
            await _eventBus.PublishAsync(new PersonResponseEvent {Person = _person}, _cancellationToken);
        }

        private void OnResponded(PersonResponseEvent response) {
            _testPerson = response.Person;
        }
    }
}
*/