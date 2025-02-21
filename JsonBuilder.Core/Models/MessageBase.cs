using JsonBuilder.Core.Models.Parameters;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace JsonBuilder.Core.Models
{
    public abstract class MessageBase
    {
        protected MessageBase()
        {
            NestedMessages = new ObservableCollection<MessageBase>();
        }

        [JsonProperty("nested")]
        public ObservableCollection<MessageBase> NestedMessages { get; set; }

        [JsonProperty("messagetype")]
        public abstract string MessageType { get; }

        [JsonProperty("parameters")]
        public virtual MessageParamBase Parameters { get; set; }

        // 用于下拉菜单显示的友好名称
        public virtual string DisplayName => MessageType;

        // 创建新实例的工厂方法
        public abstract MessageBase CreateNewInstance();
    }
}