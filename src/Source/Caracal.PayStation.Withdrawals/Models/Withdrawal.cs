namespace Caracal.PayStation.Withdrawals.Models {
    public record Withdrawal(long Id, string Account, string Amount, string Status);
}