using System.Threading.Tasks;

namespace Caracal.PayStation.Withdrawals {
    public interface WithdrawalEngine {
        Task RegisterEventListener();
    }
}