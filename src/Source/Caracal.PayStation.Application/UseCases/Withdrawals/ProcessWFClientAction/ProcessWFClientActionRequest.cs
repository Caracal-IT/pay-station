using System.Collections.Generic;

namespace Caracal.PayStation.Application.UseCases.Withdrawals.ProcessWFClientAction {
    public class ProcessWFClientActionRequest {
        public List<WorkflowAction> Items { get; set; } = new();
    }
}