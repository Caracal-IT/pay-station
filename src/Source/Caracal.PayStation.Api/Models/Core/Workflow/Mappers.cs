using AutoMapper;

namespace Caracal.PayStation.Api.Models.Core.Workflow {
    public class Mappers: Profile {
        public Mappers() {
            CreateMap<WithdrawalStatus, Application.UseCases.Withdrawals.ChangeStatus.WithdrawalStatus>().ReverseMap();
            CreateMap<Application.UseCases.Withdrawals.ChangeStatus.WithdrawalStatusUpdateResult, WithdrawalStatusUpdateResult>();
        }
    }
}