using System.Text.Json.Serialization;

namespace Mortise.Accessibility.Abstractions;

public abstract class Accessible
{
    [JsonInclude] public string UniqueId { get; protected set; }
    [JsonInclude] public string FileName { get; protected set; }
    [JsonInclude] public AccessibilityProvider Provider { get; protected set; }
    [JsonInclude] public PlatformID Platform { get; protected set; }
    [JsonInclude] public Version Version { get; protected set; }
    [JsonInclude] public AccessibleComponentStack<AccessibleComponent> Components { get; protected set; }
    [JsonIgnore] public AccessibleDetector Detector { get; protected set; }
    public abstract void Record(object component);
    public abstract AccessibleComponent? FindComponent(Accessible accessible);

    protected virtual string GenerateUniqueId()
    {
        var lastComponent = Components.Last();
        if (string.IsNullOrEmpty(lastComponent.Name))
            return lastComponent.Name;
        if (string.IsNullOrEmpty(lastComponent.Id))
            return lastComponent.Id;
        return UniqueId;
    }
}