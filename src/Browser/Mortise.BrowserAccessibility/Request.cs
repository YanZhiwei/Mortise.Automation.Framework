namespace Mortise.BrowserAccessibility;

public abstract class Request<TRes>
{
    public Response<TRes> Response { get; private set; }

    public TRes Result => Response.Result;

    protected virtual async Task OnBeforeRequestAsync()
    {
        await Task.CompletedTask;
    }

    protected abstract Task<Response<TRes>> EvaluateAsync(IFunction function);

    protected abstract Task<Response<TRes>> ContinueEvaluateAsync(Frame iframe);

    public async Task ExecuteAsync(IFunction function)
    {
        await OnBeforeRequestAsync();
        var response = await EvaluateAsync(function);
        if (response.Ok)
        {
            Response = response;
            return;
        }
        await OnContinueExecuteRequestAsync(response);
    }

    private async Task OnContinueExecuteRequestAsync(Response<TRes> response)
    {
        var iframes = response.Frame?.Child;
        if (iframes?.Any() ?? false)
        {
            foreach (var iframe in iframes)
            {
                var iframeResponse = await ContinueEvaluateAsync(iframe);
                if (iframeResponse.Ok)
                {
                    Response = iframeResponse;
                    break;
                }

                await OnContinueExecuteRequestAsync(iframeResponse);
            }
        }
    }
}