using Mortise.Accessibility.Abstractions;

namespace Mortise.Accessibility.Locator.Abstractions;

public interface IAccessibleLocatorStorage
{
    IReadOnlyDictionary<string, List<Accessible>> AccessibleDict { get; }
    bool Add(Accessible accessible);

    Accessible? Remove(string uniqueId, string? fileName = null);

    void Save();

    long GetCount(string? fileName = null);

    bool Contains(string uniqueId, string? fileName = null);

    bool Set(Accessible accessible);

    bool Load();
}