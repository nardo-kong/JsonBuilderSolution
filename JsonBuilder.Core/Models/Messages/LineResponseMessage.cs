using Newtonsoft.Json;

namespace JsonBuilder.Core.Models.Messages;
public class LineResponseMessage : MessageBase
{
    public override string MessageType => "line_response";

    [JsonProperty("parameters")]
    public LineResponseParams Params { get; set; } = new();

    public override MessageBase CreateNewInstance() => new LineResponseMessage();
}

public class LineResponseParams
{
    public string host_line_id { get; set; } = "";
    public string article_id { get; set; } = "";
    public string geo_code { get; set; } = "";
    public string ordered_packunits { get; set; } = "";
    public string picked_packunits { get; set; } = "";
    public string packunit_size { get; set; } = "";
}