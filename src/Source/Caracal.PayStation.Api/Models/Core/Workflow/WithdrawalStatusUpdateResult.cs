namespace Caracal.PayStation.Api.Models.Core.Workflow {
    public record WithdrawalStatusUpdateResult(WithdrawalStatus Status, string Message = "", bool Succeeded = false);
}