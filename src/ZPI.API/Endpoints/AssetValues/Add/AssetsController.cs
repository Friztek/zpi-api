using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZPI.API.Abstraction;
using ZPI.API.DTos;
using ZPI.Core.UseCases;

namespace ZPI.API.Endpoints.AssetValues.Add;

[ApiController]
[Route("api/asset-values")]
[AllowAnonymous]
public sealed class AssetValuesController : UseCaseController<AddAssetValueUseCase, AddAssetValuePresenter>
{
    private static readonly HttpClient client = new();
    private const string apiUrl = "http://104.45.159.232:8000/api/check_alert/";
    public AssetValuesController(ILogger logger, AddAssetValueUseCase useCase, AddAssetValuePresenter presenter)
        : base(logger, useCase, presenter)
    { }

    [HttpPost("{assetIdentifier}", Name = nameof(PatchAssetValue))]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesDefaultResponseType]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async ValueTask<IActionResult> PatchAssetValue(string assetIdentifier, AddAssetValueDto dto)
    {
        await useCase.Execute(new AddAssetValueUseCase.Input(assetIdentifier, dto.Value, dto.TimeStamp), presenter);
        await client.GetStringAsync(apiUrl + "?asset=" + assetIdentifier);
        return await presenter.GetResultAsync();
    }
}
