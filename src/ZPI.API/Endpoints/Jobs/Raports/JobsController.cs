using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZPI.API.Abstraction;
using ZPI.API.DTos;
using ZPI.Core.UseCases;

namespace ZPI.API.Endpoints.Assets.GetAll;

[ApiController]
[AllowAnonymous]
[Route("api/jobs")]
public sealed class JobsController : UseCaseController<SendRaportsDataUseCase, SendRaportPresenter>
{
    public JobsController(ILogger logger, SendRaportsDataUseCase useCase, SendRaportPresenter presenter)
        : base(logger, useCase, presenter)
    { }

    [HttpGet("raports", Name = nameof(ProcRaport))]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesDefaultResponseType]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async ValueTask<IActionResult> ProcRaport()
    {
        await useCase.Execute(new SendRaportsDataUseCase.Input(), presenter);
        return await presenter.GetResultAsync();
    }
}
