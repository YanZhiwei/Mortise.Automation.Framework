using System.Drawing;
using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using Mortise.Accessibility.Abstractions;

namespace Mortise.UIAAccessibility;

public interface IUiaAccessibleIdentity
{
    public IAccessibleMetadata Metadata { get; }

    public AutomationElement? FromHoveredElement(Point location, AutomationElement hoveredElement,
        ITreeWalker treeWalker);
}