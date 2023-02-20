using AuthService.BusinessLogic.Models;
using AutoMapper;

namespace AuthService.Grpc.Infrastructure.Mappings
{
    /// <summary>
    /// Automapper profile for User
    /// </summary>
    /// <seealso cref="AutoMapper.Profile" />
    public class UserProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserProfile"/> class.
        /// </summary>
        public UserProfile()
        {
            CreateMap<UserSimpleModel, User>();
        }
    }
}
