namespace Mortise.BrowserAccessibility.Options;

public class InjectScriptOptions
{
    public string Content { get; set; }
    public string Type { get; set; } = "text/javascript";
    public required object Id { get; set; }
}