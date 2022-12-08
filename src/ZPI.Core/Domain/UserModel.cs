namespace ZPI.Core.Domain;

public sealed record UserModel(
    string UserId,
    string FullName,
    string Email
);