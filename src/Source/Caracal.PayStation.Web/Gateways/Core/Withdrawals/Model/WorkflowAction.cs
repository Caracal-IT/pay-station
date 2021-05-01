namespace Caracal.PayStation.Web.Gateways.Core.Withdrawals.Model {
    public class WorkflowAction {
        public long WithdrawalId { get; set; }
        public object? Payload { get; set; }
        public bool Succeeded { get; set; }
    }
}