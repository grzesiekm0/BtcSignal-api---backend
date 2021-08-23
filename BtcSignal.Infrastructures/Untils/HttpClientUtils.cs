using System;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Btcsignal.Infrastructures.Untils
{
    public class HttpClientUtils
    {
        private readonly HttpClient _httpClient;

        public HttpClientUtils(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<T> SendAsync<T>(HttpRequestMessage request, bool insensitiveCase = false)
        {
            var result = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            await using (var responseStream = await result.Content.ReadAsStreamAsync())
            {
                if(insensitiveCase)
                    return await JsonSerializer.DeserializeAsync<T>(responseStream, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                });

                return await JsonSerializer.DeserializeAsync<T>(responseStream);
            }
        }

        public async Task<string> SendAsync(HttpRequestMessage request)
        {
            var result = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            using (var responseStream = await result.Content.ReadAsStreamAsync())
            {
                using (var streamReader = new StreamReader(responseStream))
                {
                    return await streamReader.ReadToEndAsync();
                }
            }
        }

        public async Task<T> SendAsyncNewton<T>(HttpRequestMessage request)
        {
            var result = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            using (var responseStream = await result.Content.ReadAsStreamAsync())
            {
                using (var streamReader = new StreamReader(responseStream))
                {
                    return JsonConvert.DeserializeObject<T>(streamReader?.ReadToEnd());
                }
            }
        }

        public async Task<HttpResponseMessage> SendAsyncResponseWithTimeOut(HttpRequestMessage request, CancellationToken ct)
        {
            return await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, ct).ConfigureAwait(false);
        }
    }
}
