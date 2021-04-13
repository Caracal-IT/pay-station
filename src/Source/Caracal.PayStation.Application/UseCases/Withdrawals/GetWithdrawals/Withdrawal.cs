namespace Caracal.PayStation.Application.UseCases.Withdrawals.GetWithdrawals {
    public record Withdrawal(long Id, string Account, string Amount, string Status);
}