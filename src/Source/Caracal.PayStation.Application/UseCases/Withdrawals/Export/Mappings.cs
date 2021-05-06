using AutoMapper;

namespace Caracal.PayStation.Application.UseCases.Withdrawals.Export {
    public class Mappings {
        public static IMapper Create() => 
            new MapperConfiguration(CreateMappings).CreateMapper();

        private static void CreateMappings(IMapperConfigurationExpression cfg) {
            cfg.CreateMap<Payments.Models.Withdrawal, Withdrawal>();
        }
    }
}