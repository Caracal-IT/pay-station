using AutoMapper;
using Caracal.PayStation.Web.Gateways.Core.Withdrawals.Model;
using Caracal.PayStation.Web.ViewModel.Withdrawals.Workflow;

namespace Caracal.PayStation.Web.ViewModel.Withdrawals {
    public class Mappers : Profile{
        public Mappers() {
            CreateMap<Withdrawal, WithdrawalViewModel>()
                .ForMember(m => m.IsSelectable, s => s.MapFrom(r => r.WorkflowUrl != null && r.WorkflowUrl.Length > 0))
                .ForMember(m => m.ClientWF, s => s.MapFrom(r => ClientAction(r)));

            CreateMap<WorkflowAction, WorkflowActionViewModel>()
                .ReverseMap();
        }

        private string ClientAction(Withdrawal withdrawal) {
            if(withdrawal.WorkflowUrl?.Length == 0)
                return string.Empty;

            return withdrawal.Status switch {
                "Requested" => "allocate",
                _ => string.Empty
            };
        }
    }
}