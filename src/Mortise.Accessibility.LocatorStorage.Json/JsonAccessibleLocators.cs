using Mortise.Accessibility.Abstractions;
using Mortise.Accessibility.LocatorStorage.Abstractions;

namespace Mortise.Accessibility.LocatorStorage.Json
{
    public sealed class JsonAccessibleLocators: IAccessibleLocators
    {
        public IEnumerable<Accessible> Accessibles { get; }
        public bool Add(Accessible accessible)
        {
            throw new NotImplementedException();
        }

        public Accessible Remove(string uniqueId, string? fileName = null)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public long GetCount(string? fileName = null)
        {
            throw new NotImplementedException();
        }

        public bool Contains(string uniqueId, string? fileName = null)
        {
            throw new NotImplementedException();
        }

        public bool Set(Accessible accessible)
        {
            throw new NotImplementedException();
        }
    }
}
