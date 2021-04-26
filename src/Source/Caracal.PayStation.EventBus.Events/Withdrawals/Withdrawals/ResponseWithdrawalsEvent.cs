using Caracal.EventBus;
using Caracal.Framework.Data;
using Caracal.PayStation.Payments.Models;

namespace Caracal.PayStation.EventBus.Events.Withdrawals.Withdrawals {
    public class ResponseWithdrawalsEvent: Event<PagedData<Withdrawal>> { }
}