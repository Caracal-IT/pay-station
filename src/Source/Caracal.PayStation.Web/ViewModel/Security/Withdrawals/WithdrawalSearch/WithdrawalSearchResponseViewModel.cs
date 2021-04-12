using System.Collections.Generic;

namespace Caracal.PayStation.Web.ViewModel.Security.Withdrawals.WithdrawalSearch {
    public class WithdrawalSearchResponseViewModel {
        public List<WithdrawalViewModel> Withdrawals { get; set; } = new ();
    }
}