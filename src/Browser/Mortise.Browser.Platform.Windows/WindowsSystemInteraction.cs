using System.Diagnostics;
using System.Drawing;
using Mortise.Browser.Platform;
using Mortise.BrowserAccessibility.Models;
using Tenon.Infra.Windows.Win32;
using Process = System.Diagnostics.Process;

namespace Mortise.Platform.Windows;

public class WindowsSystemInteraction : ISystemInteraction
{
    public IntPtr GetWindowHandle(Point point = default)
    {
        return Window.GetTop(point);
    }

    public Process? GetProcess(Point point = default)
    {
        var handle = Window.GetTop(point);
        if (handle == IntPtr.Zero)
            return null;
        var processId = Window.GetProcessId(handle);
        return Process.GetProcessById((int)processId);
    }

    public Point ScreenPointToWebPoint(Point screenPoint)
    {
        Debug.WriteLine($"ScreenPointToWebPoint:{screenPoint.ToString()}");
        var displayDevice = DisplayMonitor.FromPoint(screenPoint);
        Debug.WriteLine($"point:{screenPoint.ToString()}=>{displayDevice.ScaleX}");
        //displayDevice.ScaleX = 2;
        //displayDevice.ScaleY = 2;
        if (displayDevice == null) return screenPoint;
        var locationY = screenPoint.Y / displayDevice.ScaleY;
        float locationX = screenPoint.X;
        if (locationX < 0)
            locationX = (locationX + displayDevice.VirtualResolution.Width) / displayDevice.ScaleX;
        else if (locationX > displayDevice.VirtualResolution.Width)
            locationX = (locationX - displayDevice.VirtualResolution.Width) / displayDevice.ScaleX;
        return new Point((int)locationX, (int)locationY);
    }

    public Rectangle WebPointToScreenRectangle(Point screenPoint, DomRect domRect)
    {
        var domRectangle = new Rectangle(domRect.X, domRect.Y, domRect.Width, domRect.Height);

        var displayDevice = DisplayMonitor.FromPoint(domRect.Location);
        if (displayDevice == null) return domRectangle;
        if (screenPoint is { X: >= 0, Y: >= 0 } && screenPoint.X < displayDevice.VirtualResolution.Width && screenPoint.Y < displayDevice.VirtualResolution.Height)
            return domRectangle;
        Debug.WriteLine($"displayDevice:{displayDevice.VirtualResolution.Width}");
        if (screenPoint.X < 0)
            domRectangle.X =
                Convert.ToInt32(domRectangle.X * displayDevice.ScaleX - displayDevice.VirtualResolution.Width);
        if (screenPoint.X > displayDevice.VirtualResolution.Width)
            domRectangle.X =
                Convert.ToInt32(domRectangle.X * displayDevice.ScaleX + displayDevice.VirtualResolution.Width);
        domRectangle.Y = Convert.ToInt32(domRectangle.Y * displayDevice.ScaleY);
        return domRectangle with { Width = domRect.Width, Height = domRect.Height };
    }
}