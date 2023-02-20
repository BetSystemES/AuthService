using AutoMapper;
using Google.Protobuf.WellKnownTypes;

namespace AuthService.Grpc.Infrastructure.Mappings
{
    /// <summary>
    /// Automapper profile for DateTime
    /// </summary>
    /// <seealso cref="AutoMapper.Profile" />
    public class DateTimeProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DateTimeProfile"/> class.
        /// </summary>
        public DateTimeProfile()
        {
            CreateMap<DateTime, Timestamp>()
                .ConvertUsing(x => x.ToTimestamp());
        }
    }
}
