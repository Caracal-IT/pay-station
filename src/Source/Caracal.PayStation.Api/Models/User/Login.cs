using System.Diagnostics.CodeAnalysis;

namespace Caracal.PayStation.Api.Models.User {
    [ExcludeFromCodeCoverage]
    public record Login(long TenantId, string Username, string Password);
}