using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZPI.API.Abstraction;
using ZPI.API.DTos;
using ZPI.Core.UseCases;

namespace ZPI.API.Endpoints.Users.Add;

[ApiController]
[AllowAnonymous]
[Route("api/users")]
public sealed class UsersController : UseCaseController<AddUserUseCase, AddUserPresenter>
{
    public UsersController(ILogger logger, AddUserUseCase useCase, AddUserPresenter presenter)
        : base(logger, useCase, presenter)
    { }

    [HttpPost(Name = nameof(AddUser))]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesDefaultResponseType]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async ValueTask<IActionResult> AddUser(AddUserDto dto)
    {
        await useCase.Execute(new AddUserUseCase.Input(dto.UserId), presenter);
        return await presenter.GetResultAsync();
    }
}
