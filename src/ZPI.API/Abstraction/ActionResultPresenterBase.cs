using Microsoft.AspNetCore.Mvc;
using ZPI.Core.Abstraction;

namespace ZPI.API.Abstraction;

public abstract class ActionResultPresenterBase : AwaitableResultPresenterBase<IActionResult>
{
    protected ILogger Logger;
    protected ActionResultPresenterBase(ILogger logger)
    {
        this.Logger = logger;
    }

}
