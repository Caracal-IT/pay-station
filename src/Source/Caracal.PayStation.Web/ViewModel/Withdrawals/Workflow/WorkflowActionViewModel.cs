namespace Caracal.PayStation.Web.ViewModel.Withdrawals.Workflow {
    public class WorkflowActionViewModel {
        public long WithdrawalId { get; set; }
        public object? Payload { get; set; }
        public bool Succeeded { get; set; }
    }
}