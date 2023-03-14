namespace AuthService.Grpc.Interceptors.Helpers
{
    public class FluentValidationExceptionDetails : IBaseExceptionDetails
    {
        public string Field { get; set; }
        public string Message { get; set; }

        public FluentValidationExceptionDetails()
        {
        }

        public FluentValidationExceptionDetails(string field, string message)
        {
            Field = field;
            Message = message;
        }
    }
}
