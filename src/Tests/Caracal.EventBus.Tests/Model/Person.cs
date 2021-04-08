using System.Diagnostics.CodeAnalysis;

namespace Caracal.EventBus.Tests.Model {
    [ExcludeFromCodeCoverage]
    public record Person (int Id, string FirstName, string LastName);
}