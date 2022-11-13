using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using NodaTime;
using RestSharp;
using ZPI.Core.Abstraction.Integrations;
using ZPI.Integrations.Configuration;

namespace ZPI.Integrations.Services;

public class ManagementTokenResponse
{
    [JsonProperty("access_token")]
    public string AccessToken { get; set; }

    [JsonProperty("expires_in")]
    public string ExpiresIn { get; set; }

    [JsonProperty("token_type")]
    public string TokenType { get; set; }

    [JsonProperty("scope")]
    public string Scope { get; set; }
}

public class Auth0ManagementTokenProvider : IAuth0ManagementTokenProvider
{
    private const string TokenCacheKey = "ManagementApiToken";
    private const string GranType = "client_credentials";
    private readonly IMemoryCache memoryCache;
    private readonly ManagementApiOptions config;
    private readonly Serilog.ILogger Logger;
    public Auth0ManagementTokenProvider(IOptions<ManagementApiOptions> managementApiOptions, IMemoryCache memoryCache, Serilog.ILogger Logger)
    {
        this.memoryCache = memoryCache;
        this.config = managementApiOptions.Value;
        this.Logger = Logger.ForContext<Auth0ManagementTokenProvider>();
    }

    public async Task<string> GetTokenAsync()
    {
        if (!this.memoryCache.TryGetValue(TokenCacheKey, out string cachedToken))
        {
            this.Logger.Information("Old management token has expired, fetching new one");
            // Key not in cache, so get data.
            var (newToken, expiresTime) = await this.FetchTokenWithExpireTime();

            cachedToken = newToken;

            // Set cache options.
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                // Keep in cache for this time, reset time if accessed.
                .SetSlidingExpiration(TimeSpan.FromSeconds(expiresTime * 0.8));

            // Save data in cache.
            memoryCache.Set(TokenCacheKey, cachedToken, cacheEntryOptions);

            this.Logger.Information($"New management token has been fetched and saved in cache, expire date: {(SystemClock.Instance.GetCurrentInstant() + Duration.FromSeconds(expiresTime)).InUtc()} ");
        }
        return cachedToken;
    }

    private string GetSerializedRequestParameter()
    {
        var parameter = new { client_id = this.config.ClientId, client_secret = this.config.ClientSecret, audience = this.config.Audience, grant_type = GranType };
        return JsonConvert.SerializeObject(parameter);
    }


    private async Task<(string, int)> FetchTokenWithExpireTime()
    {
        var client = new RestClient(this.config.TokenEndpointUrl);
        var request = new RestRequest();

        var parameter = this.GetSerializedRequestParameter();
        request.AddHeader("content-type", "application/json");
        request.AddParameter("application/json", parameter, ParameterType.RequestBody);

        var response = await client.PostAsync(request);
        var deserializedToken = JsonConvert.DeserializeObject<ManagementTokenResponse>(response.Content);
        
        return (deserializedToken.AccessToken, int.Parse(deserializedToken.ExpiresIn));
    }
}