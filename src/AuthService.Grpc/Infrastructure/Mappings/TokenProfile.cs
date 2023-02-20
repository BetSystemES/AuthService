using AutoMapper;
using BusinessToken = AuthService.BusinessLogic.Models.Token;

namespace AuthService.Grpc.Infrastructure.Mappings
{
    /// <summary>
    /// Automapper profile for Token
    /// </summary>
    /// <seealso cref="AutoMapper.Profile" />
    public class TokenProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TokenProfile"/> class.
        /// </summary>
        public TokenProfile()
        {
            CreateMap<BusinessToken, Token>()
                .ReverseMap();
        }
    }
}
