using AutoMapper;
using Caracal.Security.Model;

namespace Caracal.PayStation.Application.UseCases.Infrastructure.LoginUser {
    public static class Mappings {
        public static IMapper Create() => 
            new MapperConfiguration(CreateMappings).CreateMapper();

        private static void CreateMappings(IMapperConfigurationExpression cfg) {
            cfg.CreateMap<LoginRequest, Login>();
            cfg.CreateMap<User, LoginResponse>();
        }
    }
}