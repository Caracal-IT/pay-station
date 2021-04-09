namespace Caracal.PayStation.Api.Models.User {
    /// <summary>
    /// The Context after the login.
    /// </summary>
    /// <param name="UserId">The user identifier.</param>
    /// <param name="Token">The JWT token for authorized method calls.</param>
    public record UserContext(long UserId, string Token) {
       
    }
}