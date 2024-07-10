using System.Diagnostics;

namespace Mortise.BrowserAccessibility;

public interface IBrowserAccessibleAction
{
    public bool IsConnected { get; }
    public Process Process { get; }
    public string UniqueId { get; }
    public IBrowserAccessibleContext Context { get; }
    public Task<IPageAccessibleAction[]?> GetPagesAsync();
    public Task<IPageAccessibleAction> NewPageAsync();
    public Task<IPageAccessibleAction> GetActivePageAsync();
    public Task<IPageAccessibleAction[]?> GetPagesByTitleAsync(string title);
    public Task<IPageAccessibleAction[]?> GetPagesByUrlAsync(string url);
    public Task CloseAsync();
}