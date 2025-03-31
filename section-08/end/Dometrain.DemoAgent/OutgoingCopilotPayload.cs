namespace Dometrain.DemoAgent;

public class OutgoingCopilotPayload
{
    public bool Stream { get; set; }
    public List<CopilotMessage> Messages { get; set; } = [];
}
