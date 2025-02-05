﻿using Newtonsoft.Json;
using JsonBuilder.Core.Models;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Linq;
using JsonBuilder.Core.Models.Messages;

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
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                Formatting = Formatting.Indented,
                Converters = { new MessageConverter() }
            };

            return JsonConvert.SerializeObject(message, settings);
        }

        public static string GenerateDynamic(Dictionary<string, object> parameters)
        {
            return JsonConvert.SerializeObject(parameters, _settings);
        }
    }

    public class MessageConverter : JsonConverter<MessageBase>
    {
        public override MessageBase ReadJson(JsonReader reader, Type objectType, MessageBase? existingValue, bool hasExistingValue,
            JsonSerializer serializer) 
        {
            /* 反序列化逻辑 */
            JObject obj = JObject.Load(reader);
            string messageType = obj["messagetype"]?.Value<string>() ?? "";

            return messageType switch
            {
                "pick_confirm" => ParsePickConfirm(obj),
                "line_response" => ParseLineResponse(obj),
                _ => throw new JsonException($"Unknown messagetype: {messageType}")
            };
        }

        public override void WriteJson(JsonWriter writer, MessageBase? value, JsonSerializer serializer)
        {
            if (value is null)
            {
                writer.WriteNull();
                return;
            }

            JObject obj = new JObject
            {
                ["messagetype"] = value.MessageType,
                ["nested"] = JArray.FromObject(value.NestedMessages, serializer)
            };

            var parameters = value.GetType().GetProperty("Params")?.GetValue(value);
            if (parameters != null)
            {
                obj["parameters"] = JObject.FromObject(parameters, serializer);
            }

            obj.WriteTo(writer);
        }

        private static PickConfirmMessage ParsePickConfirm(JObject obj)
        {
            return new PickConfirmMessage
            {
                Params = obj["parameters"]?.ToObject<PickConfirmParams>() ?? new(),
                NestedMessages = ParseNested(obj["nested"] as JArray)
            };
        }

        private static LineResponseMessage ParseLineResponse(JObject obj)
        {
            return new LineResponseMessage
            {
                Params = obj["parameters"]?.ToObject<LineResponseParams>() ?? new(),
                NestedMessages = ParseNested(obj["nested"] as JArray)
            };
        }

        private static List<MessageBase> ParseNested(JArray? nestedArray)
        {
            // 嵌套解析逻辑（示例）
            return nestedArray?.Select(token => token.ToObject<MessageBase>()!)
                              .Where(message => message != null)
                              .ToList() ?? new List<MessageBase>();
        }
    }
}