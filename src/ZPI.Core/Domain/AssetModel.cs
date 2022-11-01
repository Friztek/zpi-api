namespace ZPI.Core.Domain;

public sealed record AssetModel(
    string Name,
    string FriendlyName,
    string Category,
    string? Symbol
);