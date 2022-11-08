using System.Threading.Tasks;

namespace ZPI.Core.Abstraction.Integrations;

public interface IAuth0ManagementTokenProvider
{
    public Task<string> GetTokenAsync();
}