namespace ZPI.Core.Domain;

public sealed record UserModel(
    string Id,
    string FullName,
    string Email,
    UserPreferencesModel Preferences
);