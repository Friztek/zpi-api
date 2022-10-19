using NodaTime;

namespace ZPI.Core.Domain;

public sealed record AssetValueModel(
    string AssetIdentifier,
    double Value,
    OffsetDateTime TimeStamp
);