using System.Collections.Generic;

namespace Caracal.PayStation.Application.UseCases.Withdrawals.ProcessWFClientAction {
    public class ProcessWFClientActionResponse {
        public List<WorkflowAction> Items { get; set; } = new();
    }
}