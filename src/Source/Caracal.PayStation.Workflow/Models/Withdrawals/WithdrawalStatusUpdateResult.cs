namespace Caracal.PayStation.Workflow.Models.Withdrawals {
    public record WithdrawalStatusUpdateResult(WithdrawalStatus Status, string Message = "", bool Succeeded = false);
}