using JsonBuilder.Core.Models.Parameters;
using Newtonsoft.Json;

namespace JsonBuilder.Core.Models.Messages;
public class LineResponseMessage : MessageBase
{
    public override string MessageType => "line_response";

    public override MessageBase CreateNewInstance() => new LineResponseMessage();

    [JsonProperty("parameters")]
    public override MessageParamBase Parameters
    {
        get => _params ??= new LineResponseParams();
        set => _params = value as LineResponseParams;
    }
    public LineResponseParams _params;

    
}

public class LineResponseParams : MessageParamBase
{
    [JsonProperty(PropertyName = "host_line_id", Order = 1)]
    public string HostLineId { get; set; } = "";

    [JsonProperty(PropertyName = "article_id", Order = 2)]
    public string ArticleId { get; set; } = "";

    [JsonProperty(PropertyName = "geo_code", Order = 3)]
    public string GeoCode { get; set; } = "";

    [JsonProperty(PropertyName = "ordered_packunits", Order = 4)]
    public string OrderedPackunits { get; set; } = "";

    [JsonProperty(PropertyName = "picked_packunits", Order = 5)]
    public string PickedPackunits { get; set; } = "";

    [JsonProperty(PropertyName = "packunit_size", Order = 6)]
    public string PackunitSize { get; set; } = "1";
}