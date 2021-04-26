namespace Caracal.PayStation.Storage.Simulator.Model.Withdrawals {
    public record Withdrawal(long Id, string Account, string Amount, string Status);
}