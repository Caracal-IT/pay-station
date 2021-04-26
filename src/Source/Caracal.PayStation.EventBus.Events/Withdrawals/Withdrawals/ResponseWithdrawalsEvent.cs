using Caracal.EventBus;
using Caracal.Framework.Data;
using Caracal.PayStation.Withdrawals.Models;

namespace Caracal.PayStation.EventBus.Events.Withdrawals.Withdrawals {
    public class ResponseWithdrawalsEvent: Event<PagedData<Withdrawal>> { }
}