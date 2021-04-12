namespace Caracal.PayStation.Web.ViewModel.Security.Login {
    public record LoginRequestViewModel(string Username, string Password, long TenantId = 207);
}