using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZPI.API.Abstraction;
using ZPI.API.DTos;
using ZPI.Core.UseCases;

namespace ZPI.API.Endpoints.Assets.GetAll;

[ApiController]
[Route("api/assets")]
public sealed class AssetsController : UseCaseController<GetAllAssetsUseCase, GetAllAssetsPresenter>
{
    public AssetsController(ILogger logger, GetAllAssetsUseCase useCase, GetAllAssetsPresenter presenter)
        : base(logger, useCase, presenter)
    { }

    [HttpGet(Name = nameof(GetAllAssets))]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesDefaultResponseType]
    [ProducesResponseType(typeof(IEnumerable<AssetDto>), StatusCodes.Status200OK)]
    public async ValueTask<IActionResult> GetAllAssets()
    {
        await useCase.Execute(new GetAllAssetsUseCase.Input(), presenter);
        return await presenter.GetResultAsync();
    }
}
