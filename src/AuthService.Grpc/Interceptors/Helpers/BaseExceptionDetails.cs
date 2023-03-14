﻿namespace AuthService.Grpc.Interceptors.Helpers
{
    public class BaseExceptionDetails : IBaseExceptionDetails
    {
        public string Message { get; set; }

        public BaseExceptionDetails(string message)
        {
            Message = message;
        }
    }
}
