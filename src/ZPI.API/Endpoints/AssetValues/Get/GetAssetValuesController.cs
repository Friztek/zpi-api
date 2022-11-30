using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NodaTime;
using ZPI.API.Abstraction;
using ZPI.API.DTos;
using ZPI.Core.UseCases;

namespace ZPI.API.Endpoints.AssetValues.Get
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/asset-values")]
    public sealed class AssetValuesController : UseCaseController<GetAssetValuesUseCase, GetAssetValuesPresenter>
    {
        public AssetValuesController(ILogger logger, GetAssetValuesUseCase useCase, GetAssetValuesPresenter presenter)
            : base(logger, useCase, presenter)
        { }

        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(typeof(IEnumerable<AssetValueDto>), StatusCodes.Status200OK)]
        public async ValueTask<IActionResult> GetAllValuesHistory()
        {
            await useCase.Execute(new GetAssetValuesUseCase.Input(), presenter);
            return await presenter.GetResultAsync();
        }
    }
}