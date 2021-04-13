using AutoMapper;
using Caracal.Framework.Data;
using Caracal.PayStation.Web.Gateways.Core.Withdrawals.Model;

namespace Caracal.PayStation.Web.ViewModel.Security.Withdrawals.WithdrawalSearch {
    public class Mappers : Profile{
        public Mappers() {
            CreateMap<WithdrawalSearchRequestViewModel, PagedDataFilter>();
            CreateMap<PagedData<Withdrawal>, WithdrawalSearchResponseViewModel>();
        }
    }
}