namespace Caracal.PayStation.Application.UseCases.Withdrawals.FlushWithdrawals {
    public record Withdrawal(long Id, string Account, string Amount, string Status);
}