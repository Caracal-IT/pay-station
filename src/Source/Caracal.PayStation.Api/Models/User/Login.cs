using System.Diagnostics.CodeAnalysis;

namespace Caracal.PayStation.Api.Models.User {
    /// <summary>
    /// The view model for the login request.
    /// </summary>
    /// <param name="TenantId">The identifier for the tenant.</param>
    /// <param name="Username">The username for the login request.</param>
    /// <param name="Password">The password for the login request</param>
    [ExcludeFromCodeCoverage]
    public record Login(long TenantId, string Username, string Password) {
        /// <summary>The identifier for the tenant.</summary>
        public long TenantId { get; init; } = TenantId;

        /// <summary>The username for the login request.</summary>
        public string Username { get; init; } = Username;

        /// <summary>The password for the login request</summary>
        public string Password { get; init; } = Password;
    }
}