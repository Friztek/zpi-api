using System;
using ZPI.Core.UseCases;

public sealed class SyncWalletValuesPresenter : SyncWalletValuesUseCase.IOutput
{
    public void Success()
    {
        System.Console.WriteLine("Success!");
    }

    public void UnknownError(Exception exception)
    {
        System.Console.WriteLine($"Exception {exception.Message}");
    }
}