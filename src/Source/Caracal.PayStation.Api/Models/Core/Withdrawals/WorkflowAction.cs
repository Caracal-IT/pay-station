namespace Caracal.PayStation.Api.Models.Core.Withdrawals {
    public class WorkflowAction {
        public long WithdrawalId { get; set; }
        public object? Payload { get; set; }
        public bool Succeeded { get; set; }
    }
}