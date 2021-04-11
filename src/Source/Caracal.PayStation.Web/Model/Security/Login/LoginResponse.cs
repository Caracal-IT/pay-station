namespace Caracal.PayStation.Web.Model.Security.Login {
    public record LoginResponse;
    public record FailedLoggedInResponse(string Message = "Login failed!!") : LoginResponse;
    public record SuccessfulLoggedInResponse(string Message) : LoginResponse;
}