namespace Caracal.PayStation.Web.Model.Security.Login {
    public record LoginResponse;
    public record FailedLoggedInResponse(string Message = "Login failed!!", bool Succeeded = false) : LoginResponse;
    public record SuccessfulLoggedInResponse(string Message, bool Succeeded = true) : LoginResponse;
}