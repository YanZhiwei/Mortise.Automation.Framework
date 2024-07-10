using System.Drawing;
using Mortise.BrowserAccessibility.Options;

namespace Mortise.BrowserAccessibility;

public interface IPageAccessibleAction
{
    public bool IsActive { get; }

    public bool IsReady { get; }

    public string Url { get; }

    public string UniqueId { get; }

    public Task<string> GetTitleAsync();

    public Task SetActivateAsync();

    public Task GotoAsync(string url);

    public Task CloseAsync();

    public Task<bool> GoBackAsync();

    public Task<bool> GoForwardAsync();

    public Task<bool> ReloadAsync();

    public Task<bool> InjectScriptAsync(InjectScriptOptions options);

    public Task<bool> Ping();

    public Task ElementFromPoint(Point location);
}