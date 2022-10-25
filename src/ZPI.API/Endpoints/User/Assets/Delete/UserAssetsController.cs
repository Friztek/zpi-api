using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZPI.API.Abstraction;
using ZPI.API.DTos;
using ZPI.Core.UseCases;

namespace ZPI.API.Endpoints.User.Assets.Delete;

[ApiController]
[Route("api/users")]
public sealed class UserAssetsController : UseCaseController<DeleteUserAssetUseCase, DeleteUserAssetsPresenter>
{
    public UserAssetsController(ILogger logger, DeleteUserAssetUseCase useCase, DeleteUserAssetsPresenter presenter)
        : base(logger, useCase, presenter)
    { }

    [HttpDelete("me/assets/{assetName}", Name = nameof(DeleteUserAsset))]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesDefaultResponseType]
    [ProducesResponseType(typeof(IEnumerable<UserAssetDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public async ValueTask<IActionResult> DeleteUserAsset(string assetName)
    {
        await useCase.Execute(new DeleteUserAssetUseCase.Input("masno", assetName), presenter);
        return await presenter.GetResultAsync();
    }
}
