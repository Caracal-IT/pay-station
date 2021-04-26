namespace Caracal.PayStation.Storage.Simulator.Model.Withdrawals {
    public record WithdrawalStatusUpdateResult(WithdrawalStatus Status, string Message = "", bool Succeeded = false);
}