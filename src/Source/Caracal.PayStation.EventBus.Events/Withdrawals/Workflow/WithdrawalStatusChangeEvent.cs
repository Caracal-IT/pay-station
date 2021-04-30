using System.Collections.Generic;
using Caracal.EventBus;
using Caracal.PayStation.Workflow.Models.Withdrawals;

namespace Caracal.PayStation.EventBus.Events.Withdrawals.Workflow {
    public class WithdrawalStatusChangeEvent : Event<IEnumerable<WithdrawalStatus>> { }
}