namespace Caracal.PayStation.Withdrawals.Models {
    public record WithdrawalStatusUpdateResult(WithdrawalStatus Status, string Message = "", bool Succeeded = false);
}