using System.Drawing;

namespace Mortise.Accessibility.Abstractions;

public abstract class AccessibleDetector
{
    public string ProcessName { get; protected set; } = "*";
    public AccessiblePriority Priority { get; protected set; } = AccessiblePriority.Normal;
    public abstract AccessibleComponent? FromPoint(Point location);

    public abstract Task<AccessibleComponent?> FromPointAsync(Point location);
    public abstract AccessibleComponent? DtoAccessibleComponent(object element, Accessible? accessibility = null);
}