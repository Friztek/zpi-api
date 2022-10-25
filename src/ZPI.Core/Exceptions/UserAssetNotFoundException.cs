namespace ZPI.Core.Exceptions;

[Serializable]
public class UserAssetNotFoundException : NotFoundException
{
    public UserAssetNotFoundException() : base()
    {

    }
    public UserAssetNotFoundException(string message) : base(message)
    { }

    public UserAssetNotFoundException(string message, Exception e) : base(message, e)
    { }

    public static string GenerateBaseMessage(string UserId, string AssetId) => $"UserId with id: {UserId} does not have Asset with id: {AssetId}";
}