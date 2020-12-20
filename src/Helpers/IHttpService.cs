using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace PSI_FRONT.Helpers
{
    public interface IHttpService
    {
        Task<HttpResponseWrapper<T>> Get<T>(string url);
        Task<HttpResponseWrapper<object>> Post<T>(string url, T data);
        Task<HttpResponseWrapper<TResponse>> Post<T, TResponse>(string url, T data);
        void SetAuthorizationHeader(AuthenticationHeaderValue header);
    }
}
