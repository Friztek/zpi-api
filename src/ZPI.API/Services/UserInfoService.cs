using System.Security.Claims;

public class UserInfoService : IUserInfoService
{
    private readonly IHttpContextAccessor contextAccessor;
    private const string EMAIL_CLAIM = "https://how-money/email";

    public UserInfoService(IHttpContextAccessor contextAccessor)
    {
        this.contextAccessor = contextAccessor;
    }

    private Claim? GetCurrentUserClaim(Func<Claim, bool> predicate) => this.contextAccessor.HttpContext?.User.Claims.FirstOrDefault(predicate);
    public string GetCurrentUserId() => this.GetCurrentUserClaim(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

    public string GetCurrentUserEmail() => this.GetCurrentUserClaim(c => c.Type == EMAIL_CLAIM)?.Value;
}
