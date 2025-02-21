using JsonBuilder.Core.Models;
using JsonBuilder.Core.Models.Parameters;
using Newtonsoft.Json;

namespace JsonBuilder.Core.Models.Messages
{
    public class PickConfirmMessage : MessageBase
    {
        public override string MessageType => "pick_confirm";

        public override MessageBase CreateNewInstance() => new PickConfirmMessage();

        [JsonProperty("parameters")]
        public override MessageParamBase Parameters {
            get => _params ??= new PickConfirmParams();
            set => _params = value as PickConfirmParams;
        }
        public PickConfirmParams _params;
    }

    public class PickConfirmParams : MessageParamBase
    {
        [JsonProperty(PropertyName = "box_number", Order = 1)]
        public string BoxNumber { get; set; } = "";

        [JsonProperty(PropertyName = "divert_time", Order = 2)]
        public string DivertTime { get; set; } = "";

        [JsonProperty(PropertyName = "weight", Order = 3)]
        public string Weight { get; set; } = "";

        [JsonProperty(PropertyName = "transferid", Order = 4)]
        public string TransferId { get; set; } = "";

        [JsonProperty(PropertyName = "customer_code", Order = 5)]
        public string CustomerCode { get; set; } = "";

        [JsonProperty(PropertyName = "uuid", Order = 6)]
        public string Uuid { get; set; } = "";

        [JsonProperty(PropertyName = "host_order_id", Order = 7)]
        public string HostOrderId { get; set; } = "";
    }
}