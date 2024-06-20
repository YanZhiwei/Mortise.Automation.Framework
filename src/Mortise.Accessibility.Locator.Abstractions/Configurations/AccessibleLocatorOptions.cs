namespace Mortise.Accessibility.Locator.Abstractions.Configurations;

public class AccessibleLocatorOptions
{
    public IList<IAccessibleLocatorOptionsExtension> LocatorOptions { get; } =
        new List<IAccessibleLocatorOptionsExtension>();

    public void RegisterExtension(IAccessibleLocatorOptionsExtension extension)
    {
        ArgumentNullException.ThrowIfNull(extension);
        LocatorOptions.Add(extension);
    }
}