using AuthService.Grpc.Interceptors.Models;
using FluentValidation;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;
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
                DbUpdateException => HandleDbUpdateException((DbUpdateException)exception, logger),
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
            var statusMessage = new StatusMessage(typeof(ValidationException).Name, details);

            var exeptionMessageString = JsonConvert.SerializeObject(statusMessage, Formatting.Indented);

            var status = new Status(StatusCode.Internal, exeptionMessageString);

            return new RpcException(status);
        }

        private static RpcException HandleDbUpdateException<T>(DbUpdateException exception, ILogger<T> logger)
        {
            logger.LogError(exception, $"An DbUpdateException occurred, with message={exception.Message}");

            var statusMessage = new StatusMessage(typeof(DbUpdateException).Name,
                new List<GrpcExceptionDetail>()
                {
                    new GrpcExceptionDetail(exception.Message)
                });

            var exeptionMessageString = JsonConvert.SerializeObject(statusMessage, Formatting.Indented);

            var status = new Status(StatusCode.Internal, exeptionMessageString);

            return new RpcException(status);
        }

        private static RpcException HandleRpcException<T>(RpcException exception, ILogger<T> logger)
        {
            logger.LogError(exception, $"An RpcException occurred, with message={exception.Message}");

            var statusMessage = new StatusMessage(typeof(RpcException).Name,
                new List<GrpcExceptionDetail>()
                {
                    new GrpcExceptionDetail(exception.Message)
                });

            var exeptionMessageString = JsonConvert.SerializeObject(statusMessage, Formatting.Indented);

            var status = new Status(StatusCode.Internal, exeptionMessageString);

            return new RpcException(status);
        }

        private static RpcException HandleDefault<T>(Exception exception, ServerCallContext context, ILogger<T> logger)
        {
            logger.LogError(exception, $"An Exception occurred");
            return new RpcException(new Status(StatusCode.Internal, exception.Message));
        }
    }
}
