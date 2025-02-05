using Newtonsoft.Json;
using JsonBuilder.Core.Models;
using Newtonsoft.Json.Serialization;

namespace JsonBuilder.Core.Utilities
{
    public static class JsonGenerator
    {
        private static readonly JsonSerializerSettings _settings = new()
        {
            Formatting = Formatting.Indented,
            NullValueHandling = NullValueHandling.Ignore,
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        public static string Generate(MessageBase message)
        {
            return JsonConvert.SerializeObject(message, _settings);
        }

        public static string GenerateDynamic(Dictionary<string, object> parameters)
        {
            return JsonConvert.SerializeObject(parameters, _settings);
        }
    }
}