using AuthService.BusinessLogic.Models;
using AutoMapper;

namespace AuthService.Grpc.Infrastructure.Mappings
{
    /// <summary>
    /// Automapper profile for DateTime
    /// </summary>
    /// <seealso cref="AutoMapper.Profile" />
    public class CreateUserProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DateTimeProfile"/> class.
        /// </summary>
        public CreateUserProfile()
        {
            CreateMap<CreateUserRequest, CreateUserModel>()
                .ReverseMap();
        }
    }
}
