namespace Mortise.BrowserAccessibility;

public sealed class CssPoint
{
    public CssPoint(int x, int y)
    {
        X = x;
        Y = y;
    }
    public int X { get; private init; }
    public int Y { get; private init; }

    public CssPoint Offset(int offsetX, int offsetY)
    {
        return new CssPoint(X + offsetX, Y + offsetY);
            // { X = X + offsetX, Y = Y + offsetY };
    }

    public CssPoint ScaleInv(int scale)
    {
        return new CssPoint(X / scale, Y / scale);
    }
}