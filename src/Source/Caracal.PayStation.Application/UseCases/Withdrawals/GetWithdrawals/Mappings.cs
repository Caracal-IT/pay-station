using AutoMapper;
using Caracal.Framework.Data;
using Model = Caracal.PayStation.Withdrawals.Models;

namespace Caracal.PayStation.Application.UseCases.Withdrawals.GetWithdrawals {
    public class Mappings {
        public static IMapper Create() => 
            new MapperConfiguration(CreateMappings).CreateMapper();

        private static void CreateMappings(IMapperConfigurationExpression cfg) {
            cfg.CreateMap<Model.Withdrawal, Withdrawal>();
            cfg.CreateMap<PagedData<Model.Withdrawal>, GetWithdrawalsResponse>();
        }
    }
}