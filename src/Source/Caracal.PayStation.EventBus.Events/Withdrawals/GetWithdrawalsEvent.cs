using Caracal.EventBus;
using Caracal.Framework.Data;
using Caracal.PayStation.PaymentModels.Withdrawals;

namespace Caracal.PayStation.EventBus.Events.Withdrawals {
    public class RequestWithdrawalsEvent: Event<PagedDataFilter> { }
    public class ResponseWithdrawalsEvent: Event<PagedData<Withdrawal>> { }
    
    public class RequestFlushEvent: Event<PagedDataFilter> { }
    public class ResponseFlushEvent: Event<PagedData<Withdrawal>> { }
}