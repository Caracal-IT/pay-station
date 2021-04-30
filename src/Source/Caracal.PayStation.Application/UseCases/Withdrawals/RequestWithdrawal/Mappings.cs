using AutoMapper;

namespace Caracal.PayStation.Application.UseCases.Withdrawals.RequestWithdrawal {
    public class Mappings {
        public static IMapper Create() => 
            new MapperConfiguration(CreateMappings).CreateMapper();
        
        private static void CreateMappings(IMapperConfigurationExpression cfg) {
            cfg.CreateMap<RequestWithdrawalRequest, Payments.Models.Withdrawal>();
            cfg.CreateMap<Payments.Models.Withdrawal, RequestWithdrawalResponse>();
        }
    }
}