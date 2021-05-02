using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Caracal.Framework.UseCases;
using Caracal.PayStation.Payments;

namespace Caracal.PayStation.Application.UseCases.Withdrawals.GetWithdrawals {
    public class GetWithdrawalsUseCase: UseCase<GetWithdrawalsResponse, GetWithdrawalsRequest>  {
        private readonly IMapper _mapper;
        private readonly WithdrawalService _service;

        public GetWithdrawalsUseCase(WithdrawalService service) {
            _mapper = Mappings.Create();
            _service = service;
        }
        
        public override async Task<GetWithdrawalsResponse> ExecuteAsync(GetWithdrawalsRequest request, CancellationToken cancellationToken) {
            Request = request;
            var result = await _service.GetWithdrawalsAsync(request, cancellationToken);
            Response = _mapper.Map<GetWithdrawalsResponse>(result);

            return Response;
        }
    }
}