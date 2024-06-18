using System.Diagnostics;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Conditions;
using FlaUI.Core.Definitions;
using FlaUI.UIA3.Identifiers;
using Mortise.Accessibility.Abstractions;
using Mortise.UIAAccessibility;
using Tenon.Mapper.Abstractions;
using Tenon.Serialization.Abstractions;
using Tenon.Windows.Extensions;

namespace Mortise.UiaAccessibility;

[Serializable]
public class UiaAccessible : Accessible
{
    protected readonly IObjectMapper Mapper;
    public readonly UiaAccessibleIdentity NativeIdentity;
    protected readonly ISerializer Serializer;

    public UiaAccessible(IObjectMapper mapper,
        ISerializer serializer, IEnumerable<IUiaAccessibleIdentity> uiaAccessibleIdentities)
    {
        Identity = new UiaAccessibleIdentity(mapper, uiaAccessibleIdentities);
        NativeIdentity = (UiaAccessibleIdentity)Identity;
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
            ? NativeIdentity.DesktopElement
            : NativeIdentity.Automation.FromHandle(mainWindowHandle);
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
            if (currentComponent.Parent.Equals(NativeIdentity.DesktopElement))
                break;
            currentComponent = NativeIdentity.TreeWalker.GetParent(currentComponent);
            uiaComponents.Push(NativeIdentity.DtoAccessibleComponent(currentComponent, this)!);
        }

        Components = uiaComponents;
        UniqueId = GenerateUniqueId();
    }

    public override AccessibleComponent? FindComponent(string locatorString)
    {
        if (string.IsNullOrWhiteSpace(locatorString))
            throw new ArgumentNullException(nameof(locatorString));

        var uiaAccessible = Serializer.DeserializeObject<UiaAccessible>(locatorString);
        AutomationElement? foundElement = null;
        var parentElement = GetWindowElement(uiaAccessible.FileName);
        var recordElements = new Stack<AccessibleComponent>(uiaAccessible.Components);
        while (recordElements.TryPop(out var item))
        {
            var condition = CreateCondition(item);
            foundElement = parentElement.FindFirstDescendant(condition);
            if (foundElement == null)
            {
                parentElement = NativeIdentity.TreeWalker.GetParent(parentElement);
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

    protected override string GenerateUniqueId()
    {
        if (string.IsNullOrWhiteSpace(FileName))
            throw new ArgumentNullException(nameof(FileName));
        if (!(Components?.Any() ?? false))
            throw new InvalidOperationException(nameof(Components));
        if (Components.Last() is not UiaAccessibleComponent lastComponent)
            throw new NotSupportedException(nameof(Components));
        if (!string.IsNullOrWhiteSpace(lastComponent.Id))
            return $"{FileName}|{lastComponent.Id}";
        if (!string.IsNullOrWhiteSpace(lastComponent.Name))
            return $"{FileName}|{lastComponent.Name}";
        if (!string.IsNullOrWhiteSpace(lastComponent.ClassName))
            return $"{FileName}|{lastComponent.ClassName}";
        return $"{FileName}|{lastComponent.ControlType}|{lastComponent.GetHashCode()}";

    }

    protected virtual ConditionBase CreateCondition(AccessibleComponent component)
    {
        if (component is not UiaAccessibleComponent uiaComponent)
            throw new InvalidOperationException(nameof(component));
        var conditions = new ConditionBase[]
        {
            !string.IsNullOrEmpty(component.Id)
                ? new PropertyCondition(AutomationObjectIds.AutomationIdProperty, component.Id)
                : new PropertyCondition(AutomationObjectIds.AutomationIdProperty, ""),
            !string.IsNullOrEmpty(uiaComponent.ClassName)
                ? new PropertyCondition(AutomationObjectIds.ClassNameProperty, uiaComponent.ClassName)
                : new PropertyCondition(AutomationObjectIds.ClassNameProperty, ""),
            !string.IsNullOrEmpty(component.Name)
                ? new PropertyCondition(AutomationObjectIds.NameProperty, component.Name)
                : new PropertyCondition(AutomationObjectIds.NameProperty, ""),
            new PropertyCondition(AutomationObjectIds.IsDialogProperty, component.IsDialog),
            new PropertyCondition(AutomationObjectIds.IsPasswordProperty, component.IsPassword),
            new PropertyCondition(AutomationObjectIds.ControlTypeProperty,
                Mapper.Map<ControlType>(component.ControlType))
        };
        return new AndCondition(conditions);
    }
}