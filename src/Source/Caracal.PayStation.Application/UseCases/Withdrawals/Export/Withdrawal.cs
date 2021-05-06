namespace Caracal.PayStation.Application.UseCases.Withdrawals.Export {
    public record Withdrawal(long Id, string Account, string Amount, string Status);
}