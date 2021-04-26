namespace Caracal.PayStation.Workflow.Models.Withdrawals {
    public record Withdrawal(long Id, string Account, string Amount, string Status);
}