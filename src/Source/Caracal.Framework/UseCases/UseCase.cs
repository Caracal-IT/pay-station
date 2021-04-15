using System.Threading;
using System.Threading.Tasks;

namespace Caracal.Framework.UseCases {
    public abstract class UseCase {
        public virtual Task Execute() => Task.CompletedTask;
    }

    public abstract class UseCase<TResponse, TRequest> : UseCase {
        public TRequest Request { get; set; }
        public TResponse Response { get; set; }
        
        public virtual async Task<TResponse> ExecuteAsync(TRequest request, CancellationToken cancellationToken) {
            Request = request;
            await Execute();
            return Response;
        }
    }
}