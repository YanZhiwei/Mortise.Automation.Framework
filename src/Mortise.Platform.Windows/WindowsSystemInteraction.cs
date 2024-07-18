using System.Drawing;
using Tenon.Infra.Windows.Win32;
using Process = System.Diagnostics.Process;

namespace Mortise.Platform.Windows;

public class WindowsSystemInteraction : ISystemInteraction
{
    public IntPtr GetWindowHandle(Point point = default)
    {
        return point.IsEmpty ? IntPtr.Zero : Window.Get(point);
    }

    public Process GetProcess(Point location)
    {
        throw new NotImplementedException();
    }
}