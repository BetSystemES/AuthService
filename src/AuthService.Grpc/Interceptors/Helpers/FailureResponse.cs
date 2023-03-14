
namespace AuthService.Grpc.Interceptors.Helpers
{
    public class FailureResponse : BasicApiResponse
    {
        public FailureResponse()
        {
            
        }
        public FailureResponse(string reason, IEnumerable<IBaseExceptionDetails> details) : base(reason, details)
        {

        }

        public FailureResponse(string reason, IEnumerable<FluentValidationExceptionDetails> details) : base(reason, details)
        {

        }

        public FailureResponse(string reason, IEnumerable<BaseExceptionDetails> details) : base(reason, details)
        {

        }
    }
}
