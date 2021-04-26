using System.Collections.Generic;
using Caracal.EventBus;
using Caracal.PayStation.Workflow.Models.Withdrawals;

namespace Caracal.PayStation.EventBus.Events.Withdrawals.Workflow.WithdrawalStatusChange {
    public class WithdrawalStatusChangedEvent: Event<IEnumerable<WithdrawalStatusUpdateResult>> { }
}