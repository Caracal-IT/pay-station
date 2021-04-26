using System.Threading.Tasks;

namespace Caracal.PayStation.Workflow {
    public interface ChangeStateEngine {
        Task RegisterEventListener();
    }
}