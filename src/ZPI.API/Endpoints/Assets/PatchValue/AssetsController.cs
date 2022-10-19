using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using ZPI.API.Abstraction;
using ZPI.API.DTos;
using ZPI.Core.UseCases;

namespace ZPI.API.Endpoints.Assets.PatchValue;

[ApiController]
[Route("api/assets")]
public sealed class PatchAssetValueController : UseCaseController<PatchAssetValueUseCase, PatchAssetValuePresenter>
{
    public PatchAssetValueController(ILogger logger, PatchAssetValueUseCase useCase, PatchAssetValuePresenter presenter)
        : base(logger, useCase, presenter)
    { }

    [HttpPost("{assetIdentifier}", Name = nameof(PatchAssetValue))]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesDefaultResponseType]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async ValueTask<IActionResult> PatchAssetValue(string assetIdentifier, PatchAssetValueDto dto)
    {
        await useCase.Execute(new PatchAssetValueUseCase.Input(assetIdentifier, dto.Value, dto.TimeStamp), presenter);
        return await presenter.GetResultAsync();
    }
}
