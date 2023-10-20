using FreeCourse.Web.Models;
using FreeCourse.Web.Services.Abstracts;
using IdentityModel.AspNetCore.AccessTokenManagement;
using IdentityModel.Client;
using Microsoft.Extensions.Options;

namespace FreeCourse.Web.Services;

public class ClientCredentialTokenService : IClientCredentialTokenService
{
    private readonly ServiceApiSettings _serviceApiSettings;
    private readonly ClientSettings _clientSettings;
    private readonly IClientAccessTokenCache _accessTokenCache;
    private readonly HttpClient _httpClient;

    public ClientCredentialTokenService(IOptions<ServiceApiSettings> serviceApiSettings, IOptions<ClientSettings> clientSettings, IClientAccessTokenCache accessTokenCache, HttpClient httpClient)
    {
        _serviceApiSettings = serviceApiSettings.Value;
        _clientSettings = clientSettings.Value;
        _accessTokenCache = accessTokenCache;
        _httpClient = httpClient;
    }

    public async Task<string> GetToken()
    {
        var currentToken = await _accessTokenCache.GetAsync("WebClientToken");

        if (currentToken != null)
        {
            return currentToken.AccessToken;
        }
        
        var discovery = await _httpClient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest()
        {
            Address = _serviceApiSettings.IdentityBaseUri,
            Policy = new DiscoveryPolicy() { RequireHttps = false }
        });

        if (discovery.IsError)
            throw discovery.Exception;

        var clientCredentialTokenRequest = new ClientCredentialsTokenRequest()
        {
            ClientId = _clientSettings.WebClient.ClientId,
            ClientSecret = _clientSettings.WebClient.ClientSecret,
            Address = discovery.TokenEndpoint
        };

        var newToken = await _httpClient.RequestClientCredentialsTokenAsync(clientCredentialTokenRequest);

        if (newToken.IsError)
            throw newToken.Exception;

        await _accessTokenCache.SetAsync("WebClientToken", newToken.AccessToken, newToken.ExpiresIn);

        return newToken.AccessToken;
    }
}