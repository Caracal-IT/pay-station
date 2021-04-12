namespace Caracal.PayStation.Web.Gateways.Security.Model.Login {
    public record LoginRequest(string Username, string Password, long TenantId = 207);
}