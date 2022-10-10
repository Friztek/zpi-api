using System.Dynamic;
using Microsoft.AspNetCore.Mvc;

namespace ZPI.API.Utils;

public static class ActionResultFactory
{
    public static IActionResult Ok200() => new OkResult();
    public static IActionResult Ok200(object result) => new OkObjectResult(result);

    public static IActionResult Created201() => new StatusCodeResult(201);
    public static IActionResult Created201(object result) => new ObjectResult(result) { StatusCode = 201 };
    public static IActionResult BadRequest400() => new BadRequestResult();
    public static IActionResult BadRequest400(object result) => new BadRequestObjectResult(result);
    public static IActionResult NotFound404(string message, object? input = null)
    {
        dynamic @object = new ExpandoObject();
        @object.Message = message;
        @object.Input = input;

        return new NotFoundObjectResult(@object);
    }
}
