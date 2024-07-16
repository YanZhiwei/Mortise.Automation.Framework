using System.Diagnostics;
using System.Drawing;
using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using FlaUI.UIA3;
using Mortise.Accessibility.Abstractions;
using Mortise.UIAAccessibility;
using Tenon.Mapper.Abstractions;

namespace Mortise.UiaAccessibility;

public class UiaAccessibleDetector : AccessibleDetector
{
    public readonly Dictionary<string, IUiaAccessibleDetector> Applications;
    public readonly UIA3Automation Automation = new();
    public readonly AutomationElement DesktopElement;
    protected readonly IObjectMapper Mapper;
    public readonly ITreeWalker TreeWalker;

    public UiaAccessibleDetector(IObjectMapper mapper, IEnumerable<IUiaAccessibleDetector> appAccessibleDetectors)
    {
        Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        TreeWalker = Automation.TreeWalkerFactory.GetControlViewWalker();
        DesktopElement = Automation.GetDesktop();
        Priority = AccessiblePriority.Highest;
        if (appAccessibleDetectors?.Any() ?? false)
            Applications =
                appAccessibleDetectors.ToDictionary(key => key.Descriptor.IdentityString, value => value);
    }

    public override AccessibleComponent? FromPoint(Point location)
    {
        var hoveredElement =
            Automation.FromPoint(location);
        if (hoveredElement == null) return null;
        TreeWalker.GetParent(hoveredElement);
        var processName = Process.GetProcessById(hoveredElement.Properties.ProcessId).ProcessName;
        var findKey =
            Applications?.Keys?.FirstOrDefault(c =>
                c.Contains(processName, StringComparison.OrdinalIgnoreCase));
        if (!string.IsNullOrEmpty(findKey))
            hoveredElement = Applications[findKey]
                .FromHoveredElement(location, hoveredElement, TreeWalker);
        return DtoAccessibleComponent(hoveredElement);
    }

    public override Task<AccessibleComponent?> FromPointAsync(Point location)
    {
        throw new NotImplementedException();
    }

    public override AccessibleComponent? DtoAccessibleComponent(object element, Accessible? accessibility = null)
    {
        if (element is not AutomationElement automationElement) return null;
        return new UiaAccessibleComponent
        {
            Name = automationElement.Properties.Name.ValueOrDefault,
            ActualWidth = automationElement.ActualWidth,
            ActualHeight = automationElement.ActualHeight,
            BoundingRectangle = automationElement.BoundingRectangle,
            Id = automationElement.Properties.AutomationId.ValueOrDefault,
            IsEnabled = automationElement.Properties.IsEnabled.ValueOrDefault,
            IsOffscreen = automationElement.Properties.IsOffscreen.ValueOrDefault,
            IsDialog = automationElement.Properties.IsDialog.ValueOrDefault,
            NativeElement = automationElement,
            ControlType = Mapper.Map<AccessibleControlType>(automationElement.ControlType),
            Accessible = accessibility,
            ClassName = automationElement.Properties.ClassName.ValueOrDefault,
            IsPassword = automationElement.Properties.IsPassword.ValueOrDefault
        };
    }
}