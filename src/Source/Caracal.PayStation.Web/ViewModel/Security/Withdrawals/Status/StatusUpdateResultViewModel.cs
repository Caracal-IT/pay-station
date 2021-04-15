namespace Caracal.PayStation.Web.ViewModel.Security.Withdrawals.Status {
    public record StatusUpdateResultViewModel(StatusViewModel Status, string Message = "", bool Succeeded = false);
}