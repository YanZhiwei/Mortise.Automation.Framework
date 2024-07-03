using System.Drawing;
using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using Mortise.Accessibility.Abstractions;

namespace Mortise.UIAAccessibility;

public interface IUiaAccessibleDetector
{
    public IAccessibleDescriptor Descriptor { get; }

    public AutomationElement? FromHoveredElement(Point location, AutomationElement hoveredElement,
        ITreeWalker treeWalker);
}