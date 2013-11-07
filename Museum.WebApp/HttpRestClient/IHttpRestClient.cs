using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace HttpRestClient
{
    /// <summary>
    /// Interface for sending http requests to rest endpoints
    /// </summary>
    public interface IHttpRestClient
    {
        /// <summary>
        /// Executes an HttpRequest Asyncrhonously
        /// </summary>
        /// <typeparam name="T">Expected return type</typeparam>
        /// <param name="method">HttpRequest Method: GET, POST, PUT, DELETE</param>
        /// <param name="serviceHost">host uri</param>
        /// <param name="resource">relative resource uri</param>
        /// <param name="options">Request options</param>
        /// <returns>Task of type T</returns>
        Task<T> ExecuteAsync<T>(HttpMethod method, Uri serviceHost, string resource, RequestOptions options = null);
    }
}