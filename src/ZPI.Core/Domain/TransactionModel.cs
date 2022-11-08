using NodaTime;
namespace ZPI.Core.Domain
{
    public sealed record TransactionModel(
        string UserIdentifier,
        string AssetIdentifier,
        double Value,
        OffsetDateTime TimeStamp
    );
}