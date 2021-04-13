using AutoMapper;

namespace Caracal.PayStation.Application.UseCases.Withdrawals.GetWithdrawals {
    public class Mappings {
        public static IMapper Create() => 
            new MapperConfiguration(CreateMappings).CreateMapper();

        private static void CreateMappings(IMapperConfigurationExpression cfg) {
            
        }
    }
}