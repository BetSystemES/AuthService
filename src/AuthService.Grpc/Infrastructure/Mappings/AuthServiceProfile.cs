using AuthService.BusinessLogic.Models;
using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using BusinessModels = AuthService.BusinessLogic.Models;

namespace AuthService.Grpc.Infrastructure.Mappings
{
    /// <summary>Profile of grpc layer</summary>
    public class AuthServiceProfile : Profile
    {
        public AuthServiceProfile()
        {
            CreateMap<string, Guid>()
                .ConvertUsing((x, res) => res = Guid.TryParse(x, out var id) ? id : Guid.Empty);
            CreateMap<Guid?, string>()
                .ConvertUsing((x, res) => res = x?.ToString() ?? string.Empty);

            CreateMap<DateTime, Timestamp>()
                .ConvertUsing(x => Timestamp.FromDateTime(x));
            CreateMap<Timestamp, DateTime>()
                .ConvertUsing(x => x.ToDateTime());

            CreateMap<BusinessModels.Token, Token>()
                .ReverseMap();
            // TODO: fix this (AuthService.BusinessLogic.Entities.User is not available)
            CreateMap<AuthService.BusinessLogic.Entities.User, User>()
                .ReverseMap();
            CreateMap<CreateUserRequest, CreateUserModel>()
                .ReverseMap();
            CreateMap<UserSimpleModel, User>()
                .ReverseMap();
        }
    }
}
