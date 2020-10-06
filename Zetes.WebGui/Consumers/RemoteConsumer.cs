using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;

namespace Zetes.WebGui.Consumers
{
    public class RemoteConsumer : IRemoteConsumer
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _serverUrl;

        public RemoteConsumer(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            var appSettings = configuration.GetSection("AppSettings");
            _serverUrl = appSettings.GetValue<string>("ServerUrl");            
        }

        private HttpClient SetAccessToken(string accessToken)
        {
            var client = _httpClientFactory.CreateClient();
            client.SetBearerToken(accessToken);
            return client;
        }

        public async Task<TResponse> GetDate<TResponse>(string url, string accessToken)
        {

            var client = SetAccessToken(accessToken);
            var apiResponse = await client.GetAsync(Path.Combine(_serverUrl, url));
            return await GetData<TResponse>(apiResponse);
        }

        private async Task<TResponse> GetData<TResponse>(HttpResponseMessage response)
        {
            TResponse data;
            using (var stream = await response.Content.ReadAsStreamAsync())
            {
                using (var streamReader = new StreamReader(stream, Encoding.UTF8))
                {
                    data = JsonConvert.DeserializeObject<TResponse>(streamReader.ReadToEnd());
                }
            }
            return data;

        }

        public async Task<TResponse> PostData<TResponse>(string url, object obj, string accessToken)
        {
            TResponse data;
            using (var client = SetAccessToken(accessToken))
            {
                using (var request = new HttpRequestMessage(HttpMethod.Post, Path.Combine(_serverUrl, url)))
                {
                    var apiResponse = await client.PostAsync(url, CreateHttpContent(obj));
                    data = await GetData<TResponse>(apiResponse);
                }
            }

            return data;
        }

        private HttpContent CreateHttpContent(object content)
        {
            HttpContent httpContent = null;

            if (content != null)
            {
                var ms = new MemoryStream();
                SerialiseJsonIntoStream(content, ms);
                ms.Seek(0, SeekOrigin.Begin);
                httpContent = new StreamContent(ms);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            }

            return httpContent;
        }

        private void SerialiseJsonIntoStream(object value, Stream stream)
        {
            using (var sw = new StreamWriter(stream, new UTF8Encoding(false), 1024, true))
            {
                using (var jwt = new JsonTextWriter(sw) { Formatting = Formatting.None })
                {
                    var js = new JsonSerializer();
                    js.Serialize(jwt, value);
                    jwt.Flush();
                }
            }
        }
    }
}
