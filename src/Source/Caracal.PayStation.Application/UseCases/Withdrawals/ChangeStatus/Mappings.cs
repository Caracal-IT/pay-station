using AutoMapper;

namespace Caracal.PayStation.Application.UseCases.Withdrawals.ChangeStatus {
    public class Mappings {
        public static IMapper Create() => 
            new MapperConfiguration(CreateMappings).CreateMapper();

        private static void CreateMappings(IMapperConfigurationExpression cfg) {
            cfg.CreateMap<WithdrawalStatus, Payments.Models.WithdrawalStatus>()
                .ReverseMap();
            
            cfg.CreateMap<Payments.Models.WithdrawalStatusUpdateResult, WithdrawalStatusUpdateResult>();
        }
    }
}