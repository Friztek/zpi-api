namespace ZPI.Core.Exceptions;

[Serializable]
public class AssetNotFoundException : NotFoundException
{
    public AssetNotFoundException() : base()
    {

    }
    public AssetNotFoundException(string message) : base(message)
    { }

    public AssetNotFoundException(string message, Exception e) : base(message, e)
    { }

    public static string GenerateBaseMessage(string assetName) => $"Asset with id: {assetName} has not been found";
}