using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using AuthService.Grpc.Interceptors.Helpers;

namespace AuthService.Grpc.Interceptors.cov
{
    public class BaseExceptionDetailConverter : JsonConverter
    {
        public override bool CanWrite => false;

        public override bool CanRead => true;

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(IBaseExceptionDetails);
        }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            throw new InvalidOperationException("Use default serialization.");
        }

        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            var jsonObject = JObject.Load(reader);
            var detail = default(IBaseExceptionDetails);

            var type = jsonObject["$type"].ToString();
            var bool1 = type.Contains(nameof(FluentValidationExceptionDetails));
            if (bool1)
            {
                serializer.Populate(jsonObject.CreateReader(), new FluentValidationExceptionDetails());
            }
            return detail;
        }
    }
}
