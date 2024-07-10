using System.Diagnostics;
using System.Drawing;

namespace Mortise.BrowserAccessibility;

public interface IBrowserAccessibleContext
{
    public Process[] GetRunningProcess();

    /// <summary>
    ///     获取矩形框
    /// </summary>
    /// <returns></returns>
    public Rectangle GetBoundingRectangle();

    /// <summary>
    ///     获取RenderWidget矩形框
    /// </summary>
    /// <returns></returns>
    public Rectangle GetRenderingBoundingRectangle();

    public void SetMaximize();

    public void SetMinimize();

    public BrowserDescriptor GetDescriptor();
}