using System.Collections.Generic;
using Caracal.EventBus;

namespace Caracal.PayStation.EventBus.Events.Withdrawals {
    public class WithdrawalStatusChangeEvent : Event<IEnumerable<Payments.Models.WithdrawalStatus>> { }
}