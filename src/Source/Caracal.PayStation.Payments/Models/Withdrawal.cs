using System;

namespace Caracal.PayStation.Payments.Models {
    public record Withdrawal(long Id, string Account, string Amount, string Status, string WorkflowUrl) {
        public Withdrawal() : this(0, string.Empty, string.Empty, string.Empty, String.Empty) {}
    }
}