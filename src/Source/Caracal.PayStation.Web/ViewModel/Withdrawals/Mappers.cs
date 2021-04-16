using AutoMapper;
using Caracal.PayStation.Web.Gateways.Core.Withdrawals.Model;

namespace Caracal.PayStation.Web.ViewModel.Withdrawals {
    public class Mappers : Profile{
        public Mappers() {
            CreateMap<Withdrawal, WithdrawalViewModel>();
        }
    }
}