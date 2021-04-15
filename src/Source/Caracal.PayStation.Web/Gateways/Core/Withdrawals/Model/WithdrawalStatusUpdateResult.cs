namespace Caracal.PayStation.Web.Gateways.Core.Withdrawals.Model {
    public record WithdrawalStatusUpdateResult(WithdrawalStatus Status, string Message = "", bool Succeeded = false);
}