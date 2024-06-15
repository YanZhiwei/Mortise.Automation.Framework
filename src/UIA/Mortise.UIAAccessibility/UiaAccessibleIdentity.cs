using System.Diagnostics;
using System.Drawing;
using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using FlaUI.UIA3;
using Mortise.Accessibility.Abstractions;
using Mortise.UIAAccessibility;
using Tenon.Mapper.Abstractions;

namespace Mortise.UiaAccessibility;

public class UiaAccessibleIdentity : AccessibleIdentity
{
    public readonly Dictionary<string, IUiaAccessibleIdentity> AccessibilityIdentities;
    public readonly UIA3Automation Automation = new();
    public readonly AutomationElement DesktopElement;
    protected readonly IObjectMapper Mapper;
    public readonly ITreeWalker TreeWalker;

    //static UiaAccessibleIdentity()
    //{
    //    AccessibilityIdentities = ReflectHelper
    //        .CreateInterfaceTypeInstances<IUiaAccessibleIdentity>()
    //        .ToDictionary(key => key.Metadata.IdentityString, value => value);
    //}

    public UiaAccessibleIdentity(IObjectMapper mapper, IEnumerable<IUiaAccessibleIdentity> uiaAccessibleIdentities)
    {
        Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        TreeWalker = Automation.TreeWalkerFactory.GetControlViewWalker();
        DesktopElement = Automation.GetDesktop();
        Priority = AccessiblePriority.Highest;
        if (AccessibilityIdentities?.Any() ?? false)
            AccessibilityIdentities =
                uiaAccessibleIdentities.ToDictionary(key => key.Metadata.IdentityString, value => value);
    }

    public override AccessibleComponent? FromPoint(Point location)
    {
        var hoveredElement =
            Automation.FromPoint(location);
        if (hoveredElement == null) return null;
        TreeWalker.GetParent(hoveredElement);
        var processName = Process.GetProcessById(hoveredElement.Properties.ProcessId).ProcessName;
        var findKey =
            AccessibilityIdentities.Keys.FirstOrDefault(c =>
                c.Contains(processName, StringComparison.OrdinalIgnoreCase));
        if (!string.IsNullOrEmpty(findKey))
            hoveredElement = AccessibilityIdentities[findKey]
                .FromHoveredElement(location, hoveredElement, TreeWalker);
        return DtoAccessibleComponent(hoveredElement);
    }

    public override AccessibleComponent? DtoAccessibleComponent(object element, Accessible? accessibility = null)
    {
        if (element is not AutomationElement automationElement) return null;
        return new AccessibleComponent
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
            Accessible = accessibility
        };
    }
}