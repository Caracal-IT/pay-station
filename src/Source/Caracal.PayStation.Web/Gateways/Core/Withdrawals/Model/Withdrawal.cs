namespace Caracal.PayStation.Web.Gateways.Core.Withdrawals.Model {
    public record Withdrawal(long Id, string Account, string Amount, string Status);
}