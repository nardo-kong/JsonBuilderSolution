using Newtonsoft.Json;
using System.Collections.Generic;

namespace JsonBuilder.Core.Models
{
    public abstract class MessageBase
    {
        protected MessageBase()
        {
            NestedMessages = new List<MessageBase>();
            Parameters = new Dictionary<string, object>();
        }

        [JsonProperty("nested")]
        public List<MessageBase> NestedMessages { get; set; }

        [JsonProperty("messagetype")]
        public abstract string MessageType { get; }

        [JsonProperty("parameters")]
        public Dictionary<string, object> Parameters { get; set; }

        // 用于下拉菜单显示的友好名称
        public virtual string DisplayName => MessageType;

        // 创建新实例的工厂方法
        public abstract MessageBase CreateNewInstance();
    }
}