using Mortise.Accessibility.Locator.Abstractions.Configurations;
using Mortise.Accessibility.Locator.Json.Configurations;

namespace Mortise.Accessibility.Locator.Json.Extensions;

public static class AccessibleLocatorOptionsExtension
{
    public static AccessibleLocatorOptions UseLocalStorage(this JsonLocatorStorageOptions options)
    {
        if (options == null)
            throw new ArgumentNullException(nameof(options));
        options.RegisterExtension(new JsonLocatorStorageOptionsExtension(options));
        return options;
    }
}