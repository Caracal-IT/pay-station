namespace Caracal.PayStation.Web.Model.Security.Login {
    public record LoginRequest(string Username, string Password, long TenantId = 207);
}