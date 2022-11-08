using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NodaTime;
using ZPI.API.Abstraction;
using ZPI.API.DTos;
using ZPI.Core.UseCases;

namespace ZPI.API.Endpoints.AssetValues.Search;

[ApiController]
[AllowAnonymous]
[Route("api/asset-values")]
public sealed class AssetValuesController : UseCaseController<SearchAssetValuesUseCase, SearchAssetValuesPresenter>
{
    public AssetValuesController(ILogger logger, SearchAssetValuesUseCase useCase, SearchAssetValuesPresenter presenter)
        : base(logger, useCase, presenter)
    { }

    [HttpGet("{assetName}",Name = nameof(SearchAssetValuesHistory))]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesDefaultResponseType]
    [ProducesResponseType(typeof(IEnumerable<AssetValueDto>), StatusCodes.Status200OK)]
    public async ValueTask<IActionResult> SearchAssetValuesHistory(string assetName, OffsetDate? from, OffsetDate? to)
    {
        await useCase.Execute(new SearchAssetValuesUseCase.Input(assetName, from, to), presenter);
        return await presenter.GetResultAsync();
    }
}
