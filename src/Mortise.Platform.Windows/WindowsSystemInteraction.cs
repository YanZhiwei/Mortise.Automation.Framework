using System.Drawing;
using Tenon.Infra.Windows.Win32;

namespace Mortise.Platform.Windows;

public class WindowsSystemInteraction : ISystemInteraction
{
    public IntPtr GetWindowHandle(Point point = default)
    {
        return point.IsEmpty ? IntPtr.Zero : Window.Get(point);
    }
}