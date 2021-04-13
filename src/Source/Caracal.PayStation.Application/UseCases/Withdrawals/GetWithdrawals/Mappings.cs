using AutoMapper;
using Model = Caracal.PayStation.PaymentModels.Withdrawals;

namespace Caracal.PayStation.Application.UseCases.Withdrawals.GetWithdrawals {
    public class Mappings {
        public static IMapper Create() => 
            new MapperConfiguration(CreateMappings).CreateMapper();

        private static void CreateMappings(IMapperConfigurationExpression cfg) {
            cfg.CreateMap<Model.Withdrawal, Withdrawal>();
        }
    }
}