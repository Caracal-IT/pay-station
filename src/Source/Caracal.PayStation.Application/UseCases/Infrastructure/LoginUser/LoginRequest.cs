using System.Diagnostics.CodeAnalysis;

namespace Caracal.PayStation.Application.UseCases.Infrastructure.LoginUser {
    [ExcludeFromCodeCoverage]
    public record LoginRequest(string Username, string Password, long TenantId);
}