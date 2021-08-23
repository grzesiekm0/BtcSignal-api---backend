using System.Net.Http;
using System.Threading.Tasks;

namespace Btcsignal.Core.Interfaces.Repositories
{
    public interface IHttpClientRepository
    {
        Task<T> Post<T>(string path, string json, string hash = null, string token = null);
        Task<HttpResponseMessage> Post(string path, string json, string token = null);
        Task<T> PostNewton<T>(string path, string json, string hash = null, string token = null);
        Task<T> Get<T>(string path, string hash = null, string bearer = null, bool insensitiveCase = false);
        Task<string> GetString(string path);
        Task<string> PostString(string path, string text);
        Task<HttpResponseMessage> Put(string path, string json, string bearer = null);
    }
}
 