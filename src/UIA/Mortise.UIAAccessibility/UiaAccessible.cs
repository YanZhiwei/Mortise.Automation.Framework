using System.Diagnostics;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Conditions;
using FlaUI.Core.Definitions;
using FlaUI.UIA3.Identifiers;
using Mortise.Accessibility.Abstractions;
using Tenon.Mapper.Abstractions;
using Tenon.Serialization.Abstractions;
using Tenon.Windows.Extensions;

namespace Mortise.UiaAccessibility;

[Serializable]
public class UiaAccessible : Accessible
{
    public readonly UiaAccessibleIdentity Identity;
    protected readonly IObjectMapper Mapper;
    protected readonly ISerializer Serializer;

    public UiaAccessible(IObjectMapper mapper,
        ISerializer serializer)
    {
        Identity = new UiaAccessibleIdentity(mapper);
        Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        Serializer = serializer;
        Provider = AccessibilityProvider.Uia;
        Platform = PlatformID.Win32NT;
        Version = new Version(3, 0, 0);
    }

    public UiaAccessible()
    {
    }

    protected IntPtr WindowHandle { get; set; }

    protected virtual AutomationElement GetWindowElement(string processName)
    {
        var process = Process.GetProcessesByName(processName).FirstOrDefault();
        var mainWindowHandle = process?.MainWindowHandle ?? WindowHandle;
        if (mainWindowHandle == IntPtr.Zero)
            mainWindowHandle = process.FindFirstCoreWindows();

        return mainWindowHandle == IntPtr.Zero
            ? Identity.DesktopElement
            : Identity.Automation.FromHandle(mainWindowHandle);
    }

    public override void Record(object component)
    {
        if (component is not AutomationElement automationElement) throw new NotSupportedException(nameof(component));
        var uiaComponents = new AccessibleComponentStack<AccessibleComponent>();
        var currentComponent = automationElement;
        FileName = Process.GetProcessById(currentComponent.Properties.ProcessId).ProcessName;
        uiaComponents.Push(Identity.DtoAccessibleComponent(currentComponent, this)!);
        while (currentComponent.Parent != null)
        {
            if (currentComponent.Parent.Equals(Identity.DesktopElement))
                break;
            currentComponent = Identity.TreeWalker.GetParent(currentComponent);
            uiaComponents.Push(Identity.DtoAccessibleComponent(currentComponent, this)!);
        }

        Components = uiaComponents;
    }

    public override AccessibleComponent? FindComponent(string locatorPath)
    {
        if (string.IsNullOrEmpty(locatorPath))
            throw new ArgumentNullException(nameof(locatorPath));

        var uiaAccessible = Serializer.DeserializeObject<UiaAccessible>(locatorPath);
        AutomationElement? foundElement = null;
        var parentElement = GetWindowElement(uiaAccessible.FileName);
        var recordElements = new Stack<AccessibleComponent>(uiaAccessible.Components);
        while (recordElements.TryPop(out var item))
        {
            var condition = CreateCondition(item);
            foundElement = parentElement.FindFirstDescendant(condition);
            if (foundElement == null)
            {
                parentElement = Identity.TreeWalker.GetParent(parentElement);
                foundElement = parentElement.FindFirstChild(condition);
            }

            if (foundElement == null) break;
            parentElement = foundElement;
        }

        return foundElement != null ? Identity.DtoAccessibleComponent(foundElement, this) : null;
    }

    public override void Launch()
    {
        throw new NotImplementedException();
    }

    public override void Attach()
    {
        throw new NotImplementedException();
    }

    public override void Close()
    {
        throw new NotImplementedException();
    }

    protected virtual ConditionBase CreateCondition(AccessibleComponent component)
    {
        var conditions = new ConditionBase[]
        {
            !string.IsNullOrEmpty(component.Id)
                ? new PropertyCondition(AutomationObjectIds.AutomationIdProperty, component.Id)
                : new PropertyCondition(AutomationObjectIds.AutomationIdProperty, ""),
            !string.IsNullOrEmpty(component.Name)
                ? new PropertyCondition(AutomationObjectIds.NameProperty, component.Name)
                : new PropertyCondition(AutomationObjectIds.NameProperty, ""),
            new PropertyCondition(AutomationObjectIds.IsDialogProperty, component.IsDialog),
            new PropertyCondition(AutomationObjectIds.ControlTypeProperty,
                Mapper.Map<ControlType>(component.ControlType))
        };
        return new AndCondition(conditions);
    }
}