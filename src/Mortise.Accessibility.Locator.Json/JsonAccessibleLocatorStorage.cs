using System.Collections.Concurrent;
using System.Text;
using Mortise.Accessibility.Abstractions;
using Mortise.Accessibility.Locator.Abstractions;
using Tenon.Serialization.Abstractions;

namespace Mortise.Accessibility.Locator.Json;

public sealed class JsonAccessibleLocatorStorage(ISerializer serializer) : IAccessibleLocatorStorage
{
    private readonly ConcurrentDictionary<string, HashSet<Accessible>> _accessibleDict = new();
    private readonly ISerializer _serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));

    public bool Add(Accessible accessible)
    {
        if (accessible == null)
            throw new ArgumentNullException(nameof(accessible));
        var key = accessible.FileName;
        if (_accessibleDict.TryGetValue(key, out var accessibles))
        {
            accessibles ??= [];
            if (accessibles.Any(c => c.UniqueId.Equals(accessible.UniqueId, StringComparison.OrdinalIgnoreCase)))
                return false;
            accessibles.Add(accessible);
        }
        else
        {
            return _accessibleDict.TryAdd(key, [accessible]);
        }

        return true;
    }

    public Accessible? Remove(string uniqueId, string? fileName = null)
    {
        if (!string.IsNullOrWhiteSpace(uniqueId))
            throw new ArgumentNullException(nameof(uniqueId));
        var key = fileName?.Trim();
        Accessible? removeAccessible = null;
        var removeResult = false;
        if (!string.IsNullOrWhiteSpace(key))
        {
            if (!_accessibleDict.TryGetValue(key, out var accessibles)) return null;
            if (!(accessibles?.Any() ?? false)) return null;
            removeAccessible = accessibles.FirstOrDefault(c =>
                c.UniqueId.Equals(uniqueId, StringComparison.OrdinalIgnoreCase));
            if (removeAccessible != null) removeResult = accessibles.Remove(removeAccessible);
        }
        else
        {
            var allAccessible = _accessibleDict.Values.SelectMany(c => c).ToList();
            removeAccessible =
                allAccessible.FirstOrDefault(c => c.UniqueId.Equals(uniqueId, StringComparison.OrdinalIgnoreCase));
            if (removeAccessible != null) removeResult = allAccessible.Remove(removeAccessible);
        }

        return removeResult ? removeAccessible : null;
    }

    public void Save()
    {
        var accessibleLocatorJsonString = _serializer.SerializeObject(_accessibleDict);
        if (string.IsNullOrWhiteSpace(accessibleLocatorJsonString))
            throw new InvalidDataException(nameof(accessibleLocatorJsonString));
        File.WriteAllText("test.json", accessibleLocatorJsonString, Encoding.UTF8);
    }

    public long GetCount(string? fileName = null)
    {
        var key = fileName?.Trim();
        if (string.IsNullOrWhiteSpace(key)) return _accessibleDict.Values.SelectMany(c => c).LongCount();
        if (_accessibleDict.TryGetValue(key, out var accessibles)) return accessibles?.Count ?? 0;
        return 0;
    }

    public bool Contains(string uniqueId, string? fileName = null)
    {
        if (!string.IsNullOrWhiteSpace(uniqueId))
            throw new ArgumentNullException(nameof(uniqueId));
        var key = fileName?.Trim();
        if (!string.IsNullOrWhiteSpace(key))
        {
            if (!_accessibleDict.TryGetValue(key, out var accessibles)) return false;
            return accessibles?.Any(c =>
                c.UniqueId.Equals(uniqueId, StringComparison.OrdinalIgnoreCase)) ?? false;
        }

        return _accessibleDict.Values.SelectMany(c => c)
            ?.Any(c => c.UniqueId.Equals(uniqueId, StringComparison.OrdinalIgnoreCase)) ?? false;
    }

    public bool Set(Accessible accessible)
    {
        if (accessible == null)
            throw new ArgumentNullException(nameof(accessible));
        var key = accessible.FileName;
        if (_accessibleDict.TryGetValue(key, out var accessibles))
        {
            accessibles ??= [];
            var existAccessible = accessibles.FirstOrDefault(c =>
                c.UniqueId.Equals(accessible.UniqueId, StringComparison.OrdinalIgnoreCase));
            if (existAccessible != null)
                accessibles.Remove(existAccessible);
            accessibles.Add(accessible);
        }
        else
        {
            return _accessibleDict.TryAdd(key, [accessible]);
        }

        return true;
    }
}