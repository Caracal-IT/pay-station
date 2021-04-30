namespace Caracal.PayStation.Application.UseCases.Withdrawals.UpdateClientEvent {
    public class UpdateClientEventRequest {
        public long WithdrawalId { get; set; }
        public string WorkflowUrl { get; set; }
    }
}