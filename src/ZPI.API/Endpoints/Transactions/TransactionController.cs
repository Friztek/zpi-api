// using System.Net.Mime;
// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Mvc;
// using Newtonsoft.Json;
// using NodaTime;
// using ZPI.API.Abstraction;
// using ZPI.API.DTos;
// using ZPI.Core.Abstraction.Repositories;
// using ZPI.Core.UseCases;

// namespace ZPI.API.Endpoints.AssetValues.Get
// {
// [ApiController]
// [Route("api/transactions")]
// public sealed class TransactionController : ControllerBase
// {
//     private readonly ITransactionepository repository;
//     private readonly IUserInfoService service;
//     public TransactionController(ITransactionepository repository, IUserInfoService service) { 
//         this.repository = repository;
//         this.service = service;
//     }

//     [Produces(MediaTypeNames.Application.Json)]
//     [Consumes(MediaTypeNames.Application.Json)]
//     [ProducesDefaultResponseType]
//     [ProducesResponseType(typeof(IEnumerable<TransactionDto>), StatusCodes.Status200OK)]
//     public async Task<Object> SearchTransactions(TransactionBodyDto transaction)
//     {
//         var id = service.GetCurrentUserId();
//         var asset = await this.repository.SearchAsync(transaction.AssetIdentifier, transaction.From, transaction.To, id);
//         var model = JsonConvert.DeserializeObject<List<AlertDto>>(respone);
//         return model;
//     }
// }
//}