namespace ZPI.Core.Exceptions;

[Serializable]
public class UserNotFoundException : NotFoundException
{
    public UserNotFoundException() : base()
    {

    }
    public UserNotFoundException(string message) : base(message)
    { }

    public UserNotFoundException(string message, Exception e) : base(message, e)
    { }

    public static string GenerateBaseMessage(string userId) => $"User with id: {userId} has not been found";
}