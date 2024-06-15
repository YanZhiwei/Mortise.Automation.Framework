using FlaUI.Core.AutomationElements;
using Mortise.Accessibility.Abstractions;

namespace Mortise.UiaAccessibility;

public class UiaAccessibleComponent : AccessibleComponent, IAccessibleAction
{
    public void Click()
    {
        CheckNativeElement(out var automationElement);
        automationElement.Click();
    }

    protected void CheckNativeElement(out AutomationElement automationElement)
    {
        if (NativeElement is not AutomationElement uiaElement)
            throw new InvalidOperationException(nameof(Click));
        automationElement = uiaElement;
    }
}