using System.Threading.Tasks;

namespace Caracal.PayStation.Payments {
    public interface WithdrawalEngine {
        Task RegisterEventListener();
    }
}