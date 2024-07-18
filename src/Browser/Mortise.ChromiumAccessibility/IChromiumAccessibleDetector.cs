using System.Diagnostics;
using System.Drawing;
using Mortise.Accessibility.Abstractions;

namespace Mortise.ChromiumAccessibility;

public interface IChromiumAccessibleDetector
{
    public IAccessibleDescriptor Descriptor { get; }

    public ChromiumAccessibleComponent? FromHoveredElement(ChromiumAccessible chromiumAccessible, Point location, Process process);

    public Task<ChromiumAccessibleComponent?> FromHoveredElementAsync(ChromiumAccessible chromiumAccessible, Point location, Process process);
}