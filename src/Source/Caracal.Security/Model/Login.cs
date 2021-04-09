using System.Diagnostics.CodeAnalysis;

namespace Caracal.Security.Model {
    [ExcludeFromCodeCoverage]
    public record Login(string Username, string Password, long TenantId);
}