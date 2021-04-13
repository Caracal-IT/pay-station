using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Caracal.Framework.UseCases;

namespace Caracal.PayStation.Application.UseCases.Withdrawals.GetWithdrawals {
    public class GetWithdrawalsUseCase: UseCase<GetWithdrawalsResponse, GetWithdrawalsRequest>  {
        private readonly IMapper _mapper;

        public GetWithdrawalsUseCase() {
            _mapper = Mappings.Create();
        }

        public override Task Execute() {
            Response = new GetWithdrawalsResponse {PageNumber = 1, NumberOfResults = 5, NumberOfRows = 5};

            foreach (var i in Enumerable.Range(1, 5))
                Response.Items.Add(new Withdrawal(i, $"account {i}", $"R {i}0.44", "Requested"));

            return Task.CompletedTask;
        }
    }
}