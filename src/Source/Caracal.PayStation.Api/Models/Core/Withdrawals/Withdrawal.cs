namespace Caracal.PayStation.Api.Models.Core.Withdrawals {
    public record Withdrawal(long Id, string Account, string Amount, string Status) {
        public string? WorkflowUrl { get; set; }
    }
}