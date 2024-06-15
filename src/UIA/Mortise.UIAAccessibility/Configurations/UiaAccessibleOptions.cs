namespace Mortise.UiaAccessibility.Configurations;

public sealed class UiaAccessibleOptions
{
    internal IList<IUiaAccessibleOptionsExtension> AccessibleOptions { get; } =
        new List<IUiaAccessibleOptionsExtension>();

    public void RegisterAccessible(IUiaAccessibleOptionsExtension accessibleOptionsExtension)
    {
        if (accessibleOptionsExtension == null)
            throw new ArgumentNullException(nameof(accessibleOptionsExtension));

        AccessibleOptions.Add(accessibleOptionsExtension);
    }
}