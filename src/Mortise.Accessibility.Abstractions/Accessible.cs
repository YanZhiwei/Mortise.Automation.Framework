using System.Text.Json.Serialization;

namespace Mortise.Accessibility.Abstractions;

public abstract class Accessible
{
    [JsonInclude] public string FileName { get; protected set; }

    [JsonInclude] public AccessibilityProvider Provider { get; protected set; }

    [JsonInclude] public PlatformID Platform { get; protected set; }

    [JsonInclude] public Version Version { get; protected set; }

    [JsonInclude] public AccessibleComponentStack<AccessibleComponent> Components { get; protected set; }

    [JsonInclude] public AccessibleIdentity Identity { get; protected set; }

    public abstract void Record(object component);

    public abstract AccessibleComponent? FindComponent(string locatorPath);

    public abstract void Launch();

    public abstract void Attach();

    public abstract void Close();
}