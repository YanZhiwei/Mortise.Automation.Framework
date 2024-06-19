using Mortise.Accessibility.Abstractions;

namespace Mortise.Accessibility.LocatorStorage.Abstractions;

public interface IAccessibleLocators
{
    IEnumerable<Accessible> Accessibles { get; }

    bool Add(Accessible accessible);

    Accessible Remove(string uniqueId, string? fileName = null);

    void Save();

    long GetCount(string? fileName = null);

    bool Contains(string uniqueId, string? fileName = null);

    bool Set(Accessible accessible);
}