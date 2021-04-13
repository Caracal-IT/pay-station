namespace Caracal.PayStation.Web.ViewModel.Security.Withdrawals {
    public class WithdrawalViewModel {
        public WithdrawalViewModel(long id, string account, string amount, string status) {
            Id = id;
            Account = account;
            Amount = amount;
            Status = status;
        }
        public long Id { get; set; }
        public string Account { get; set; }
        public string Amount { get; set; }
        public string Status { get; set; }
    }
}