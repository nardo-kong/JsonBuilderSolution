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
        public string DivertTime { get; set; } = "20250303171152";

        [JsonProperty(PropertyName = "weight", Order = 3)]
        public string Weight { get; set; } = "6000";

        [JsonProperty(PropertyName = "transferid", Order = 4)]
        public string TransferId { get; set; } = "26955983";

        [JsonProperty(PropertyName = "customer_code", Order = 5)]
        public string CustomerCode { get; set; } = "";

        [JsonProperty(PropertyName = "uuid", Order = 6)]
        public string Uuid { get; set; } = "f1e75a54-46e7-4e8f-87d1-cffa2753b7ae";

        [JsonProperty(PropertyName = "host_order_id", Order = 7)]
        public string HostOrderId { get; set; } = "";
    }
}