using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Caracal.Framework.UseCases;
using Caracal.PayStation.Payments.Repositories;

namespace Caracal.PayStation.Application.UseCases.Withdrawals.GetWithdrawals {
    public class GetWithdrawalsUseCase: UseCase<GetWithdrawalsResponse, GetWithdrawalsRequest>  {
        private readonly IMapper _mapper;
        private readonly WithdrawalsRepository _repository;

        public GetWithdrawalsUseCase(WithdrawalsRepository repository) {
            _mapper = Mappings.Create();
            _repository = repository;
        }
        
        public override async Task<GetWithdrawalsResponse> ExecuteAsync(GetWithdrawalsRequest request, CancellationToken cancellationToken) {
            Request = request;
            var result = await _repository.GetWithdrawalsAsync(request, cancellationToken);
            Response = _mapper.Map<GetWithdrawalsResponse>(result);
            
            return Response;
        }
    }
}