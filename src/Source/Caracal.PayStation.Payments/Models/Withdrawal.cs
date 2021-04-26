namespace Caracal.PayStation.Payments.Models {
    public record Withdrawal(long Id, string Account, string Amount, string Status);
}