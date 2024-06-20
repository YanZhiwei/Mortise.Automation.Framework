using System.Text;
using Mortise.Accessibility.Locator.Abstractions.Configurations;

namespace Mortise.Accessibility.Locator.Json.Configurations;

public sealed class JsonLocatorStorageOptions : AccessibleLocatorOptions
{
    public string AppData { get; set; } = Path.Combine(AppContext.BaseDirectory, "locators");

    public Encoding Encoding { get; set; } = Encoding.UTF8;
}