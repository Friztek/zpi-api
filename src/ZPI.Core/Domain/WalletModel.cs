using NodaTime;
namespace ZPI.Core.Domain
{
    public sealed record WalletModel(
        string UserIdentifier,
        double Value,
        OffsetDateTime TimeStamp
    );
}