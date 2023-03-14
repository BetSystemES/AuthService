namespace AuthService.Grpc.Interceptors.Helpers
{
    public class StatusMessage
    {
        public bool IsSuccessful { get; set; }

        public string? Reason { get; set; }

        public IEnumerable<GrpcExceptionDetail> Details { get; set; }

        public StatusMessage(string reason, IEnumerable<GrpcExceptionDetail> details)
        {
            IsSuccessful = false;
            Reason = reason;
            Details = details;
        }
    }
}
