using System.Text.Json.Serialization;

namespace Dometrain.DemoAgent;

public class CopilotMetadata
{
    [JsonPropertyName("display_name")]
    public required string DisplayName { get; set; }
    [JsonPropertyName("display_icon")]
    public required string DisplayIcon { get; set; }
    [JsonPropertyName("display_url")]
    public required string DisplayUrl { get; set; }
} 