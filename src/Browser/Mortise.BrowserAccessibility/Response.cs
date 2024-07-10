namespace Mortise.BrowserAccessibility;

public class Response<TRes> 
{
    public Frame? Frame { get; set; }

    public TRes? Result { get; set; }

    public bool Ok { get; set; }

    public bool IsNavigationRequest { get; set; }
}