namespace Caracal.PayStation.PaymentModels.Withdrawals {
    public record Withdrawal(long Id, string Account, string Amount, string Status);
}