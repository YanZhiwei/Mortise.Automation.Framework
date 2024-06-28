using System.Diagnostics;
using System.Text.Json.Serialization;
using Mortise.Accessibility.Abstractions.Options;

namespace Mortise.Accessibility.Abstractions;

public abstract class Accessible
{
    [JsonInclude] public string UniqueId { get; protected set; }
    [JsonInclude] public string FileName { get; protected set; }
    [JsonInclude] public AccessibilityProvider Provider { get; protected set; }
    [JsonInclude] public PlatformID Platform { get; protected set; }
    [JsonInclude] public Version Version { get; protected set; }
    [JsonInclude] public AccessibleComponentStack<AccessibleComponent> Components { get; protected set; }
    [JsonIgnore] public AccessibleIdentity Identity { get; protected set; }
    public abstract void Record(object component);
    public abstract AccessibleComponent? FindComponent(Accessible accessible);
    public abstract Task<Process> LaunchAsync<T>(T options) where T : LaunchOptions;
    public abstract Task<Process> AttachAsync<T>(T options) where T : AttachOptions;
    public abstract Task<bool> CloseAsync();

    protected virtual string GenerateUniqueId()
    {
        AccessibleComponent lastComponent = Components.Last();
        if (string.IsNullOrEmpty(lastComponent.Name))
            return lastComponent.Name;
        if (string.IsNullOrEmpty(lastComponent.Id))
            return lastComponent.Id;
        return UniqueId;
    }
}