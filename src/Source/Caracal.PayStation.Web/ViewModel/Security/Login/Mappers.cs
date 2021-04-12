using AutoMapper;
using Caracal.PayStation.Web.Gateways.Security.Model.Login;

namespace Caracal.PayStation.Web.ViewModel.Security.Login {
    public class Mappers : Profile{
        public Mappers() {
            CreateMap<LoginRequestViewModel, LoginRequest>();
        }
    }
}