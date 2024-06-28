namespace Mortise.Accessibility.Abstractions.Options;

public class LaunchOptions
{
    public string? ExecutablePath { get; set; }

    public string? UserDataDir { get; set; }

    public IDictionary<string, string> Env { get; } = new Dictionary<string, string>();
}