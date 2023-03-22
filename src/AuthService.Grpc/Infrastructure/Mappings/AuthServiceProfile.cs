using AuthService.BusinessLogic.Models;
using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using BusinessEntities = AuthService.BusinessLogic.Entities;

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

            CreateMap<BusinessEntities.User, User>()
                .ForMember(dest => dest.IsLocked,
                    opt =>
                        opt.MapFrom(src => src.LockoutEnabled));

            CreateMap<User, BusinessEntities.User>()
                .ForMember(dest => dest.LockoutEnabled,
                    opt =>
                        opt.MapFrom(src => src.IsLocked))
                .ForMember(x => x.PasswordHash, opt => opt.Ignore())
                .ForMember(x => x.LockoutEndAtUtc, opt => opt.Ignore())
                .ForMember(x => x.AccessFailedCount, opt => opt.Ignore())
                .ForMember(x => x.CreatedAtUtc, opt => opt.Ignore())
                .ForMember(x => x.UpdatedAtUtc, opt => opt.Ignore())
                .ForMember(x => x.UserRole, opt => opt.Ignore())
                .ForMember(x => x.RefreshTokens, opt => opt.Ignore());

            CreateMap<CreateUserRequest, CreateUserModel>()
                .ReverseMap();
            CreateMap<UserSimpleModel, User>()
                .ReverseMap();

            CreateMap<BusinessEntities.Role, Role>()
                .ReverseMap();
        }
    }
}
