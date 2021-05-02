using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Caracal.Framework.UseCases;
using Caracal.PayStation.Payments;

namespace Caracal.PayStation.Application.UseCases.Withdrawals.RequestWithdrawal {
    public class RequestWithdrawalUseCase: UseCase<RequestWithdrawalResponse, RequestWithdrawalRequest>  {
        private readonly IMapper _mapper;
        private readonly WithdrawalService _service;

        public RequestWithdrawalUseCase(WithdrawalService service) {
            _mapper = Mappings.Create();
            _service = service;
        }
        
        public override async Task<RequestWithdrawalResponse> ExecuteAsync(RequestWithdrawalRequest request, CancellationToken cancellationToken) {
            Request = request;
            var result = await _service.AddWithdrawalAsync(_mapper.Map<Payments.Models.Withdrawal>(request), cancellationToken);
            Response = _mapper.Map<RequestWithdrawalResponse>(result);

            return Response;
        }
    }
}