using FluentValidation;
using Grpc.Core;
using Newtonsoft.Json;

namespace AuthService.Grpc.Interceptors.Helpers
{
    public static class ExceptionHelpers
    {
        public static RpcException Handle<T>(this Exception exception, ServerCallContext context, ILogger<T> logger)
        {
            return exception switch
            {
                TimeoutException => HandleTimeoutException((TimeoutException)exception, context, logger),
                RpcException => HandleRpcException((RpcException)exception, logger),
                ValidationException => HandleValidationException((ValidationException)exception, logger),
                _ => HandleDefault(exception, context, logger)
            };
        }

        private static RpcException HandleTimeoutException<T>(TimeoutException exception, ServerCallContext context, ILogger<T> logger)
        {
            logger.LogError(exception, $"An TimeoutException occurred");

            var status = new Status(StatusCode.Internal, "An external resource did not answer within the time limit");

            return new RpcException(status);
        }

        private static RpcException HandleValidationException<T>(ValidationException exception, ILogger<T> logger)
        {
            logger.LogError(exception, $"An ValidationException occurred");

            var details = exception.Errors.Select(x => new GrpcExceptionDetail(x.PropertyName, x.ErrorMessage));
            var failureResponse = new StatusMessage(typeof(ValidationException).Name, details);

            var exeptionMessageString = JsonConvert.SerializeObject(failureResponse, Formatting.Indented);

            var status = new Status(StatusCode.Internal, exeptionMessageString);

            return new RpcException(status);
        }

        private static RpcException HandleRpcException<T>(RpcException exception, ILogger<T> logger)
        {
            logger.LogError(exception, $"An RpcException occurred");
            return new RpcException(new Status(exception.StatusCode, exception.Message));
        }

        private static RpcException HandleDefault<T>(Exception exception, ServerCallContext context, ILogger<T> logger)
        {
            logger.LogError(exception, $"An Exception occurred");
            return new RpcException(new Status(StatusCode.Internal, exception.Message));
        }
    }
}
