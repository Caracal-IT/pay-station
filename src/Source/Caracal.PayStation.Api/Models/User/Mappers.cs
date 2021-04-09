using AutoMapper;
using Caracal.PayStation.Application.UseCases.Infrastructure.LoginUser;

namespace Caracal.PayStation.Api.Models.User {
    public class Mappers: Profile {
        public Mappers() {
            CreateMap<Login, LoginRequest>();
        }
    }
}