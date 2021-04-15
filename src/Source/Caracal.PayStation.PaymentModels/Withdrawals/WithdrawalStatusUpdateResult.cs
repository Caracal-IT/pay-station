namespace Caracal.PayStation.PaymentModels.Withdrawals {
    public record WithdrawalStatusUpdateResult(WithdrawalStatus Status, string Message = "", bool Succeeded = false);
}