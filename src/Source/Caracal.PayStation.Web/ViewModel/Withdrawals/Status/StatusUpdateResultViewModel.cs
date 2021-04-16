namespace Caracal.PayStation.Web.ViewModel.Withdrawals.Status {
    public record StatusUpdateResultViewModel(StatusViewModel Status, string Message = "", bool Succeeded = false);
}