namespace Mortise.Accessibility.Abstractions;

public interface IAccessibleDescriptor
{
    public string[] SupportedProcessNames { get; }

    public string IdentityString { get; }
}