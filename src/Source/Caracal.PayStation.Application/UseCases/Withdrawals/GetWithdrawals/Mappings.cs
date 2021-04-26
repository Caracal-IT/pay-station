using AutoMapper;
using Caracal.Framework.Data;

namespace Caracal.PayStation.Application.UseCases.Withdrawals.GetWithdrawals {
    public class Mappings {
        public static IMapper Create() => 
            new MapperConfiguration(CreateMappings).CreateMapper();

        private static void CreateMappings(IMapperConfigurationExpression cfg) {
            cfg.CreateMap<Payments.Models.Withdrawal, Withdrawal>();
            cfg.CreateMap<PagedData<Payments.Models.Withdrawal>, GetWithdrawalsResponse>();
        }
    }
}