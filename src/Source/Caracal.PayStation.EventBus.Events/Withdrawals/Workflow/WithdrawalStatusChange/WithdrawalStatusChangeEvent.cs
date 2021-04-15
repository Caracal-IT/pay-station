using System.Collections.Generic;
using Caracal.EventBus;
using Caracal.PayStation.PaymentModels.Withdrawals;

namespace Caracal.PayStation.EventBus.Events.Withdrawals.Workflow.WithdrawalStatusChange {
    public class WithdrawalStatusChangeEvent : Event<IEnumerable<WithdrawalStatus>> { }
}