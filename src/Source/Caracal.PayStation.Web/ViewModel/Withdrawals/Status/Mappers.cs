using AutoMapper;
using Caracal.PayStation.Web.Gateways.Core.Withdrawals.Model;

namespace Caracal.PayStation.Web.ViewModel.Withdrawals.Status {
    public class Mappers : Profile{
        public Mappers() {
            CreateMap<StatusViewModel, WithdrawalStatus>().ReverseMap();
            CreateMap<WithdrawalStatusUpdateResult, StatusUpdateResultViewModel>();
        }
    }
}