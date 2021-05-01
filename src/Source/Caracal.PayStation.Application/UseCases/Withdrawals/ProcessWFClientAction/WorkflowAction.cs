namespace Caracal.PayStation.Application.UseCases.Withdrawals.ProcessWFClientAction {
    public class WorkflowAction {
        public long WithdrawalId { get; set; }
        public object? Payload { get; set; }
        public bool Succeeded { get; set; }
    }
}