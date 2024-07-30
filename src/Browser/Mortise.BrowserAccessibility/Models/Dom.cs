namespace Mortise.BrowserAccessibility.Models;

public sealed class Dom
{
    public string CustomId { get; set; }
    public string TagName { get; set; }
    public int IframeIndex { get; set; } = -1;
    public DomRect? BoundingRect { get; set; }

    public override string ToString()
    {
        return $"{TagName} {CustomId}";
    }
}