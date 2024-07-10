namespace Mortise.BrowserAccessibility;

public class Frame
{
    private Frame? Parent { get; set; }
    public string Name { get; set; }
    public string Src { get; set; }
    public string Id { get; set; }

    public Stack<Frame> Child { get; set; }
}