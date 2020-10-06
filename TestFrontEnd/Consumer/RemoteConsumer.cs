using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TestFrontEnd.Consumer
{
    public class RemoteConsumer : IRemoteConsumer
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RemoteConsumer(IHttpClientFactory httpClientFactory,
            IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
        }

        public T GetData<T>(string url)
        {
            var client = _httpClientFactory.CreateClient();
            return GetDataFromUrl<T>(client, url);
        }

        private T GetDataFromUrl<T>(HttpClient client, string url)
        {
            var response = AccessTokenWrapper(() => client.GetAsync(url)).GetAwaiter().GetResult();
            T data = default;
            try
            {
                using (var stream = response.Content.ReadAsStreamAsync().GetAwaiter().GetResult())
                {
                    using (var streamReader = new StreamReader(stream, Encoding.UTF8))
                    {
                        data = JsonConvert.DeserializeObject<T>(streamReader.ReadToEnd());
                    }
                }
            }
            catch (Exception ex)
            {
                var test = ex;
            }
            return data;
        }

        public T GetData<T>(string url, string accessToken)
        {
            var client = _httpClientFactory.CreateClient();
            client.SetBearerToken(accessToken);
            return GetDataFromUrl<T>(client, url);             
        }

        private async Task RefreshAccessToken()
        {
            var identityServerClient = _httpClientFactory.CreateClient();
            var discoveryDocument = await identityServerClient.GetDiscoveryDocumentAsync("https://localhost:44349/");
            var httpContext = _httpContextAccessor.HttpContext;
            var accessToken = await httpContext.GetTokenAsync("access_token");
            var refreshToken = await httpContext.GetTokenAsync("refresh_token");
            var refreshTokenClient = _httpClientFactory.CreateClient();
            var tokenResponse = await refreshTokenClient.RequestRefreshTokenAsync(new RefreshTokenRequest()
            {
                RefreshToken = refreshToken,
                Address = discoveryDocument.TokenEndpoint,
                ClientId = "client_id_mvc",
                ClientSecret = "client_secret_mvc"
            });

            var authInfo = await httpContext.AuthenticateAsync("Cookie");
            authInfo.Properties.UpdateTokenValue("access_token", tokenResponse.AccessToken);
            authInfo.Properties.UpdateTokenValue("id_token", tokenResponse.IdentityToken);
            authInfo.Properties.UpdateTokenValue("refresh_token", tokenResponse.RefreshToken);

            await httpContext.SignInAsync("Cookie", authInfo.Principal, authInfo.Properties);
        }

        private async Task<HttpResponseMessage> AccessTokenWrapper(Func<Task<HttpResponseMessage>> initialRequest)
        {
            var response = await initialRequest();
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                await RefreshAccessToken();
                response = await initialRequest();
            }
            return response;
        }

        public T PostData<T>(string url, string accessToken, object postObject)
        {
            var client = _httpClientFactory.CreateClient();
            client.SetBearerToken(accessToken);
            var content = new StringContent(JsonConvert.SerializeObject(postObject), Encoding.UTF8, "application/json");
            var response = AccessTokenWrapper(() => client.PostAsync(url, content)).GetAwaiter().GetResult();
            T data = default;
            try
            {
                using (var stream = response.Content.ReadAsStreamAsync().GetAwaiter().GetResult())
                {
                    using (var streamReader = new StreamReader(stream, Encoding.UTF8))
                    {
                        data = JsonConvert.DeserializeObject<T>(streamReader.ReadToEnd());
                    }
                }
            }
            catch (Exception ex)
            {
                var test = ex;
            }
            return data;
        }
    }
}
