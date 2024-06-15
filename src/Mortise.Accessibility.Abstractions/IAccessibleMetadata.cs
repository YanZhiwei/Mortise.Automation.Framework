namespace Mortise.Accessibility.Abstractions;

public interface IAccessibleMetadata
{
    public string[] SupportedProcessNames { get; }

    public string IdentityString { get; }
}