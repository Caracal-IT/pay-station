using AutoMapper;
using Model = Caracal.PayStation.Workflow.Models.Withdrawals;

namespace Caracal.PayStation.Application.UseCases.Withdrawals.ChangeStatus {
    public class Mappings {
        public static IMapper Create() => 
            new MapperConfiguration(CreateMappings).CreateMapper();

        private static void CreateMappings(IMapperConfigurationExpression cfg) {
            cfg.CreateMap<WithdrawalStatus, Model.WithdrawalStatus>()
                .ReverseMap();
            
            cfg.CreateMap<Model.WithdrawalStatusUpdateResult, WithdrawalStatusUpdateResult>();
        }
    }
}