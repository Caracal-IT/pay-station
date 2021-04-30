namespace Caracal.PayStation.Application.UseCases.Withdrawals.RequestWithdrawal {
    public record RequestWithdrawalResponse(long Id, string Account, string Amount, string Status);
}