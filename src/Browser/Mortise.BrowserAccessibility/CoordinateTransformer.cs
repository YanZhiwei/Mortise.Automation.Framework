namespace Mortise.BrowserAccessibility;

public class CoordinateTransformer
{
    public static CssPoint ScreenPointToCssPoint(CssPoint screenPoint, RenderOption renderOption)
    {
        var finalPoint = screenPoint;
        finalPoint = finalPoint.Offset(
            -(renderOption.WindowLeft + renderOption.PageRenderX),
            -(renderOption.WindowTop + renderOption.PageRenderY)
        );

        finalPoint = finalPoint.ScaleInv(1);

        return finalPoint;
    }

    private object GetPageRenderOffsetInfo(RenderOption renderOption)
    {
        throw new NotImplementedException();
    }
}