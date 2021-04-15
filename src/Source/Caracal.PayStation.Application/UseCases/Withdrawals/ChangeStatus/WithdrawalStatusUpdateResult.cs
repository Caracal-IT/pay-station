namespace Caracal.PayStation.Application.UseCases.Withdrawals.ChangeStatus {
    public record WithdrawalStatusUpdateResult(WithdrawalStatus Status, string Message = "", bool Succeeded = false);
}