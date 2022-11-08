// using NodaTime;
// using ZPI.Core.Abstraction;
// using ZPI.Core.Abstraction.Repositories;
// using ZPI.Core.Domain;
// using ZPI.Core.Exceptions;

// namespace ZPI.Core.UseCases;

// public sealed class GetTransactionsUseCase : IUseCase<GetTransactionsUseCase.Input, GetTransactionsUseCase.IOutput>
// {
//     private readonly ITransactionepository repository;

//     public GetTransactionsUseCase(ITransactionepository repository)
//     {
//         this.repository = repository;
//     }

//      public async Task Execute(Input inputPort, IOutput outputPort)
//     {
//         try
//         {
//             var assets = await this.repository.SearchAsync(new IAssetValuesRepository.GetAssetValues());
//             outputPort.Success(assets);
//         }
//         catch (Exception e)
//         {
//             outputPort.UnknownError(e);
//         }
//     }


//     public sealed record class Input(string AssetName, OffsetDate? From, OffsetDate? To, string UserId) : : IUserPreferencesRepository.GetUserPreferences(UserId), IInputPort;;
//     public interface IOutput : IOutputPort
//     {
//         public void Success(IEnumerable<TransactionModel> transactionValues);
//         public void UnknownError(Exception exception);
//     }
// }