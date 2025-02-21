using JsonBuilder.Core.Models;
using Newtonsoft.Json;

namespace JsonBuilder.Core.Models.Messages
{
    public class OrderInsertMessage : MessageBase
    {

        public override string MessageType => "order_insert";

        public override MessageBase CreateNewInstance() => new OrderInsertMessage();
    }

    public class OrderInsertParams
    {
        public int transferid { get; set; }
        public string uuid { get; set; }
        public string host_order_id { get; set; }
        public string order_type { get; set; }
        public string packing_required { get; set; }
        public string packing_workstation { get; set; }
        public string destination { get; set; }
        public string customer_code { get; set; }
        public string departure_date { get; set; }
        public string departure_time { get; set; }
        public string batch_id { get; set; }
    }
}