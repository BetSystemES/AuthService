using AuthService.Grpc.Interceptors.cov;
using Newtonsoft.Json;

namespace AuthService.Grpc.Interceptors.Helpers
{
    public class StatusMessage
    {
        public bool IsSuccessful { get; set; }
        public string? Reason { get; set; }

        [JsonProperty(ItemConverterType = typeof(BaseExceptionDetailConverter))]
        public IEnumerable<IBaseExceptionDetails> Details { get; set; }
    }
}
