using AutoMapper;
using Caracal.Framework.Data;

using GetWithdrawals = Caracal.PayStation.Application.UseCases.Withdrawals.GetWithdrawals;

namespace Caracal.PayStation.Api.Models.Core.Withdrawals {
    public class Mappers: Profile {
        public Mappers() {
            CreateMap<PagedDataFilter, GetWithdrawals.GetWithdrawalsRequest>();
            CreateMap<GetWithdrawals.Withdrawal, Withdrawal>();
            CreateMap<GetWithdrawals.GetWithdrawalsResponse, PagedData<Withdrawal>>();
            
            
        }
    }
}