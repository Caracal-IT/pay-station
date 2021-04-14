using AutoMapper;
using Caracal.Framework.Data;

using GetWithdrawals = Caracal.PayStation.Application.UseCases.Withdrawals.GetWithdrawals;
using FlushWithdrawals = Caracal.PayStation.Application.UseCases.Withdrawals.FlushWithdrawals;

namespace Caracal.PayStation.Api.Models.Core.Withdrawals {
    public class Mappers: Profile {
        public Mappers() {
            CreateMap<PagedDataFilter, GetWithdrawals.GetWithdrawalsRequest>();
            CreateMap<PagedDataFilter, FlushWithdrawals.FlushWithdrawalsRequest>();
            CreateMap<GetWithdrawals.Withdrawal, Withdrawal>();
            CreateMap<FlushWithdrawals.Withdrawal, Withdrawal>();
            
            CreateMap<GetWithdrawals.GetWithdrawalsResponse, PagedData<Withdrawal>>();
            CreateMap<FlushWithdrawals.FlushWithdrawalsResponse, PagedData<Withdrawal>>();
        }
    }
}