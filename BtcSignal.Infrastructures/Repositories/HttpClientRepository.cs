using Btcsignal.Infrastructures.Untils;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Btcsignal.Core.Interfaces.Repositories;
//using IdentityModel.Client;

namespace Btcsignal.Infrastructures.Repositories
{
    public class HttpClientRepository : IHttpClientRepository
    {
        private readonly HttpClientUtilsFactory _httpClientUtilsFactory;

        public HttpClientRepository(HttpClientUtilsFactory httpClientUtilsFactory)
        {
            _httpClientUtilsFactory = httpClientUtilsFactory ?? throw new ArgumentNullException(nameof(httpClientUtilsFactory));
        }

        public async Task<T> Post<T>(string path, string json, string hash = null, string token = null)
        {
            var client = _httpClientUtilsFactory.Create();
            var request = new HttpRequestMessage(HttpMethod.Post, path)
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };
            if (hash != null)
            {
                request.Headers.Add("Authentication-Hash", hash);
            }

            if (token != null)
            {
                request.Headers.Add("Authorization", "Bearer " + token);
            }

            return await client.SendAsync<T>(request).ConfigureAwait(false);
        }

        public async Task<HttpResponseMessage> Post(string path, string json, string token = null)
        {
            var cts = new CancellationTokenSource();
            TimeSpan timeout = TimeSpan.FromSeconds(10);
            cts.CancelAfter(timeout);

            var client = _httpClientUtilsFactory.Create();
            var request = new HttpRequestMessage(HttpMethod.Post, path)
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };

            if (token != null)
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            return await client.SendAsyncResponseWithTimeOut(request, cts.Token);
        }

        public async Task<T> PostNewton<T>(string path, string json, string hash = null, string token = null)
        {
            var client = _httpClientUtilsFactory.Create();
            var request = new HttpRequestMessage(HttpMethod.Post, path)
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };
            if (hash != null)
            {
                request.Headers.Add("Authentication-Hash", hash);
            }

            if (token != null)
            {
                request.Headers.Add("Authorization", "Bearer " + token);
            }

            return await client.SendAsyncNewton<T>(request).ConfigureAwait(false);
        }

        public async Task<T> Get<T>(string path, string hash = null, string bearer = null, bool insensitiveCase = false)
        {
            var client = _httpClientUtilsFactory.Create();
            var request = new HttpRequestMessage(HttpMethod.Get, path);
            if (hash != null)
            {
                request.Headers.Add("Authentication-Hash", hash);
            }
            if (!string.IsNullOrEmpty(bearer))
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", bearer);

            return await client.SendAsync<T>(request, insensitiveCase).ConfigureAwait(false);
        }

        public async Task<string> GetString(string path)
        {
            var client = _httpClientUtilsFactory.Create();
            var request = new HttpRequestMessage(HttpMethod.Get, path);

            return await client.SendAsync(request).ConfigureAwait(false);
        }

        public async Task<string> PostString(string path, string text)
        {
            var client = _httpClientUtilsFactory.Create();
            var request = new HttpRequestMessage(HttpMethod.Post, path)
            {
                Content = new StringContent(text, Encoding.UTF8, "text/plain")
            };

            return await client.SendAsync(request).ConfigureAwait(false);
        }

        public async Task<HttpResponseMessage> Put(string path, string json, string bearer = null)
        {
            var cts = new CancellationTokenSource();
            TimeSpan timeout = TimeSpan.FromSeconds(10);
            cts.CancelAfter(timeout);

            var client = _httpClientUtilsFactory.Create();
            var request = new HttpRequestMessage(HttpMethod.Put, path)
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };

            if (bearer != null)
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", bearer);

            return await client.SendAsyncResponseWithTimeOut(request, cts.Token);
        }
    }
}
