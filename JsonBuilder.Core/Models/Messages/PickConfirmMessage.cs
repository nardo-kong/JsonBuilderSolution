using JsonBuilder.Core.Models;
using Newtonsoft.Json;

namespace JsonBuilder.Core.Models.Messages
{
    public class PickConfirmMessage : MessageBase
    {
        public PickConfirmMessage()
        {
            // 初始化默认参数
            Parameters = new Dictionary<string, object>
            {
                { "box_number", "默认值" },
                { "divert_time", DateTime.Now.ToString("yyyyMMddHHmmss") }
            };
        }

        public override string MessageType => "pick_confirm";

        public override MessageBase CreateNewInstance() => new PickConfirmMessage();
    }

    public class PickConfirmParams
    {
        public string box_number { get; set; }
        public string divert_time { get; set; }
        public string weight { get; set; }
        public string transferid { get; set; }
        public string customer_code { get; set; }
        public string uuid { get; set; }
        public string host_order_id { get; set; }
    }
}