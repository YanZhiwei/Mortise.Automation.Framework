using System.Drawing;

namespace Mortise.BrowserAccessibility.Models;

public sealed class DomRect
{
    public int X { get; set; }
    public int Y { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public int FrameOffsetX { get; set; }
    public int FrameOffsetY { get; set; }
    public string TagName { get; set; }

    public Point Location => new(X, Y);
}